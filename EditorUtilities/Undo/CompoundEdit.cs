using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities.Undo
{
	/// <summary>
	/// 複数の変更を一つの変更として記録する実装です.
	/// 例えば世代の更新など複数のセルが大規模に更新される場合に使用されます。
	/// </summary>
	public class CompoundEdit : UndoableEdit
	{
		private List<UndoableEdit> edits;

		public CompoundEdit()
		{
			this.edits = new List<UndoableEdit>();
		}

		/// <summary>
		/// 指定の変更をこの変更に含めます.
		/// (Undo/Redo時に一緒に実行される。)
		/// </summary>
		/// <param name="edit"></param>
		/// <returns></returns>
		public CompoundEdit AddEdit(UndoableEdit edit)
		{
			edits.Add(edit);
			return this;
		}

		public void Redo()
		{
			edits.ForEach(elem => elem.Redo());
		}

		public void Undo()
		{
			for(int i=edits.Count - 1; i>=0; i--)
			{
				edits[i].Undo();
			}
		}
	}
}
