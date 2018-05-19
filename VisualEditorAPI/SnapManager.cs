using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualEditorAPI.Geom;

namespace VisualEditorAPI
{
	/// <summary>
	/// スナップされている線分とその対象を管理するクラス.
	/// </summary>
	public class SnapManager
	{
		/// <summary>
		/// スナップ許容範囲.
		/// </summary>
		public int Area
		{
			set; get;
		} = 4;

		/// <summary>
		/// スナップ対象.
		/// </summary>
		public VisualContent[] Targets
		{
			get { return snapTargetList.ToArray(); }
		}

		/// <summary>
		/// スナップしている線分の一覧.
		/// </summary>
		public Line[] Lines
		{
			get { return snapLineList.ToArray(); }
		}

		/// <summary>
		/// スナップ中ならtrue;
		/// </summary>
		public bool Now
		{
			get { return snapLineList.Count > 0; }
		}

		private List<Line> snapLineList;
		private List<VisualContent> snapTargetList;

		public SnapManager()
		{
			this.snapLineList = new List<Line>();
			this.snapTargetList = new List<VisualContent>();
		}

		/// <summary>
		/// 指定のコンテンツに対してスナップ出来るなら線分を記録してtrueを返します.
		/// </summary>
		/// <param name="content"></param>
		/// <param name="mouseX"></param>
		/// <param name="mouseY"></param>
		/// <param name="controlWidth"></param>
		/// <param name="controlHeight"></param>
		/// <returns></returns>
		public bool Snap(VisualContent content, ref int mouseX, ref int mouseY, int controlWidth, int controlHeight)
		{
			snapLineList.Clear();
			int left = content.Left;
			int right = content.Right;
			int top = content.Top;
			int bottom = content.Bottom;
			int diffLeft = Math.Abs(left - mouseX);
			int diffRight = Math.Abs(right - mouseX);
			int diffTop = Math.Abs(top - mouseY);
			int diffBottom = Math.Abs(bottom - mouseY);
			//横方向のスナップ
			if(diffLeft < Area)
			{
				snapLineList.Add(new Line(
					new Point(left, 0),
					new Point(left, controlHeight)
				));
				mouseX = left;
			} else if(diffRight < Area)
			{
				snapLineList.Add(new Line(
					new Point(right, 0),
					new Point(right, controlHeight)
				));
				mouseX = right;
			}
			//縦方向のスナップ
			if(diffTop < Area)
			{
				snapLineList.Add(new Line(
					new Point(0, top),
					new Point(controlWidth, top)
				));
				mouseY = top;
			}
			else if(diffBottom < Area)
			{
				snapLineList.Add(new Line(
					new Point(0, bottom),
					new Point(controlWidth, bottom)
				));
				mouseY = bottom;
			}
			bool ret = 
				diffLeft < Area ||
				diffRight < Area ||
				diffTop < Area ||
				diffBottom < Area;
			if(ret)
			{
				this.snapTargetList.Add(content);
			}
			return ret;
		}

		/// <summary>
		/// 全てのスナップを削除します.
		/// </summary>
		public void Clear()
		{
			snapLineList.Clear();
			snapTargetList.Clear();
		}
	}
}
