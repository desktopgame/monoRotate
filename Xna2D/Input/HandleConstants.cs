using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Input
{
	/// <summary>
	/// キーボードの入力とパッドの入力を紐づけるハンドルを定義する定数クラスです.
	/// </summary>
	public static class HandleConstants {
		/// <summary>
		/// 決定キー.
		/// </summary>
		public static readonly Handle ENTER = new Handle(
			(index, mouse, key) => (key.IsKeyDown(Keys.Z) || key.IsKeyDown(Keys.Space)),
			(index, gamePad) => gamePad.Buttons.A == ButtonState.Pressed
		);

		/// <summary>
		/// キャンセルキー.
		/// </summary>
		public static readonly Handle CANCEL = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.X),
			(index, gamePad) => gamePad.Buttons.B == ButtonState.Pressed
		);

		/// <summary>
		/// 抽出キー.
		/// </summary>
		public static readonly Handle EXTRACT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.C),
			(index, gamePad) => gamePad.Buttons.X == ButtonState.Pressed
		);

		/// <summary>
		/// 上キー.
		/// </summary>
		public static readonly Handle TOP = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Up),
			(index, gamePad) => 
				gamePad.ThumbSticks.Left.Y > 0 ||
				gamePad.ThumbSticks.Right.Y > 0
		);

		/// <summary>
		/// 左キー.
		/// </summary>
		public static readonly Handle LEFT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Left),
			(index, gamePad) => 
				gamePad.ThumbSticks.Left.X < 0 ||
				gamePad.ThumbSticks.Right.X < 0
		);

		/// <summary>
		/// 下キー.
		/// </summary>
		public static readonly Handle BOTTOM = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Down),
			(index, gamePad) => 
				gamePad.ThumbSticks.Left.Y < 0 ||
				gamePad.ThumbSticks.Right.Y < 0
		);

		/// <summary>
		/// 右キー.
		/// </summary>
		public static readonly Handle RIGHT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Right),
			(index, gamePad) => 
				gamePad.ThumbSticks.Left.X > 0 ||
				gamePad.ThumbSticks.Right.X > 0
		);

		/// <summary>
		/// ポーズキー.
		/// </summary>
		public static readonly Handle PAUSE = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Q),
			(index, gamePad) => gamePad.Buttons.Start == ButtonState.Pressed
		);

		/// <summary>
		/// 終了キー.
		/// </summary>
		public static readonly Handle EXIT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Escape),
			(index, gamePad) => gamePad.Buttons.Back == ButtonState.Pressed
		);
	}
}
