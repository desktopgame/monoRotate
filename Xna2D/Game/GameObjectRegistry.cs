using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// IGameObjectのファクトリをIDと紐づけて管理するレジストリ.
	/// </summary>
	public class GameObjectRegistry
	{
		private static GameObjectRegistry instance;
		private Dictionary<int, Func<IGameData>> factoryDictionary;

		public int Count
		{
			get { return factoryDictionary.Count; }
		}

		public Func<IGameData> this[int id]
		{
			set {
				//ID割り当て
				factoryDictionary[id] = () =>
				{
					IGameData ret = value();
					ret.Initialize(id);
					return ret;
				};
				//factoryDictionary[id] = value;
			}
			get { return factoryDictionary[id]; }
		}

		private GameObjectRegistry()
		{
			this.factoryDictionary = new Dictionary<int, Func<IGameData>>();
		}

		/// <summary>
		/// 唯一のインスタンスを返します.
		/// </summary>
		/// <returns></returns>
		public static GameObjectRegistry GetInstance()
		{
			if(instance == null)
			{
				instance = new GameObjectRegistry();
			}
			return instance;
		}

		/// <summary>
		/// 指定のファクトリを登録します.
		/// </summary>
		/// <param name="f"></param>
		public void Reg(Func<IGameData> f)
		{
			this[factoryDictionary.Count] = f;
		}
	}
}
