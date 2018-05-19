using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Views
{
	/// <summary>
	/// ビューをリストで一括管理するクラス.
	/// </summary>
	public class ViewGroup : View
	{
		public override Vector2 Position
		{
			set
			{
				base.Position = value;
				DoLayout();
			}
		}

		public int Count
		{
			get { return viewList.Count; }
		}

		public View this[int index]
		{
			get { return viewList[index]; }
		}
		private List<View> viewList;

		public ViewGroup()
		{
			this.viewList = new List<View>();
		}

		public override void Update(GameTime gameTime)
		{
			viewList.ForEach(view => view.Update(gameTime));
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			viewList.ForEach(view => view.Draw(gameTime, renderer));
		}

		/// <summary>
		/// ビューを追加します.
		/// </summary>
		/// <param name="view"></param>
		/// <exception cref="ArgumentException">引数が既に親を持っている</exception>
		public void Add(View view)
		{
			if(view.Parent != null)
			{
				throw new ArgumentException();
			}
			view.Parent = this;
			viewList.Add(view);
			DoLayout();
		}

		/// <summary>
		/// ビューを削除します.
		/// </summary>
		/// <param name="view"></param>
		public void Remove(View view)
		{
			viewList.Remove(view);
			DoLayout();
		}

		/// <summary>
		/// ビューを削除します.
		/// </summary>
		/// <param name="index"></param>
		public void RemoveAt(int index)
		{
			viewList.RemoveAt(index);
			DoLayout();
		}

		/// <summary>
		/// ビューを全て削除します.
		/// </summary>
		public void Clear()
		{
			viewList.Clear();
		}

		/// <summary>
		/// レイアウトを更新します.
		/// サブクラスは最初にこのメソッドを呼び出してください.
		/// </summary>
		protected virtual void DoLayout()
		{
			for(int i=0; i<Count; i++)
			{
				View view = this[i];
				if(view is ViewGroup)
				{
					((ViewGroup)view).DoLayout();
				}
			}
		}
		
		/// <summary>
		/// Parentがnullのとき例外をスローします.
		/// </summary>
		protected void CheckParent()
		{
			if(Parent == null)
			{
				throw new NullReferenceException("このビューのレイアウトには親ビューが必要です");
			}
		}
	}
}
