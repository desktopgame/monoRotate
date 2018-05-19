using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;
using Xna2D.Game;
using Xna2D.Game.Blocks;
using Xna2D.Input;
using Microsoft.Xna.Framework.Input;
using TestGame.Scenes.Play.Blocks;

namespace TestGame.Scenes.Play
{
	/// <summary>
	/// プレイ画面.
	/// </summary>
	public class PlayScene : SceneBase, ILayered
	{
		private struct RectAlpha
		{
			public Rectangle Rect { set; get; }
			public float Alpha { set; get; }
			public RectAlpha(Rectangle rect, float alpha)
			{
				this.Rect = rect;
				this.Alpha = alpha;
			}
		}
		private StageSelector stageSelector;
		private GameObjectCollection gObjCollection;
		private Sound sound;

		private static readonly Handle PAUSE = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Q),
			(index, pad) => pad.Buttons.X == ButtonState.Pressed
		);

		public PlayScene(StageSelector stageSelector, Sound sound)
		{
			this.Next = (int)SceneTypes.Title;
			this.stageSelector = stageSelector;
			this.sound = sound;
		}

		public override void Update(GameTime gameTime)
		{
			Detector detector = Detector.GetInstance();
			//Wで最初から
			if(detector.IsDetect(Keys.W))
			{
				Show();
			//ポーズ
			} else if(detector.IsDetect(PAUSE))
			{
				this.Next = (int)SceneTypes.Pause;
				this.IsEnd = true;
			}
			//全てのオブジェクトを更新
			for(int i=0; i<gObjCollection.Count; i++)
			{
				IGameObject gObj = gObjCollection[i];
				gObj.Update(gameTime, gObjCollection);
			}
			//オブジェクトのデスポーン
			gObjCollection.RemoveAll((gObj) => gObj.IsDespawn);
			//オブジェクトの追加
			gObjCollection.AddExec();
			//時間切れ
			if(gObjCollection.FindObject<TimeObject>(elem => elem is TimeObject).CurrentTime <= 0)
			{
				this.IsEnd = true;
				this.Next = (int)SceneTypes.GameOver;
			}
			if(gObjCollection.FindObject<FlagBlock>(elem => elem is FlagBlock).GoalPlayer)
			{
				this.IsEnd = true;
				this.Next = (int)SceneTypes.Clear;
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			var camera = gObjCollection.FindObject<Camera>((obj) => obj is Camera);
			var player = gObjCollection.FindObject<IPlayer>((obj) => obj is IPlayer);
			var time = gObjCollection.FindObject<TimeObject>((obj) => obj is TimeObject);
			camera.Calculate(player.Position);
			//背景の塗りつぶし
			renderer.Begin();
			renderer.FillRectangle(new Rectangle(0, 0, GameConstants.SCREEN_WIDTH, GameConstants.SCREEN_HEIGHT), Color.Black);
			renderer.End();
			//全てのオブジェクトを描画
			renderer.Begin(camera.matrix);
			for(int i = 0; i < gObjCollection.Count; i++)
			{
				var e = gObjCollection[i];
				if(e == time || e is TimeObject) {
					continue;
				}
				e.Draw(gameTime, renderer, gObjCollection);
			}
			renderer.End();
			//タイムは回転を無視して描画
			//NOTE:回転を無視して描画するかどうかを表すプロパティの追加
			renderer.Begin();
			time.Draw(gameTime, renderer, gObjCollection);
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			sound.StopBGM();
			sound.PlayBGM("Sound/Song/Play");
			//ステージを読み込む
			if(stageSelector.Option == StageSelector.Options.Init)
			{
				this.gObjCollection = new GameObjectCollection();
				List<IGameData> gameData = GIO.Load(stageSelector.Path);
				gObjCollection.AddRange(gameData.ConvertAll((elem) => elem as IGameObject));
				gObjCollection.Add(new Rotater());
			}
			//初期化しておく
			this.stageSelector.Option = StageSelector.Options.Init;
		}

		public override void Hide() {
			base.Hide();
			if(this.Next == (int)SceneTypes.Pause) {
				sound.PauseBGM();
			} else {
				sound.StopBGM();
			}
		}

		public bool IsNeedBackLayer(LayeredScene scene)
		{
			return false;
		}

		public bool IsNeedFrontLayer(LayeredScene scene)
		{
			return false;
		}
	}
}
