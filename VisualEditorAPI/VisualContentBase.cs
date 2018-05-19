using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// VisualContentの基底クラスです.
	/// </summary>
	public abstract class VisualContentBase : VisualContent
	{
		public event EventHandler<VisualContentShapeEventArgs> OnInvalidate;

		public bool IsSelected
		{
			set; get;
		}
		
		public int X
		{
			set
			{
				Rectangle old = Bounds;
				this._x = value;
				OnInvalidate?.Invoke(this, new VisualContentShapeEventArgs(old, Bounds));
			}
			get { return _x; }
		}
		private int _x;

		public int Y
		{
			set
			{
				Rectangle old = Bounds;
				this._y = value;
				OnInvalidate?.Invoke(this, new VisualContentShapeEventArgs(old, Bounds));
			}
			get { return _y; }
		}
		private int _y;

		public int Width
		{
			set
			{
				Rectangle old = Bounds;
				this._width = value;
				OnInvalidate?.Invoke(this, new VisualContentShapeEventArgs(old, Bounds));
			}
			get { return _width; }
		}
		private int _width;

		public int Height
		{
			set
			{
				Rectangle old = Bounds;
				this._height = value;
				OnInvalidate?.Invoke(this, new VisualContentShapeEventArgs(old, Bounds));
			}
			get { return _height; }
		}
		private int _height;
		
		public int Left
		{
			get { return X; }
		}

		public int Right
		{
			get { return X + Width; }
		}

		public int Top
		{
			get { return Y; }
		}

		public int Bottom
		{
			get { return Y + Height; }
		}

		public Point Location
		{
			get { return new Point(X, Y); }
		}

		public Size Size
		{
			get { return new Size(Width, Height); }
		}

		public Rectangle Bounds
		{
			get { return new Rectangle(X, Y, Width, Height); }
		}


		public abstract void Draw(Graphics g);

		public abstract object Clone();
	}
}
