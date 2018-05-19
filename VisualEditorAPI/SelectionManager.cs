using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// 二つの点から矩形範囲を計算するクラス.
	/// </summary>
	public class SelectionManager
	{
		/// <summary>
		/// 選択中の領域が変更されると呼ばれます.
		/// </summary>
		public event EventHandler OnBoundsUpdate;

		/// <summary>
		/// 開始位置.
		/// </summary>
		public Point StartPoint {
			set
			{
				this._startPoint = value;
				this._endPoint = value;
				OnBoundsUpdate?.Invoke(this, EventArgs.Empty);
			}
			get { return _startPoint; }
		}
		private Point _startPoint;

		/// <summary>
		/// 終了位置.
		/// </summary>
		public Point EndPoint {
			set
			{
				this._endPoint = value;
				OnBoundsUpdate?.Invoke(this, EventArgs.Empty);
			}
			get { return _endPoint; }
		}
		private Point _endPoint;

		/// <summary>
		/// 範囲.
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				Rectangle tmp = new Rectangle();
				tmp.X = Math.Min(StartPoint.X, EndPoint.X);
				tmp.Y = Math.Min(StartPoint.Y, EndPoint.Y);
				tmp.Width = Math.Max(StartPoint.X, EndPoint.X) - tmp.X;
				tmp.Height = Math.Max(StartPoint.Y, EndPoint.Y) - tmp.Y;
				return tmp;
			}
		}

		public SelectionManager()
		{
		}
		
		/// <summary>
		/// 選択を解除.
		/// </summary>
		public void Clear()
		{
			this.StartPoint = Point.Empty;
			this.EndPoint = Point.Empty;
		}
	}
}
