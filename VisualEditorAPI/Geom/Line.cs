using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI.Geom
{
	/// <summary>
	/// 始点と終点を表すオブジェクト.
	/// </summary>
	public class Line
	{
		public Point P1 { set; get; }

		public int X1 { get { return P1.X; } }

		public int Y1 { get { return P1.Y; } }

		public Point P2 { set; get; }

		public int X2 { get { return P2.X; } }

		public int Y2 { get { return P2.Y; } }

		public Line(Point p1, Point p2)
		{
			this.P1 = p1;
			this.P2 = p2;
		}
	}
}
