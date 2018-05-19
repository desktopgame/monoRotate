using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// IGameObjectReadOnlyCollectionの拡張です.
	/// </summary>
	public static class IGameObjectReadOnlyCollectionExtension
	{
		/// <summary>
		/// 全ての要素を訪問します.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		public static void ForEach(this IGameObjectReadOnlyCollection self, Action<IGameObject> action)
		{
			for(int i=0; i<self.Count; i++)
			{
				action(self[i]);
			}
		}

		/// <summary>
		/// 最初に見つかったプレイヤーを返します.
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IPlayer FindPlayer(this IGameObjectReadOnlyCollection self)
		{
			return self.FindObject<IPlayer>(elem => elem is IPlayer);
		}
	}
}
