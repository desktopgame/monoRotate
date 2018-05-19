using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;
using Xna2D.Game;
using Xna2D.Input;

namespace TestGame.Scenes.Pause
{
	/// <summary>
	/// ポーズ画面.
	/// </summary>
	public class PauseScene : SceneBase
	{
		private IScene playScene;
		private StageSelector stageSelector;
		private int selectedIndex;
		private Sound sound;

		private static readonly Vector2 BACK_SIZE = new Vector2(160, 50);
		private static readonly Vector2 RETRY_SIZE = new Vector2(200, 50);
		private static readonly Vector2 SELECT_SIZE = new Vector2(430, 50);
		
		private static readonly Vector2 BACK_POS = Vector2.Zero;
		private static readonly Vector2 RETRY_POS = Vector2.Zero;
		private static readonly Vector2 SELECT_POS = Vector2.Zero;

		private static readonly int BACK_INDEX = 0;
		private static readonly int RETRY_INDEX = 1;
		private static readonly int SELECT_INDEX = 2;

		private static readonly SceneTypes[] TYPES = new SceneTypes[] { SceneTypes.Play, SceneTypes.Play, SceneTypes.Select};

		static PauseScene()
		{
			float baseY = (GameConstants.SCREEN_HEIGHT - 50) / 2;
			float sumWidth = BACK_SIZE.X + RETRY_SIZE.X + SELECT_SIZE.X;
			float margin = (GameConstants.SCREEN_WIDTH - sumWidth) / (3 + 1);
			float x = margin;
			Vector2[] layout = new Vector2[3];
			for(int i = 0; i < 3; i++)
			{
				layout[i] = new Vector2(x + -60, baseY);
				x += 190;
				x += margin;
			}
			BACK_POS = layout[0];
			RETRY_POS = layout[1];
			SELECT_POS = layout[2];
		}

		public PauseScene(IScene playScene, StageSelector stageSelector, Sound sound)
		{
			this.playScene = playScene;
			this.stageSelector = stageSelector;
			this.Next = (int)SceneTypes.Play;
			this.sound = sound;
		}

		public override void Update(GameTime gameTime)
		{
			Detector detector = Detector.GetInstance();
			if(detector.IsDetect(Handle.LEFT))
			{
				this.selectedIndex--;
			} else if(detector.IsDetect(Handle.RIGHT))
			{
				this.selectedIndex++;
			}
			if(selectedIndex >= 3)
			{
				this.selectedIndex = 0;
			} else if(selectedIndex < 0)
			{
				this.selectedIndex = 2;
			}
			if(detector.IsDetect(Select.SelectScene.ENTER))
			{
				this.IsEnd = true;
				this.Next = (int)TYPES[selectedIndex];
				if(selectedIndex == BACK_INDEX)
				{
					this.stageSelector.Option = StageSelector.Options.Continue;
				}
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			playScene.Draw(gameTime, renderer);
			renderer.Begin();
			renderer.FillRectangle(new Rectangle(0, 0, GameConstants.SCREEN_WIDTH, GameConstants.SCREEN_HEIGHT), Color.White * 0.5f);
			renderer.Draw("Textures/Back", BACK_POS, Color.White * (selectedIndex == 0 ? 1f : 0.5f));
			renderer.Draw("Textures/Retry", RETRY_POS, Color.White * (selectedIndex == 1 ? 1f : 0.5f));
			renderer.Draw("Textures/StageSelectIcon", SELECT_POS, Color.White * (selectedIndex == 2 ? 1f : 0.5f));
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			this.selectedIndex = 0;
		}

		public override void Hide() {
			base.Hide();
			if(this.Next == (int)SceneTypes.Play) {
				sound.ResumeBGM();
			}
		}
	}
}
