using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Game
{
	/// <summary>
	/// 流れる背景.
	/// </summary>
	public class FlowBackground : Background
	{
		private Vector2 period;

		protected static readonly string PERIOD_X = "PeriodX";
		protected static readonly string PERIOD_Y = "PeriodY";

		public FlowBackground(string path, float width, float height) : base(path, width, height)
		{
			this.CanRotate = true;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			base.Update(gameTime, elements);
			this.X += period.X;
			this.Y += period.Y;
			Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
			Vector2 scrollArea = camera.RotateScrollArea;
			if(Top >= scrollArea.Y)
			{
				this.Y = 0;
			} else if(Bottom < 0)
			{
				this.Y = scrollArea.Y;
			}
			if(Left >= scrollArea.X)
			{
				this.X = 0;
			} else if(Right < 0)
			{
				this.X = scrollArea.X;
			}
		}

		public override bool IsReadOnly(string key)
		{
			if(key == PERIOD_X ||
			   key == PERIOD_Y)
			{
				return false;
			}
			return base.IsReadOnly(key);
		}

		public override void Write(Dictionary<string, string> d)
		{
			base.Write(d);
			d[PERIOD_X] = period.X.ToString();
			d[PERIOD_Y] = period.Y.ToString();
		}

		public override void Read(Dictionary<string, string> d)
		{
			base.Read(d);
			this.period.X = d.ParseFloat(PERIOD_X);
			this.period.Y = d.ParseFloat(PERIOD_Y);
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			base.Draw(gameTime, renderer, elements);
		}

		protected override IGameObject NewInstance()
		{
			return new FlowBackground(Path, Width, Height);
		}
	}
}
