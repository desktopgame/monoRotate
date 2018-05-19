using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Scenes
{
	/// <summary>
	/// リソースの位置や大きさに関する定数クラス.
	/// </summary>
	public static class Resource
	{
		/// <summary>
		/// 数字と切り抜き範囲の対応.
		/// </summary>
		public static readonly Rectangle[] NUMBER_RECTANGLES = new Rectangle[] {
			new Rectangle(0, 0, 30, 40),
			new Rectangle(30, 0, 8, 40),
			new Rectangle(38, 0, 32, 40),
			new Rectangle(70, 0, 32, 40),
			new Rectangle(103, 0, 32, 40),
			new Rectangle(134, 0, 32, 40),
			new Rectangle(166, 0, 32, 40),
			new Rectangle(198, 0, 32, 40),
			new Rectangle(230, 0, 32, 40),
			new Rectangle(263, 0, 32, 40),
		};
	}
}
