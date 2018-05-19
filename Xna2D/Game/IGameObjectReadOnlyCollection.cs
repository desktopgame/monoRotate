using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 読み取り専用のゲームオブジェクトのコレクションです.
	/// </summary>
	public interface IGameObjectReadOnlyCollection : IEnumerable<IGameObject>
	{
		/// <summary>
		/// 要素数を返します.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// 指定位置の要素を返します.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		IGameObject this[int index] { get; }

		/// <summary>
		/// 指定のオブジェクトの追加を予約します.
		/// </summary>
		/// <param name="data"></param>
		void AddLater(IEnumerable<IGameObject> data);
		
		/// <summary>
		/// 指定の条件を満たすオブジェクトの中で最初に見つかったものを返します.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name=""></param>
		/// <param name="cond"></param>
		/// <returns></returns>
		T FindObject<T>(Predicate<IGameObject> cond);

		/// <summary>
		/// 指定の条件を満たす全てのオブジェクトを返します.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cond"></param>
		/// <returns></returns>
		T[] FindObjects<T>(Predicate<IGameObject> cond);
	}
}
