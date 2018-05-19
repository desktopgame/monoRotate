using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 定数クラス.
	/// </summary>
	public static class GameConstants
	{
		public static readonly int SCREEN_WIDTH = 1280;
		public static readonly int SCREEN_HEIGHT = 720;
		public static readonly Vector2 SCREEN_SIZE = new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT);
		public static readonly float GRAVITY = 1f;
	}
}
