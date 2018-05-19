using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Game;
using Xna2D.Views;
using Xna2D.Contents;
using System.Diagnostics;

namespace TestGame.Scenes.Title
{
	/// <summary>
	/// 4*4 の大きさを持つパーティクル.
	/// </summary>
	public class Particle : View
	{
		public Color Color
		{
			set; get;
		}

		public Vector2 Period
		{
			set; get;
		}
		private static readonly Random R = new Random();

		public Particle(Color color)
		{
			this.Color = color;
			this.Period = new Vector2(
				R.Next(-4, 4),
				R.Next(-4, 4)
			);
			this.Size = new Vector2(4, 4);
		//	Debug.WriteLine(Period.X + "/" + Period.Y);
		}

		public Particle(Color color, Vector2 period)
		{
			this.Color = color;
			this.Period = period;
			this.Size = new Vector2(4, 4);
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			this.Position += Period;
			if(GetX() >= GameConstants.SCREEN_WIDTH)
			{
				SetX(0f);
			} else if(GetX() + GetWidth() < 0)
			{
				SetX(GameConstants.SCREEN_WIDTH - GetWidth());
			}
			if(GetY() >= GameConstants.SCREEN_HEIGHT)
			{
				SetY(0f);
			} else if(GetY() + GetHeight() < 0)
			{
				SetY(GameConstants.SCREEN_HEIGHT - GetHeight());
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.FillRectangle(Bounds, Color);
		}

		/// <summary>
		/// なんかいい感じのパーティクル群を作成.
		/// </summary>
		/// <returns></returns>
		public static List<Particle> Create()
		{
			List<Particle> list = new List<Particle>();
			Vector2 prev = new Vector2(4, 4);
			float max = 4;
			float min = -4;
			for(int i = 0; i < 200; i++)
			{
				//0, -3[0]
				//1, -2[1]
				//2, -1[2]
				//3, 0[3]
				//4, 1[4]
				Vector2 newV = prev;
				newV.X = (newV.X + newV.Y) + (i / 10);
				newV.Y++;
				//X方向の速さを制限
				if(newV.X > max)
				{
					newV.X = min;
				}
				else if(newV.X < min)
				{
					newV.X = max;
				}
				//Y方向の速さを制限
				if(newV.Y > max)
				{
					newV.Y = min;
				}
				else if(newV.Y < min)
				{
					newV.Y = max;
				}
				list.Add(new Particle(Color.Gray * 0.8f, newV));
				prev = newV;
			}
			return list;
		}
	}
}
