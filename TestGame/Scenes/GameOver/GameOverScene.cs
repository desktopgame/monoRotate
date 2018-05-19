using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;
using Xna2D.Game;
using Xna2D.Input;
using Microsoft.Xna.Framework.Input;

namespace TestGame.Scenes.GameOver
{
	/// <summary>
	/// ゲームオーバー画面.
	/// </summary>
	public class GameOverScene : SceneBase
	{
		private IScene playScene;
		private Vector2 gameOverPosition;
		private Vector2 continuePosition;
		private float yesnoAlpha;
		private int selectedIndex;
		private Sound sound;

		private static readonly Handle ENTER = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Space) | key.IsKeyDown(Keys.Enter),
			(index, pad) => pad.Buttons.A == ButtonState.Pressed
		);

		private static readonly Vector2 GAMEOVER_SIZE = new Vector2(207, 30);
		private static readonly Vector2 CONTINUE_SIZE = new Vector2(197, 30);
		private static readonly Vector2 YES_SIZE = new Vector2(172, 30);
		private static readonly Vector2 NO_SIZE = new Vector2(48, 30);

		private static readonly Vector2 GAMEOVER_TO = (GameConstants.SCREEN_SIZE - GAMEOVER_SIZE) / 2;
		private static readonly Vector2 CONTINUE_TO = (GameConstants.SCREEN_SIZE - CONTINUE_SIZE) / 2;

		static GameOverScene()
		{

			GameOverScene.GAMEOVER_TO.Y -= 100;
			GameOverScene.CONTINUE_TO.X -= 100;
		}

		public GameOverScene(IScene playScene, Sound sound)
		{
			this.playScene = playScene;
			this.sound = sound;
		}

		public override void Update(GameTime gameTime)
		{
			//ゲームオーバーの文字を降ろす
			if(gameOverPosition.Y < GAMEOVER_TO.Y)
			{
				this.gameOverPosition.Y += 4f;
			//もう降りたのでコンティニューの文字をスライド
			} else if(continuePosition.X < CONTINUE_TO.X)
			{
				this.continuePosition.X += 2f;
			//スライドしたので透明度を上げる
			} else if(yesnoAlpha <= 1f)
			{
				this.yesnoAlpha += 0.01f;
			}
			//選択項目の変更
			Detector detector = Detector.GetInstance();
			if(detector.IsDetect(Handle.LEFT))
			{
				selectedIndex--;
			} else if(detector.IsDetect(Handle.RIGHT))
			{
				selectedIndex++;
			}
			if(selectedIndex >= 2)
			{
				this.selectedIndex = 0;
			} else if(selectedIndex < 0)
			{
				this.selectedIndex = 1;
			}
			//選択の決定
			if(yesnoAlpha >= 1f && detector.IsDetect(ENTER))
			{
				this.IsEnd = true;
				this.Next = (int)(selectedIndex == 0 ? SceneTypes.Play : SceneTypes.Select);
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			playScene.Draw(gameTime, renderer);
			renderer.Begin();
			renderer.Draw("Textures/GameOver", gameOverPosition, Color.White);
			renderer.Draw("Textures/Continue", continuePosition, Color.White);
			renderer.Draw("Textures/Yes", continuePosition + new Vector2(250, 0), (selectedIndex == 0 ? Color.White : Color.Gray) * yesnoAlpha);
			renderer.Draw("Textures/No", continuePosition + new Vector2(350, 0), (selectedIndex == 1 ? Color.White : Color.Gray) * yesnoAlpha);
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			//Game Overの初期位置
			this.gameOverPosition = GAMEOVER_TO;
			gameOverPosition.Y = 0;
			//Continueの初期位置
			this.continuePosition = CONTINUE_TO;
			continuePosition.X = 0;
			//デフォルトではYes, Noともに透明度0
			this.yesnoAlpha = 0f;
			this.selectedIndex = 0;
			sound.PlayBGM("Sound/Song/GameOver");
		}

		public override void Hide() {
			base.Hide();
			sound.StopBGM();
		}
	}
}
