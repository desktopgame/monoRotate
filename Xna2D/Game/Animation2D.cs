using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// アニメーション描画のためのクラス.
	/// </summary>
	public class Animation2D : ICloneable
	{
		/// <summary>
		/// セル一つあたりの横幅.
		/// </summary>
		public int CellWidth { private set; get; }

		/// <summary>
		/// セル一つあたりの縦幅.
		/// </summary>
		public int CellHeight { private set; get; }

		/// <summary>
		/// 最大の行数.
		/// </summary>
		public int RowMax { private set; get; }

		/// <summary>
		/// 最大の列数.
		/// </summary>
		public int ColumnMax { private set; get; }

		/// <summary>
		/// 現在の行位置.
		/// </summary>
		public int Row { private set; get; }

		/// <summary>
		/// 現在の列位置.
		/// </summary>
		public int Column { private set; get; }

		/// <summary>
		/// 現在のコマ.
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				int tmpRow = Math.Min(RowMax-1, Math.Max(Row, 0));
				int tmpCol = Math.Min(ColumnMax-1, Math.Max(Column, 0));
				Rectangle ret = new Rectangle();
				ret.X = tmpCol * CellWidth;
				ret.Y = tmpRow * CellHeight;
				ret.Width = CellWidth;
				ret.Height = CellHeight;
				return ret;
			}
		}

		/// <summary>
		/// アニメーションが完了したならtrue.
		/// </summary>
		public bool IsEnd
		{
			get
			{
				if(Row > RowMax)
				{
					return true;
				}
				return Row >= RowMax && Column >= ColumnMax;
			}
		}

		public Animation2D(int cellWidth, int cellHeight, int rowMax, int columnMax, int defaultRow, int defaultColumn)
		{
			this.CellWidth = cellWidth;
			this.CellHeight = cellHeight;
			this.RowMax = rowMax;
			this.ColumnMax = columnMax;
			this.Row = defaultRow;
			this.Column = defaultColumn;
		}

		public Animation2D(int cellWidth, int cellHeight, int rowMax, int columnMax)
			: this(cellWidth, cellHeight, rowMax, columnMax, 0, 0)
		{
		}

		/// <summary>
		/// 次のコマへ移動します.
		/// </summary>
		public void Update()
		{
			if(Column < ColumnMax)
			{
				this.Column++;
			} else
			{
				this.Row++;
				this.Column = 0;
			}
		//	Debug.WriteLine(Row + "/" + Column + "   " + RowMax + "/" + ColumnMax);
		}

		/// <summary>
		/// もうアニメーションが終了しているなら位置を戻します.
		/// </summary>
		public void Loop()
		{
			if(IsEnd)
			{
				this.Row = 0;
				this.Column = 0;
			}
		}

		public object Clone()
		{
			return new Animation2D(CellWidth, CellHeight, RowMax, ColumnMax);
		}
	}
}
