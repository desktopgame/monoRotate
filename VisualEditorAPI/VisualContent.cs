using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// レイヤーに配置されるオブジェクト.
	/// </summary>
	public interface VisualContent : ICloneable
	{
		/// <summary>
		/// このコンテンツの領域を再描画するコントロールが監視します.
		/// </summary>
		event EventHandler<VisualContentShapeEventArgs> OnInvalidate;

		/// <summary>
		/// このオブジェクトが選択されているならtrue.
		/// </summary>
		bool IsSelected { set; get; }

		/// <summary>
		/// X座標.
		/// </summary>
		int X { set; get; }

		/// <summary>
		/// Y座標.
		/// </summary>
		int Y { set; get; }

		/// <summary>
		/// 横幅.
		/// </summary>
		int Width { set; get; }

		/// <summary>
		/// 縦幅.
		/// </summary>
		int Height { set; get; }

		/// <summary>
		/// 左のX座標.
		/// </summary>
		int Left { get; }

		/// <summary>
		/// 右のX座標.
		/// </summary>
		int Right { get; }

		/// <summary>
		/// 上のY座標.
		/// </summary>
		int Top { get; }

		/// <summary>
		/// 下のY座標.
		/// </summary>
		int Bottom { get; }

		/// <summary>
		/// 位置.
		/// </summary>
		Point Location { get; }

		/// <summary>
		/// 大きさ.
		/// </summary>
		Size Size { get; }

		/// <summary>
		/// 範囲.
		/// </summary>
		Rectangle Bounds { get; }

		/// <summary>
		/// このオブジェクトを描画します.
		/// </summary>
		/// <param name="g"></param>
		void Draw(Graphics g);
	}
}
