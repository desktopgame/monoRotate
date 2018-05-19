using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// コレクションの書き換えを通知するイベント.
	/// </summary>
	public class VisualContentCollectionEventArgs : EventArgs
	{
		/// <summary>
		/// 書き換えの種類.
		/// </summary>
		public enum EventType
		{
			Added, Removed
		}

		/// <summary>
		/// 変更の種類.
		/// </summary>
		public EventType Type
		{
			private set; get;
		}

		/// <summary>
		/// 対象のコンテンツ.
		/// </summary>
		public VisualContent Content
		{
			private set; get;
		}

		public VisualContentCollectionEventArgs(EventType type, VisualContent content)
		{
			this.Type = type;
			this.Content = content;
		}
	}
}
