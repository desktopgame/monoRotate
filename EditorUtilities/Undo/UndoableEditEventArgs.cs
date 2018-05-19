using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities.Undo
{
	/// <summary>
	/// 取り消し可能な変更の実行を監視するリスナーです.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void UndoableEditHandler(object sender, UndoableEditEventArgs e);

	/// <summary>
	/// 取り消し可能な変更が実行されたことを通知するイベントです.
	/// </summary>
	public class UndoableEditEventArgs : EventArgs
	{
		/// <summary>
		/// 既に実行された変更.
		/// </summary>
		public UndoableEdit Edit
		{
			private set; get;
		}

		public UndoableEditEventArgs(UndoableEdit edit)
		{
			this.Edit = edit;
		}
	}
}
