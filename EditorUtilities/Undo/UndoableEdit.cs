using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities.Undo
{
	/// <summary>
	/// 取り消し可能な変更です.
	/// </summary>
	public interface UndoableEdit
	{
		/// <summary>
		/// 変更を取り消します.
		/// </summary>
		void Undo();

		/// <summary>
		/// 変更を実行します.
		/// </summary>
		void Redo();
	}
}
