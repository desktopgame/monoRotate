using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities.Undo
{
	/// <summary>
	/// 変更可能なオブジェクトです.
	/// </summary>
	public interface Editable
	{
		/// <summary>
		/// このオブジェクトで行われた取り消し可能な変更を監視するリスナーのリストです.
		/// </summary>
		event UndoableEditHandler OnEdit;

		/// <summary>
		/// この呼び出しの後に続く変更を一つの変更にまとめて記録する場合に、
		/// 変更の前に呼び出せます。
		/// try {
		///		editable.Begin();
		///		editable.something();
		///		editable.something2();
		/// } finally {
		///		editable.End();
		/// }
		/// </summary>
		void Begin();

		/// <summary>
		/// この呼び出しの前に続いた変更を一つの変更としてまとめて通知します.
		/// </summary>
		void End();
	}
}
