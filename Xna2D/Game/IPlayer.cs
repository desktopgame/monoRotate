using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// プレイヤー
	/// </summary>
	public interface IPlayer : IGameObject, IRotetable
	{
		/// <summary>
		/// 死亡しているならtrue.
		/// </summary>
		bool IsDie { set; get; }
	}
}
