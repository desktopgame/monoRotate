using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// IGameObjectReadOnlyCollectionのデフォルト実装です.
	/// </summary>
	public class GameObjectCollection : IGameObjectReadOnlyCollection
	{
		private List<IGameObject> gameObjectList;
		private List<IGameObject> rangeList;

		public int Count
		{
			get { return gameObjectList.Count; }
		}

		public GameObjectCollection()
		{
			this.gameObjectList = new List<IGameObject>();
			this.rangeList = new List<IGameObject>();
		}

		public void Add(IGameObject o)
		{
			gameObjectList.Add(o);
		}

		public void AddRange(IEnumerable<IGameObject> range)
		{
			gameObjectList.AddRange(range);
		}

		public void Remove(IGameObject o)
		{
			gameObjectList.Remove(o);
		}

		public IGameObject RemoveAt(int index)
		{
			IGameObject ret = gameObjectList[index];
			gameObjectList.RemoveAt(index);
			return ret;
		}

		public void RemoveAll(Predicate<IGameObject> cond)
		{
			gameObjectList.RemoveAll(cond);
		}

		public void Clear()
		{
			gameObjectList.Clear();
		}

		//
		//IGameObjectReadOnlyCollectionの実装
		//

		public IGameObject this[int index]
		{
			set { gameObjectList[index] = value; }
			get { return gameObjectList[index]; }
		}

		public IEnumerator<IGameObject> GetEnumerator()
		{
			return gameObjectList.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return gameObjectList.GetEnumerator();
		}


		public void AddLater(IEnumerable<IGameObject> data)
		{
			rangeList.AddRange(data);
		}

		public void AddExec()
		{
			gameObjectList.AddRange(rangeList);
			rangeList.Clear();
		}

		public T FindObject<T>(Predicate<IGameObject> cond)
		{
			for(int i=0; i<gameObjectList.Count; i++)
			{
				IGameObject obj = gameObjectList[i];
				if(cond(obj))
				{
					return (T)obj;
				}
			}
			return default(T);
		}

		public T[] FindObjects<T>(Predicate<IGameObject> cond)
		{
			List<T> ret = new List<T>();
			for(int i = 0; i < gameObjectList.Count; i++)
			{
				IGameObject obj = gameObjectList[i];
				if(cond(obj))
				{
					ret.Add((T)obj);
				}
			}
			return ret.Distinct().ToArray();
		//	return ret.ToArray();
		}

	}
}
