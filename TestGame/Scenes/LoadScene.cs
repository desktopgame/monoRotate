using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Contents.Loaders;
using Xna2D.Scenes;
using Xna2D.Utilities;
using System.Diagnostics;
using Xna2D.Game;

namespace TestGame.Scenes
{
	/// <summary>
	/// コンテンツ読み込みクラス.
	/// </summary>
	public class LoadScene : SceneBase
	{
		private ContentIterator contentIterator;
		private FrameTimer loadRate;
		private BlinkTimer blinkTimer;
		private bool complete;
		
		public LoadScene(ContentIterator loaderIterator)
		{
			this.contentIterator = loaderIterator;
			this.loadRate = new FrameTimer(5);
			this.blinkTimer = new BlinkTimer();
			this.Next = (int)SceneTypes.Title;
			this.complete = false;
			loaderIterator.Initialize();
		}

		public override void Update(GameTime gameTime)
		{
			//読み込みが完了した
			if(complete)
			{
				//点滅させる
				blinkTimer.Update();
				//3回点滅したら終了
				if(blinkTimer.Count == 5)
				{
					this.IsEnd = true;
				}
				return;
			}
			//読み込みが完了した
			if(!contentIterator.HasNext)
			{
				this.complete = true;
				return;
			}
			//まだ完了していない
			//if(loadRate.Update().Elapsed())
			//{
				contentIterator.Next();
			//}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Begin();
			float width = 864;
			float progressAlpha = blinkTimer.GetAlpha();
			Vector2 numCenter = (GameConstants.SCREEN_SIZE - new Vector2(30, 40)) / 2;
			Vector2 boxCenter = (GameConstants.SCREEN_SIZE - new Vector2(width, 100)) / 2;
			boxCenter.Y = numCenter.Y + 20f;
			renderer.Draw("Textures/Back/Black", Vector2.Zero, Color.White);
			renderer.DrawNumber("Textures/NumberWhite30", numCenter, Color.White, contentIterator.Parcent, Resource.NUMBER_RECTANGLES);
			Rectangle box = new Rectangle((int)boxCenter.X, (int)boxCenter.Y + 20, (int)width, 100);
			Rectangle progress = box;
			progress.Width = (int)((width / (float)contentIterator.Length) * contentIterator.Offset);
			renderer.DrawRectangle(box, Color.White);
			renderer.FillRectangle(progress, Color.Gray * progressAlpha);
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			blinkTimer.Clear();
		}
	}
}
