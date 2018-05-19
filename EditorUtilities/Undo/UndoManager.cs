using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities.Undo
{
	/// <summary>
	/// 変更を記録して元に戻すためのスタックです.
	/// </summary>
	public class UndoManager : UndoableEdit, Editable
	{
		
		/// <summary>
		/// このインスタンスの持つ変更履歴の変更を監視します.
		/// </summary>
		public event UndoableEditHandler OnEdit;

		/// <summary>
		/// 取り消しを実行できるならtrue.
		/// </summary>
		public bool CanUndo
		{
			get { return undoStuck.Count > 0; }
		}

		/// <summary>
		/// 実行できるならtrue.
		/// </summary>
		public bool CanRedo
		{
			get { return redoStuck.Count > 0; }
		}

		/// <summary>
		/// 変更を記録しないようマークされているならtrue.
		/// </summary>
		public bool IsProgress
		{
			get { return mark > 0; }
		}

		protected Stack<UndoableEdit> undoStuck;
		protected Stack<UndoableEdit> redoStuck;
		private int mark;

		public UndoManager()
		{
			this.undoStuck = new Stack<UndoableEdit>();
			this.redoStuck = new Stack<UndoableEdit>();
			this.mark = 0;
		}

		/// <summary>
		/// 指定の変更可能なオブジェクトが発行するイベントを傍受して変更を記録します.
		/// </summary>
		/// <param name="editable"></param>
		public void Install(Editable editable)
		{
			editable.OnEdit += Handle;
		}

		/// <summary>
		/// 指定の変更可能なオブジェクトが発行するイベントの傍受を終了します.
		/// </summary>
		/// <param name="editable"></param>
		public void Uninstall(Editable editable)
		{
			editable.OnEdit -= Handle;
		}

		/// <summary>
		/// イベントを受け取ります.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Handle(object sender, UndoableEditEventArgs e)
		{
			if(IsProgress)
			{
				return;
			}
			undoStuck.Push(e.Edit);
			OnEdit?.Invoke(this, new UndoableEditEventArgs(this));
		}

		/// <summary>
		/// 変更を実行して記録します.
		/// </summary>
		/// <param name="undoableEdit"></param>
		public void Commit(UndoableEdit undoableEdit)
		{
			if(IsProgress)
			{
				return;
			}
			undoableEdit.Redo();
			undoStuck.Push(undoableEdit);
			OnEdit?.Invoke(this, new UndoableEditEventArgs(this));
		}

		/// <summary>
		/// 最後に実行された変更を取り消します.
		/// </summary>
		public void Undo()
		{
			if(!CanUndo)
			{
				throw new InvalidOperationException();
			}
			UndoableEdit undoableEdit = undoStuck.Pop();
			undoableEdit.Undo();
			redoStuck.Push(undoableEdit);
			OnEdit?.Invoke(this, new UndoableEditEventArgs(this));
		}

		/// <summary>
		/// 最後の取り消された変更を実行します.
		/// </summary>
		public void Redo()
		{
			if(!CanRedo)
			{
				throw new InvalidOperationException();
			}
			UndoableEdit undoableEdit = redoStuck.Pop();
			undoableEdit.Redo();
			undoStuck.Push(undoableEdit);
			OnEdit?.Invoke(this, new UndoableEditEventArgs(this));
		}

		/// <summary>
		/// 履歴を削除します.
		/// </summary>
		public void DiscardAllEdits()
		{
			undoStuck.Clear();
			redoStuck.Clear();
			OnEdit?.Invoke(this, new UndoableEditEventArgs(this));
		}

		/// <summary>
		/// アンドゥ, リドゥによって発生した変更を記録しないなら最初に呼び出します.
		/// </summary>
		public void Begin()
		{
			this.mark++;
		}

		/// <summary>
		/// アンドゥ, リドゥによって発生した変更を記録しないなら最初に呼び出します.
		/// </summary>
		public void End()
		{
			this.mark--;
		}
		
	}
}
