using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// あるオブジェクトから衝突されるときに、計算を簡略化することが出来るならこれを実装します.
	/// これを実装しない場合常に厳密な方法で判定されます。
	/// </summary>
	public interface ICollidable : IGameObject
	{
		/// <summary>
		/// 衝突しているかどうかを計算して返します.
		/// 衝突しているなら衝突した方向を返します。
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		/// <returns></returns>
		bool IsCollision(IGameObject o, out Direction dir);
	}
}
