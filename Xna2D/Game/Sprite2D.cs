using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 描画オプションのためのデータクラス.
	/// </summary>
	public class Sprite2D
	{
		/// <summary>
		/// X座標.
		/// </summary>
		public float X { set; get; }

		/// <summary>
		/// Y座標.
		/// </summary>
		public float Y { set; get; }
		
		/// <summary>
		/// X方向の倍率.
		/// </summary>
		public float ScaleX { set; get; }
		
		/// <summary>
		/// Y方向の倍率.
		/// </summary>
		public float ScaleY { set; get; }

		/// <summary>
		/// 回転.
		/// </summary>
		public float Rotate { set; get; }

		/// <summary>
		/// 透明度.
		/// </summary>
		public float Alpha { set; get; }

		public Sprite2D()
		{
		}
	}
}
