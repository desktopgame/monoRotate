using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// このオブジェクトがIColliderによって接触されているとき、
	/// コールバックされる必要があるならtrue.
	/// </summary>
	public interface ICollisionCallback
	{
		/// <summary>
		/// 衝突されると呼ばれます.
		/// </summary>
		/// <param name="collider"></param>
		/// <param name="dir">colliderの向き</param>
		void Collision(ICollider collider, Direction dir);
	}
}
