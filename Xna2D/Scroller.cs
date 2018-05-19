using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Game;

namespace Xna2D
{
	/// <summary>
	/// 強制スクロール.
	/// </summary>
	public class Scroller : GameObjectBase
	{
		/// <summary>
		/// １フレームに加速する量.
		/// </summary>
		public Vector2 Period { private set; get; }

		protected static readonly string KEY_PERIOD_X = "PeriodX";
		protected static readonly string KEY_PERIOD_Y = "PeriodY";

		public Scroller(string path) : base(path)
		{
			this.Width = 32;
			this.Height = 32;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			IPlayer player = elements.FindPlayer();
			player.X += Period.X;
			player.Y += Period.Y;
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
		}

		public override bool IsReadOnly(string key)
		{
			if(key == KEY_PERIOD_X ||
				key == KEY_PERIOD_Y)
			{
				return false;
			}
			return base.IsReadOnly(key);
		}

		public override void Write(Dictionary<string, string> d)
		{
			base.Write(d);
			d[KEY_PERIOD_X] = Period.X.ToString();
			d[KEY_PERIOD_Y] = Period.Y.ToString();
		}

		public override void Read(Dictionary<string, string> d)
		{
			base.Read(d);
			float px = d.ParseFloat(KEY_PERIOD_X);
			float py = d.ParseFloat(KEY_PERIOD_Y);
			this.Period = new Vector2(px, py);
		}

		protected override IGameObject NewInstance()
		{
			return new Scroller(Path);
		}
	}
}
