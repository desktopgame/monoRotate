using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// コンテンツの一覧です.
	/// </summary>
	public class VisualContentCollection : IList<VisualContent>
	{
		public VisualContent this[int index]
		{
			get { return contentList[index]; }
		}
		private List<VisualContent> contentList;

		/// <summary>
		/// コレクションの中身が書き換えられると呼ばれます.
		/// </summary>
		public event EventHandler<VisualContentCollectionEventArgs> OnStateChanged;

		public VisualContentCollection()
		{
			this.contentList = new List<VisualContent>();
		}

		/// <summary>
		/// このレイヤーの持つオブジェクトのどれかと指定のオブジェクトが重なるならtrue.
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public bool IsIntersect(VisualContent content)
		{
			System.Drawing.Rectangle oR = content.Bounds;
			return contentList.Exists((elem) => elem.Bounds.IntersectsWith(oR));
		}

		/// <summary>
		/// 指定の範囲を追加します.
		/// </summary>
		/// <param name="range"></param>
		public void AddRange(IEnumerable<VisualContent> range)
		{
			contentList.AddRange(range);
			Array.ForEach(range.ToArray(), elem =>
			{
				OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Added, elem));
			});
		}

		/// <summary>
		/// 条件を満たす要素を削除します.
		/// </summary>
		/// <param name="predicate"></param>
		public void RemoveAll(Predicate<VisualContent> predicate)
		{
			int index = contentList.FindIndex(predicate);
			if(index == -1)
			{
				return;
			}
			VisualContent content = contentList[index];
			OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Removed, content));
			contentList.RemoveAt(index);
			RemoveAll(predicate);
		}

		/// <summary>
		/// 条件を満たす要素があるならtrue.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public bool Exists(Predicate<VisualContent> predicate)
		{
			return contentList.Exists(predicate);
		}

		#region IListの実装
		public int Count
		{
			get { return ((IList<VisualContent>)contentList).Count; }
		}

		public bool IsReadOnly
		{
			get { return ((IList<VisualContent>)contentList).IsReadOnly; }
		}

		VisualContent IList<VisualContent>.this[int index]
		{
			get { return ((IList<VisualContent>)contentList)[index]; }
			set {
				if(index >= Count)
				{
					((IList<VisualContent>)contentList)[index] = value;
					OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Added, value));
					return;
				}
				VisualContent old = contentList[index];
				OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Removed, old));
				OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Added, value));
			}
		}

		public int IndexOf(VisualContent item)
		{
			return ((IList<VisualContent>)contentList).IndexOf(item);
		}

		public void Insert(int index, VisualContent item)
		{
			((IList<VisualContent>)contentList).Insert(index, item);
			OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Added, item));
		}

		public void RemoveAt(int index)
		{
			VisualContent elem = contentList[index];
			((IList<VisualContent>)contentList).RemoveAt(index);
			OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Removed, elem));
		}

		public void Add(VisualContent item)
		{
			((IList<VisualContent>)contentList).Add(item);
			OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Added, item));
		}

		public void Clear()
		{
			VisualContent[] arr = contentList.ToArray();
			((IList<VisualContent>)contentList).Clear();
			Array.ForEach(arr, elem =>
			{
				OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Removed, elem));
			});
		}

		public bool Contains(VisualContent item)
		{
			return ((IList<VisualContent>)contentList).Contains(item);
		}

		public void CopyTo(VisualContent[] array, int arrayIndex)
		{
			((IList<VisualContent>)contentList).CopyTo(array, arrayIndex);
		}

		public bool Remove(VisualContent item)
		{
			bool ret = ((IList<VisualContent>)contentList).Remove(item);
			OnStateChanged?.Invoke(this, new VisualContentCollectionEventArgs(VisualContentCollectionEventArgs.EventType.Removed, item));
			return ret;
		}

		public IEnumerator<VisualContent> GetEnumerator()
		{
			return ((IList<VisualContent>)contentList).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IList<VisualContent>)contentList).GetEnumerator();
		}
		#endregion
	}
}
