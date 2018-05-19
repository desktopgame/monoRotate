using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game.Blocks
{
	[Compatibility]
	public class MoveBlock : Block, ICollisionCallback
	{
		private Vector2 period;
		private Vector2 offset;
		private Vector2 length;

		protected static readonly string KEY_PERIOD_X = "PeriodX";
		protected static readonly string KEY_PERIOD_Y = "PeriodY";
		protected static readonly string KEY_LENGTH_X = "LengthX";
		protected static readonly string KEY_LENGTH_Y = "LengthY";

		public MoveBlock(string path, Vector2 period, Vector2 length) : base(path)
		{
			this.period = period;
			this.length = length;
		}

		public MoveBlock(string path, float width, float height, Vector2 period, Vector2 length) : base(path, width, height)
		{
			this.period = period;
			this.length = length;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			base.Update(gameTime, elements);
			//動く
			this.offset += period;
			this.X += period.X;
			this.Y += period.Y;
			//ついた
			if(offset == length)
			{
				period *= -1;
				length *= -1;
				this.offset = Vector2.Zero;
			}
			IEnumerator<IGameObject> objects= elements.GetEnumerator();
			while(objects.MoveNext())
			{
				IGameObject obj = objects.Current;
				if(obj is Block || obj is Background)
				{
					continue;
				}
				//衝突している
				if(!obj.Bounds.Intersects(Bounds))
				{
					continue;
				}
				//右に向かっている
				if(period.X > 0 && Math.Abs((Left - obj.Right)) > 3)
				{
					obj.X = X + Width;
				//左に向かっている
				}
				else if(period.X < 0 && Math.Abs((Right - obj.Left)) > 3)
				{
					obj.X = X - obj.Width;
				}
				//下に向かっている
				if(period.Y > 0 && Math.Abs((Top - obj.Bottom)) > 3)
				{
					obj.Y = Bottom;
					/*
					float diff = CenterX - obj.CenterX;
					if(diff > 0)
					{
						obj.X = X - obj.Width;
					}
					else
					{
						obj.X = X + Width;
					}
					//*/
				}
			}
		}

		public override void Write(Dictionary<string, string> d)
		{
			base.Write(d);
			d[KEY_PERIOD_X] = period.X.ToString();
			d[KEY_PERIOD_Y] = period.Y.ToString();
			d[KEY_LENGTH_X] = length.X.ToString();
			d[KEY_LENGTH_Y] = length.Y.ToString();
		}

		public override void Read(Dictionary<string, string> d)
		{
			base.Read(d);
			this.period.X = d.ParseFloat(KEY_PERIOD_X);
			this.period.Y = d.ParseFloat(KEY_PERIOD_Y);
			this.length.X = d.ParseFloat(KEY_LENGTH_X);
			this.length.Y = d.ParseFloat(KEY_LENGTH_Y);
		}

		public override bool IsReadOnly(string key)
		{
			if(key == KEY_PERIOD_X ||
			   key == KEY_PERIOD_Y ||
			   key == KEY_LENGTH_X ||
			   key == KEY_LENGTH_Y)
			{
				return false;
			}
			return base.IsReadOnly(key);
		}

		protected override IGameObject NewInstance()
		{
			MoveBlock ret = new MoveBlock(Path, Width, Height, period, length);
			ret.offset = offset;
			ret.length = length;
			ret.period = period;
			return ret;
		}

		public void Collision(ICollider collider, Direction dir)
		{
			
		}
	}
}
