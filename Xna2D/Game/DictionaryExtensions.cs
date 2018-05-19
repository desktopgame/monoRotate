using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	public static class DictionaryExtensions
	{
		/// <summary>
		/// 辞書に指定のキーが含まれるならそれを変換して返します.
		/// 含まれない、変換に失敗した場合にはデフォルト値を返します.
		/// </summary>
		/// <typeparam name="R"></typeparam>
		/// <typeparam name="K"></typeparam>
		/// <typeparam name="V"></typeparam>
		/// <param name="self"></param>
		/// <param name="key"></param>
		/// <param name="f"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static R Parse<R, K, V>(this Dictionary<K, V> self, K key, Func<V, R> f, R defaultValue)
		{
			if(!self.ContainsKey(key))
			{
				return defaultValue;
			}
			try
			{
				return f(self[key]);
			} catch (Exception e)
			{
				return defaultValue;
			}
		}

		public static int ParseInteger<K>(this Dictionary<K, string> self, K key, int defaultValue = 0)
		{
			return Parse(self, key, (value) => int.Parse(value), defaultValue);
		}

		public static float ParseFloat<K>(this Dictionary<K, string> self, K key, float defaultValue = 0.0f)
		{
			return Parse(self, key, (value) => float.Parse(value), defaultValue);
		}

		public static bool ParseBoolean<K>(this Dictionary<K, string> self, K key, bool defaultValue = false)
		{
			return Parse(self, key, (value) => bool.Parse(value), defaultValue);
		}
	}
}
