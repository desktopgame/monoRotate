using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	public static class DirectionExtensions
	{
		/// <summary>
		/// 方向を反転します.
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static Direction Reverse(this Direction self)
		{
			switch(self)
			{
				case Direction.Top: return Direction.Bottom;
				case Direction.Bottom: return Direction.Top;
				case Direction.Left: return Direction.Right;
				case Direction.Right: return Direction.Left;
			}
			return Direction.None;
		}
	}
}
