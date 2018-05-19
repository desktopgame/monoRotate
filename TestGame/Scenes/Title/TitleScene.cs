using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Scenes;
using Xna2D.Input;
using Xna2D.Contents;
using Xna2D.Views;
using Xna2D.Game;
using Xna2D.Utilities;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Scenes.Title
{
	/// <summary>
	/// タイトル画面.
	/// </summary>
	public class TitleScene : SceneBase, ILayered
	{
		private static readonly Vector2 TITLE_SIZE = new Vector2(385, 80);
		private static readonly Vector2 TITLE_ORIGIN = Vector2.Zero;
		private static readonly Vector2 START_SIZE = new Vector2(480, 120);
		private static readonly Vector2 CREDIT_SIZE = new Vector2(498, 120);
		private static readonly Vector2 EXIT_SIZE = new Vector2(306, 120);
		private static readonly Vector2 TITLE_POS;

		static TitleScene()
		{
			TitleScene.TITLE_POS = ((GameConstants.SCREEN_SIZE - TITLE_SIZE) / 2);
			TitleScene.TITLE_POS.Y = 100;
		}

		private Button[] buttons;
		private Vector2[] sizes;
		private List<Particle> particleList;
		private float titleRotate;
		private int selectedIndex;
		private bool pushButton;
		private Sound sound;

		public TitleScene(Sound sound)
		{
			this.Next = (int)SceneTypes.Select;
			this.selectedIndex = 0;
			this.particleList = new List<Particle>();
			this.pushButton = false;
			this.titleRotate = 0f;
			this.sound = sound;
			InitView();
		}

		private void InitView()
		{
			//ボタンの生成
			this.buttons = new Button[]
			{
				new Button("Textures/Start", SceneTypes.Select),
				new Button("Textures/Credit", SceneTypes.Credit),
				new Button("Textures/Exit", SceneTypes.Exit)
			};
			this.sizes = new Vector2[]
			{
				START_SIZE,
				CREDIT_SIZE,
				EXIT_SIZE
			};
			for(int i=0; i<buttons.Length; i++)
			{
				Vector2 center = (GameConstants.SCREEN_SIZE - sizes[i]) / 2;
				center.Y += ((i + 1) * 90);
				buttons[i].Size = sizes[i];
				buttons[i].Position = center;
			}
			//パーティクルの生成
			this.particleList = Particle.Create();
		}

		public override void Update(GameTime gameTime)
		{
			//ボタンが押された
			Detector detector = Detector.GetInstance();
			if(Detector.GetInstance().IsDetect(HandleConstants.ENTER))
			{
				this.IsEnd = true;
				this.Next = (int)buttons[selectedIndex].SceneType;
				this.pushButton = true;
			}
			Array.ForEach(buttons, button => button.Update(gameTime));
			particleList.ForEach(particle => particle.Update(gameTime));
			Move();
		}

		private void Move()
		{
			//ボタンが押されたら移動できない
			if(pushButton)
			{
				return;
			}
			Detector detector = Detector.GetInstance();
			//選択項目の変更
			if(detector.IsDetect(Handle.UP))
			{
				this.selectedIndex--;
				this.titleRotate -= 10f;
			}
			else if(detector.IsDetect(Handle.DOWN))
			{
				this.selectedIndex++;
				this.titleRotate += 10f;
			}
			if(selectedIndex >= 3)
			{
				this.selectedIndex = 0;
			}
			else if(selectedIndex < 0)
			{
				this.selectedIndex = 2;
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Begin();
			renderer.Draw("Textures/Back/Black", Vector2.Zero, Color.White);
			renderer.Draw("Textures/Title", TITLE_POS, null, Color.White, MathHelper.ToRadians(titleRotate), TITLE_ORIGIN, new Vector2(1, 1), SpriteEffects.None, 1f);
			//renderer.Draw("Textures/Title", TITLE_POS, Color.White);
			for(int i=0; i<buttons.Length; i++)
			{
				Button button = buttons[i];
				button.Draw(gameTime, renderer);
			}
			particleList.ForEach(particle => particle.Draw(gameTime, renderer));
			renderer.DrawRectangle(buttons[selectedIndex].Bounds, Color.White);
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			this.pushButton = false;
			sound.PlayBGM("Sound/Song/Title");
		}

		public override void Hide()
		{
			//sound.StopBGM();
			base.Hide();
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
