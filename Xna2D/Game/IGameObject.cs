using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Contents;

namespace Xna2D.Game
{
	/// <summary>
	/// ゲーム中に表示されるなんらかのオブジェクト.
	/// </summary>
	public interface IGameObject : IMapEditorSupport, ICloneable
	{
		/// <summary>
		/// X座標.
		/// </summary>
		float X { set; get; }

		/// <summary>
		/// Y座標.
		/// </summary>
		float Y { set; get; }

		/// <summary>
		/// 横幅.
		/// </summary>
		float Width { set; get; }

		/// <summary>
		/// 縦幅.
		/// </summary>
		float Height { set; get; }

		/// <summary>
		/// 左のX座標.
		/// </summary>
		float Left { get; }

		/// <summary>
		/// 右のX座標.
		/// </summary>
		float Right { get; }

		/// <summary>
		/// 上のY座標.
		/// </summary>
		float Top { get; }

		/// <summary>
		/// 下のY座標.
		/// </summary>
		float Bottom { get; }

		/// <summary>
		/// 中央のX座標.
		/// </summary>
		float CenterX { get; }

		/// <summary>
		/// 中央のY座標.
		/// </summary>
		float CenterY { get; }

		/// <summary>
		/// 位置を返します.
		/// </summary>
		Vector2 Position { get; }

		/// <summary>
		/// 中央を返します.
		/// </summary>
		Vector2 Center { get; }

		/// <summary>
		/// 大きさを返します.
		/// </summary>
		Vector2 Size { get; }

		/// <summary>
		/// このオブジェクトの範囲.
		/// </summary>
		Rectangle Bounds { get; }

		/// <summary>
		/// このオブジェクトのレイヤー(0.0~1.0).
		/// </summary>
		float LayerDepth { set; get; }

		/// <summary>
		/// このオブジェクトが削除されるべきならtrue.
		/// </summary>
		bool IsDespawn { get; }
		
		/// <summary>
		/// このオブジェクトを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="elements"></param>
		void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements);

		/// <summary>
		/// このオブジェクトを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		/// <param name="elements"></param>
		void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements);
	}
}
