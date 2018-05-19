using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 回転可能なオブジェクトが実装します.
	/// </summary>
	public interface IRotetable
	{
		/// <summary>
		/// 回転を拒否するならtrue.
		/// </summary>
		bool CanRotate { get; }

		/// <summary>
		/// まだ回転が終了していなければ.
		/// </summary>
		bool IsRotateNow { get; }

		/// <summary>
		/// 回転の開始座標.
		/// </summary>
		Vector2 RotateOrigin { get; }

		/// <summary>
		/// 回転の開始を受信します.
		/// この時点の座標をRotateOriginに保存します.
		/// <param name="elements"></param>
		/// <param name="len">回転量</param>
		/// </summary>
		void BeginRotate(IGameObjectReadOnlyCollection elements, float len);

		/// <summary>
		/// 回転を更新します.
		/// </summary>
		/// <param name="elements"></param>
		/// <param name="rad">回転量</param>
		/// <param name="pos">回転後の位置</param>
		void Progress(IGameObjectReadOnlyCollection elements, float rad, Vector2 pos);

		/// <summary>
		/// 回転の終了を受信します.
		/// </summary>
		/// <param name="elements"></param>
		void EndRotate(IGameObjectReadOnlyCollection elements);
	}
}
