using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Views
{
	/// <summary>
	/// 何かしらを表示するビュー.
	/// </summary>
	public class View
	{
		/// <summary>
		/// 位置.
		/// </summary>
		public virtual Vector2 Position
		{
			set; get;
		}

		/// <summary>
		/// 大きさ.
		/// </summary>
		public virtual Vector2 Size
		{
			set; get;
		}
		
		/// <summary>
		/// 範囲.
		/// </summary>
		public Rectangle Bounds
		{
			get { return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y); }
		}

		/// <summary>
		/// このビューを包含するビュー.
		/// </summary>
		public ViewGroup Parent
		{
			internal set; get;
		}

		/// <summary>
		/// ビューを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{
		}

		/// <summary>
		/// ビューを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		public virtual void Draw(GameTime gameTime, Renderer renderer)
		{
		}
		
		public void SetX(float x)
		{
			this.Position = new Vector2(x, Position.Y);
		}

		public float GetX()
		{
			return Position.X;
		}

		public void SetY(float y)
		{
			this.Position = new Vector2(Position.X, y);
		}
		
		public float GetY()
		{
			return Position.Y;
		}

		public void SetWidth(float w)
		{
			this.Size = new Vector2(w, Size.Y);
		}

		public float GetWidth()
		{
			return Size.X;
		}

		public void SetHeight(float h)
		{
			this.Size = new Vector2(Size.X, h);
		}

		public float GetHeight()
		{
			return Size.Y;
		}
	}
}
