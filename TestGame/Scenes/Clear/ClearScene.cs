using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;
using Xna2D.Game;
using Xna2D.Input;
using TestGame.Scenes.Pause;
using TestGame.Scenes.Select;
using System.IO;
using Xna2D.Utilities;

namespace TestGame.Scenes.Clear
{
	/// <summary>
	/// クリア画面.
	/// </summary>
	public class ClearScene : SceneBase
	{
		private IScene playScene;
		private StageSelector stageSelector;
	//	private Rectangle rect;
		private List<Rectangle> rectList;
		private int selectedIndex;
		private FrameTimer rectTimer;
		private float alpha;
		private Sound sound;

		private static readonly Vector2 CLEAR_SIZE = new Vector2(425, 50);
		private static readonly Vector2 NEXT_SIZE = new Vector2(308, 40);
		private static readonly Vector2 SELECT_SIZE = new Vector2(430, 50);

		private static readonly Vector2 CLEAR_POS = (GameConstants.SCREEN_SIZE - CLEAR_SIZE) / 2;
		private static readonly Vector2 NEXT_POS = Vector2.Zero;
		private static readonly Vector2 SELECT_POS = Vector2.Zero;

		private static readonly int SPLIT_ROWS = 10;
		private static readonly int SPLIT_COLS = 10;
		private static readonly int WIDTH = GameConstants.SCREEN_WIDTH / SPLIT_COLS;
		private static readonly int HEIGHT = GameConstants.SCREEN_HEIGHT / SPLIT_ROWS;
		private static readonly Random RANDOM = new Random();

		static ClearScene()
		{
			CLEAR_POS.Y -= 100;
			float baseY = (GameConstants.SCREEN_HEIGHT - 50) / 2;
			float sumWidth = NEXT_SIZE.X + SELECT_SIZE.X;
			float margin = (GameConstants.SCREEN_WIDTH - sumWidth) / (2 + 1);
			float x = margin;
			Vector2[] layout = new Vector2[2];
			for(int i = 0; i < 2; i++)
			{
				layout[i] = new Vector2(x + -60, baseY);
				x += 400;
				x += margin;
			}
			NEXT_POS = layout[0];
			SELECT_POS = layout[1];
		}

		public ClearScene(IScene playScene, StageSelector stageSelector, Sound sound)
		{
			this.playScene = playScene;
			this.stageSelector = stageSelector;
		//	this.rect = new Rectangle(0, 0, 0, GameConstants.SCREEN_HEIGHT);
			this.rectList = new List<Rectangle>();
			this.rectTimer = new FrameTimer(2);
			this.sound = sound;
		}

		public override void Update(GameTime gameTime)
		{
			//画面を塗りつぶす
			if(!CompleteFillBG())
			{
				UpdateRect();
				return;
			}
			//文字を浮き上がらせる
			if(alpha < 1f)
			{
				this.alpha += 0.01f;
			}
			//選択項目の移動
			Detector detector = Detector.GetInstance();
			if(detector.IsDetect(Handle.LEFT))
			{
				this.selectedIndex--;
			} else if(detector.IsDetect(Handle.RIGHT))
			{
				this.selectedIndex++;
			}
			if(selectedIndex >= 2)
			{
				this.selectedIndex = 0;
			} else if(selectedIndex < 0)
			{
				this.selectedIndex = 1;
			}
			if(alpha >= 1f && detector.IsDetect(SelectScene.ENTER))
			{
				this.IsEnd = true;
				this.Next = (int)(selectedIndex == 0 ? SceneTypes.Play : SceneTypes.Select);
				stageSelector.Index++;
				if(!File.Exists(stageSelector.Path))
				{
					this.Next = (int)SceneTypes.Select;
				}
			}
		}

		private void UpdateRect()
		{
			/*
			if(!rectTimer.Update().Elapsed())
			{
				return;
			}
			//*/
			Rectangle rect = Rectangle.Empty;
			rect.Width = WIDTH;
			rect.Height = HEIGHT;
			do
			{
				int row = RANDOM.Next(0, SPLIT_ROWS);
				int col = RANDOM.Next(0, SPLIT_COLS);
				rect.X = col * WIDTH;
				rect.Y = row * HEIGHT;
			} while(rectList.Contains(rect));
			rectList.Add(rect);
		}

		private bool CompleteFillBG()
		{
			return rectList.Count >= (SPLIT_ROWS * SPLIT_COLS);
//			return rect.Width > GameConstants.SCREEN_WIDTH;
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			playScene.Draw(gameTime, renderer);
			renderer.Begin();
			rectList.ForEach(rect =>
			{
				renderer.FillRectangle(rect, Color.Gray * 0.8f);
			});
			//renderer.FillRectangle(rect, Color.Gray * 0.8f);
			DrawIcons(renderer);
			renderer.End();
		}

		private void DrawIcons(Renderer renderer)
		{
			if(!CompleteFillBG())
			{
				return;
			}
			renderer.Draw("Textures/StageClear", CLEAR_POS, Color.White * alpha);
			if(alpha < 1f)
			{
				renderer.Draw("Textures/NextStage", NEXT_POS, Color.White * alpha);
				renderer.Draw("Textures/StageSelectIcon", SELECT_POS, Color.White * alpha);
			}
			else
			{
				renderer.Draw("Textures/NextStage", NEXT_POS, Color.White * (selectedIndex == 0 ? 1f : 0.5f));
				renderer.Draw("Textures/StageSelectIcon", SELECT_POS, Color.White * (selectedIndex == 1 ? 1f : 0.5f));
			}
		}

		public override void Show()
		{
			base.Show();
			this.alpha = 0f;
		//	this.rect.Width = 0;
			rectList.Clear();
			rectTimer.Clear();
			sound.PlayBGM("Sound/Song/Result");
		}

		public override void Hide() {
			base.Hide();
			sound.StopBGM();
		}
	}
}
