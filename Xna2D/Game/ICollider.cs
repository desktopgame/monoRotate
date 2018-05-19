using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 自発的に移動して他のオブジェクトと衝突することが出来るオブジェクト.
	/// </summary>
	public interface ICollider : IGameObject
	{
		/// <summary>
		/// 加速度X.
		/// </summary>
		float VX { set; get; }

		/// <summary>
		/// 加速度Y.
		/// </summary>
		float VY { set; get; }
	}
}
