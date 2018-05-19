using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 方角を表す列挙型です.
	/// </summary>
	[Flags]
	public enum Direction
	{
		Top = 1,
		Bottom = 2,
		Left = 4,
		Right = 8,
		None = 16
	}
}
