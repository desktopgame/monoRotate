using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Utilities;
using Xna2D.Contents;
using System.Diagnostics;

namespace Xna2D.Scenes
{
	/// <summary>
	/// フェードイン.
	/// </summary>
	public class FadeInScene : LayeredScene, ILayered
	{
		private Vector2 screenSize;
		private int horizontalCellDivide;
		private int verticalCellDivide;
		private FrameTimer timer;
		private List<Rectangle> rectangleList;

		private static readonly Random R = new Random();

		public FadeInScene(IScene delegatez, Vector2 screenSize, int horizontalCellDivide, int verticalCellDivide) : base(MaskType.Back, delegatez)
		{
			this.screenSize = screenSize;
			this.horizontalCellDivide = horizontalCellDivide;
			this.verticalCellDivide = verticalCellDivide;
			this.timer = new FrameTimer(1);
			this.rectangleList = new List<Rectangle>();
			Fill();
		}

		public FadeInScene(IScene delegatez, Vector2 screenSize)
			: this(delegatez, screenSize, 10, 10)
		{
		}

		private void Fill()
		{
			int width = (int)(screenSize.X / horizontalCellDivide);
			int height = (int)(screenSize.Y / verticalCellDivide);
			rectangleList.Clear();
			for(int i=0; i<verticalCellDivide; i++)
			{
				for(int j=0; j<horizontalCellDivide; j++)
				{
					Rectangle rect = new Rectangle(j * width, i * height, width, height);
					rectangleList.Add(rect);
				}
			}
		}

		protected override void UpdateLayer(GameTime gameTime)
		{
			if(timer.Update().Elapsed())
			{
				rectangleList.RemoveAt(R.Next(0, rectangleList.Count));
				if(rectangleList.Count > 0)
				{
				rectangleList.RemoveAt(R.Next(0, rectangleList.Count));
				}
			}
			if(rectangleList.Count == 0)
			{
				this.layerEnd = true;
			}
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
			Fill();
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
