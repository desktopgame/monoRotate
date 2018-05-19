using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualEditorAPI
{
	/// <summary>
	/// レイヤーとオブジェクトを紐づけるコレクションのラッパーです.
	/// </summary>
	public class VisualLayer : MouseListener
	{
		/// <summary>
		/// このレイヤーの指定範囲を再描画する必要がある場合に呼ばれます.
		/// </summary>
		public event EventHandler<Rectangle> OnInvalidate;

		/// <summary>
		/// このレイヤーを含むコントロール全域を再描画する必要がある場合に呼ばれます.
		/// </summary>
		public event EventHandler OnRefresh;
		
		/// <summary>
		/// このレイヤーに配置されているオブジェクトの一覧です.
		/// </summary>
		public VisualContentCollection Items { private set; get; }

		/// <summary>
		/// このレイヤーで受け取るイベントの種類.
		/// </summary>
		public EventMask Mask { set; get; } = EventMask.DetectSelected;
		
		/// <summary>
		/// スナップ線の描画ペン.
		/// </summary>
		public Pen SnapLinePen
		{
			set {
				this._snapLinePen = value;
				OnRefresh?.Invoke(this, EventArgs.Empty);
			}
			get { return _snapLinePen; }
		}
		private Pen _snapLinePen;

		/// <summary>
		/// スナップ先の描画ブラシ.
		/// </summary>
		public Brush SnapTargetBrush
		{
			set
			{
				this._snapTargetBrush = value;
				OnRefresh?.Invoke(this, EventArgs.Empty);
			}
			get { return _snapTargetBrush; }
		}
		private Brush _snapTargetBrush;

		/// <summary>
		/// 選択領域の描画ブラシ.
		/// </summary>
		public Brush SelectedAreaBrush
		{
			set
			{
				this._selectedAreaBrush = value;
				OnRefresh?.Invoke(this, EventArgs.Empty);
			}
			get { return _selectedAreaBrush; }
		}
		private Brush _selectedAreaBrush;

		/// <summary>
		/// 選択されたコンテンツのブラシ.
		/// </summary>
		public Brush SelectedContentBrush
		{
			set
			{
				this._selectedContentBrush = value;
				OnRefresh?.Invoke(this, EventArgs.Empty);
			}
			get { return _selectedContentBrush; }
		}
		private Brush _selectedContentBrush;

		/// <summary>
		/// このレイヤーのスナップ中の線分.
		/// </summary>
		public SnapManager SnapManager
		{
			private set; get;
		}

		/// <summary>
		/// このレイヤーの選択中の領域.
		/// </summary>
		public SelectionManager SelectionManager
		{
			private set; get;
		}

		/// <summary>
		/// コンテンツが追加/削除されたときに自動でその領域を再描画するならtrue.
		/// デフォルトではtrueです。
		/// </summary>
		public bool Repaint
		{
			set; get;
		} = true;

		protected bool isPressed;
		protected VisualEditor editor;

		//作成予想位置
		private Rectangle oldDBounds;
		private Rectangle newDBounds;

		//選択位置
		private Rectangle oldSBounds;

		//切り取り, コピーによってコピーされたオブジェクト
		private Rectangle clipRect;
		private List<VisualContent> clip;
		
		public VisualLayer(VisualEditor editor)
		{
			this.editor = editor;
			this.SnapManager = new SnapManager();
			this.SelectionManager = new SelectionManager();
			this.Items = new VisualContentCollection();
			this.SnapLinePen = Pens.Blue;
			this.SnapTargetBrush = new SolidBrush(Color.FromArgb(128, Color.Blue));
			this.SelectedAreaBrush = new SolidBrush(Color.FromArgb(128, Color.Blue));
			this.SelectedContentBrush = new SolidBrush(Color.FromArgb(128, Color.Yellow));
			this.clip = new List<VisualContent>();
			this.Items.OnStateChanged += Items_OnStateChanged;
		}

		#region MouseListenerの実装
		public void MouseDown(object sender, MouseEventArgs e)
		{
			this.isPressed = true;
			MouseDraw(sender, e);
			if(editor.DragMode == DragMode.Select)
			{
				SelectionManager.Clear();
				SelectionManager.StartPoint = e.Location;
			}
		}
		
		public void MouseUp(object sender, MouseEventArgs e)
		{
			this.isPressed = false;
			if(editor.DragMode == DragMode.Select)
			{
				SelectionManager.EndPoint = e.Location;
				Rectangle newBounds = SelectionManager.Bounds;
				OnInvalidate?.Invoke(this, oldDBounds);
				OnInvalidate?.Invoke(this, newBounds);
				UpdateSelection();
			}
		}
		
		public void MouseMove(object sender, MouseEventArgs e)
		{
			if(isPressed)
			{
				MouseDraw(sender, e);
				Select(sender, e);
			}
			//スナップを検査
			SnapUpdate(e);
			//オブジェクトが生成される位置を決定
			if(editor.Factory == null)
			{
				return;
			}
			newDBounds.X = e.X;
			newDBounds.Y = e.Y;
			newDBounds.Width = editor.Factory.Width;
			newDBounds.Height = editor.Factory.Height;
			OnInvalidate?.Invoke(this, oldDBounds);
			OnInvalidate?.Invoke(this, newDBounds);
			OnRefresh?.Invoke(this, EventArgs.Empty);
			this.oldDBounds = newDBounds;
		}

		private void SnapUpdate(ref int mouseX, ref int mouseY)
		{
			SnapManager.Clear();
			for(int i = 0; i < Items.Count; i++)
			{
				SnapManager.Snap(Items[i], ref mouseX, ref mouseY, editor.Size.Width, editor.Size.Height);
			}
		}

		private void SnapUpdate(MouseEventArgs e)
		{
			int mouseX = e.X;
			int mouseY = e.Y;
			SnapUpdate(ref mouseX, ref mouseY);
		}

		/// <summary>
		/// マウスイベントの発生した位置に、ファクトリの生成したコンテンツを配置します.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void MouseDraw(object sender, MouseEventArgs e)
		{
			Draw(sender, e);
		}

		private void Draw(object sender, MouseEventArgs e)
		{
			if(editor.DragMode != DragMode.Draw)
			{
				return;
			}
			if(editor.Factory == null)
			{
				return;
			}
			//スナップ
			int mouseX = e.X;
			int mouseY = e.Y;
			SnapUpdate(ref mouseX, ref mouseY);
			//コンテンツの配置
			VisualContent content = editor.Factory.NewInstance();
			content.X = mouseX;
			content.Y = mouseY;
			if(IsIntersect(content))
			{
				return;
			}
			Items.Add(content);
			//追加されたコンテンツ領域を再描画
			//OnInvalidate?.Invoke(this, content.Bounds);
		}

		private void Select(object sender, MouseEventArgs e)
		{
			if(editor.DragMode != DragMode.Select)
			{
				return;
			}
			//選択領域を再描画
			SelectionManager.EndPoint = e.Location;
			OnInvalidate?.Invoke(this, SelectionManager.Bounds);
			OnInvalidate?.Invoke(this, oldSBounds);
			OnRefresh?.Invoke(this, EventArgs.Empty);
			this.oldSBounds = SelectionManager.Bounds;
			UpdateSelection();
		}

		private void UpdateSelection()
		{
			//交差するオブジェクトを選択状態に
			for(int i = 0; i < Items.Count; i++)
			{
				Items[i].IsSelected = Items[i].Bounds.IntersectsWith(SelectionManager.Bounds);
			}
		}
		#endregion

		/// <summary>
		/// このレイヤーを描画します.
		/// </summary>
		/// <param name="g"></param>
		public void Draw(Graphics g)
		{
			if(editor.DragMode == DragMode.Draw)
			{
				//全てのスナップを描画
				Array.ForEach(SnapManager.Lines, line =>
				{
					g.DrawLine(SnapLinePen, line.P1, line.P2);
				});
			}
			//全てのコンテンツを描画
			for(int i=0; i<Items.Count; i++)
			{
				Items[i].Draw(g);
				if(Items[i].IsSelected)
				{
					g.FillRectangle(SelectedContentBrush, Items[i].Bounds);
				}
			}
			if(editor.DragMode == DragMode.Draw)
			{
				//コンテンツ生成位置を描画
				g.DrawRectangle(Pens.Red, newDBounds);
				//スナップ先を描画
				Array.ForEach(SnapManager.Targets, target =>
				{
					g.FillRectangle(SnapTargetBrush, target.Bounds);
				});
			}
			if(editor.DragMode == DragMode.Select)
			{
				g.FillRectangle(SelectedAreaBrush, SelectionManager.Bounds);
			}
		}
		
		/// <summary>
		/// 全てのオブジェクトを非選択状態にします.
		/// </summary>
		public void ClearSelection()
		{
			for(int i = 0; i < Items.Count; i++)
			{
				Items[i].IsSelected = false;
			}
			SelectionManager.Clear();
		}

		/// <summary>
		/// このレイヤーの持つオブジェクトのどれかと指定のオブジェクトが重なるならtrue.
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		private bool IsIntersect(VisualContent content)
		{
			Rectangle b = content.Bounds;
			for(int i=0; i<Items.Count; i++)
			{
				if(Items[i].Bounds.IntersectsWith(b))
				{
					return true;
				}
			}
			return false;
		}

		//
		//ユーティリティ
		//
		
		/// <summary>
		/// 選択領域をバッファしてレイヤから削除します.
		/// </summary>
		public void Cut()
		{
			clip.Clear();
			VisualContent[] contents = GetSelectedContents();
			this.clip.AddRange(contents);
			this.clipRect = SelectionManager.Bounds;
			int index = 0;
			while(index < contents.Length)
			{
				Items.Remove(contents[index++]);
			}
		}

		/// <summary>
		/// バッファを選択領域を起点にして貼り付けます.
		/// </summary>
		public void Paste()
		{
			Rectangle rect = SelectionManager.Bounds;
			clip.ForEach(content =>
			{
				VisualContent clone = (VisualContent)content.Clone();
				clone.X = rect.X + (clone.X - clipRect.X);
				clone.Y = rect.Y + (clone.Y - clipRect.Y);
				Items.Add(clone);
			});
		}

		/// <summary>
		/// 選択領域をバッファします.
		/// </summary>
		public void Copy()
		{
			clip.Clear();
			this.clipRect = SelectionManager.Bounds;
			VisualContent[] contents = GetSelectedContents();
			this.clip.AddRange(contents);
		}
		
		/// <summary>
		/// 指定のオブジェクトをX方向で平均化します.
		/// </summary>
		/// <param name="contents"></param>
		/// <param name="f"></param>
		public void FlattenX(VisualContent[] contents, Func<int[], int> f)
		{
			int x = f(contents.Select((elem) => elem.X).ToArray());
			Array.ForEach(contents, c => c.X = x);
			CleanUp();
		}
		
		/// <summary>
		/// 選択されているコンテンツをX方向で平均化します.
		/// </summary>
		/// <param name="f"></param>
		public void FlattenX(Func<int[], int> f)
		{
			FlattenX(GetSelectedContents(), f);
		}

		/// <summary>
		/// 指定のオブジェクトをY方向で平均化します.
		/// </summary>
		/// <param name="contents"></param>
		/// <param name="f"></param>
		public void FlattenY(VisualContent[] contents, Func<int[], int> f)
		{
			int y = f(contents.Select((elem) => elem.Y).ToArray());
			Array.ForEach(contents, c => c.Y = y);
			CleanUp();
		}

		/// <summary>
		/// 選択されているコンテンツをY方向で平均化します.
		/// </summary>
		/// <param name="f"></param>
		public void FlattenY(Func<int[], int> f)
		{
			FlattenY(GetSelectedContents(), f);
		}

		/// <summary>
		/// 重なっているオブジェクトを削除します.
		/// </summary>
		public void CleanUp()
		{
			Items.RemoveAll((elem) =>
			{
				return Items.Exists((elem2) => !elem.Equals(elem2) && elem.Bounds.IntersectsWith(elem2.Bounds));
			});
		}

		/// <summary>
		/// 選択されている要素の一覧を返します.
		/// </summary>
		/// <returns></returns>
		public VisualContent[] GetSelectedContents()
		{
			UpdateSelection();
			return Items.ToList().Where((elem) => elem.IsSelected).ToArray();
		}

		//
		//コールバック
		//

		private void Items_OnStateChanged(object sender, VisualContentCollectionEventArgs e)
		{
			//追加されたコンテンツの変形を監視
			if(e.Type == VisualContentCollectionEventArgs.EventType.Added)
			{
				e.Content.OnInvalidate -= Content_OnInvalidate;
				e.Content.OnInvalidate += Content_OnInvalidate;
			//監視を終了
			} else if(e.Type == VisualContentCollectionEventArgs.EventType.Removed)
			{
				e.Content.OnInvalidate -= Content_OnInvalidate;
			}
			//追加/削除された領域の再描画
			if(Repaint)
			{
				OnInvalidate?.Invoke(this, e.Content.Bounds);
			}
		}

		private void Content_OnInvalidate(object sender, VisualContentShapeEventArgs e)
		{
			OnInvalidate?.Invoke(this, e.OldBounds);
			OnInvalidate?.Invoke(this, e.NewBounds);
		}
	}
}
