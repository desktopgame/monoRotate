using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Utilities;
using Xna2D.Contents;

namespace Xna2D.Scenes
{
	/// <summary>
	/// フェードアウト.
	/// </summary>
	public class FadeOutScene : LayeredScene, ILayered
	{
		private Vector2 screenSize;
		private int horizontalCellDivide;
		private int verticalCellDivide;
		private FrameTimer periodTimer;
		private FrameTimer waitTimer;
		private List<Rectangle> rectangleList;

		private static readonly Random R = new Random();

		public FadeOutScene(IScene delegatez, Vector2 screenSize, int horizontalCellDivide, int verticalCellDivide) : base(MaskType.Front, delegatez)
		{
			this.screenSize = screenSize;
			this.horizontalCellDivide = horizontalCellDivide;
			this.verticalCellDivide = verticalCellDivide;
			this.periodTimer = new FrameTimer(1);
			this.waitTimer = new FrameTimer(15);
			this.rectangleList = new List<Rectangle>();
		}

		public FadeOutScene(IScene delegatez, Vector2 screenSize) 
			: this(delegatez, screenSize, 10, 10)
		{
		}

		protected override void UpdateLayer(GameTime game)
		{
			FillProgress();
			if(rectangleList.Count == (horizontalCellDivide * verticalCellDivide))
			{
				if(waitTimer.Update().Elapsed())
				{
					this.layerEnd = true;
				}
			}
		}

		private void FillProgress()
		{
			if(rectangleList.Count == (horizontalCellDivide * verticalCellDivide))
			{
				return;
			}
			Rectangle rect = Rectangle.Empty;
			int width = (int)screenSize.X / horizontalCellDivide;
			int height = (int)screenSize.Y / verticalCellDivide;
			do
			{
				int row = R.Next(0, verticalCellDivide);
				int col = R.Next(0, horizontalCellDivide);
				rect.X = col * width;
				rect.Y = row * height;
				rect.Width = width;
				rect.Height = height;
			} while(rectangleList.Contains(rect));
			rectangleList.Add(rect);
		}

		protected override void DrawLayer(GameTime gameTime, Renderer renderer)
		{
			DrawDelegate(gameTime, renderer);
			renderer.Begin();
			rectangleList.ForEach(rect =>
			{
				renderer.FillRectangle(rect, Color.Gray * 0.5f);
			});
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			periodTimer.Clear();
			waitTimer.Clear();
		}

		public bool IsNeedBackLayer(LayeredScene scene)
		{
			if(delegatez is ILayered)
			{
				return ((ILayered)delegatez).IsNeedBackLayer(scene);
			}
			return true;
		}

		public bool IsNeedFrontLayer(LayeredScene scene)
		{
			if(delegatez is ILayered)
			{
				return ((ILayered)delegatez).IsNeedFrontLayer(scene);
			}
			return true;
		}
	}
}
