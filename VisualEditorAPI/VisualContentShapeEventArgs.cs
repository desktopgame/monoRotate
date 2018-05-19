using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// コンテンツの変形を通知するイベントです.
	/// </summary>
	public class VisualContentShapeEventArgs : EventArgs
	{
		/// <summary>
		/// 変形前の領域.
		/// </summary>
		public Rectangle OldBounds { private set; get; }

		/// <summary>
		/// 変形後の領域.
		/// </summary>
		public Rectangle NewBounds { private set; get; }

		public VisualContentShapeEventArgs(Rectangle oldBounds, Rectangle newBounds)
		{
			this.OldBounds = oldBounds;
			this.NewBounds = NewBounds;
		}
	}
}
