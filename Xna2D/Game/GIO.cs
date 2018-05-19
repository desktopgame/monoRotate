using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// ゲームデータのI/Oユーティリティクラス.
	/// </summary>
	public static class GIO
	{
		private static readonly string OBJECT_COUNT = "ObjectCount";
		private static readonly string OBJECT_PREFIX = "Object";
		private static readonly string OBJECT_KEYS_SUFFIX = ".Keys";

		/// <summary>
		/// 指定のコレクションの全てのゲームオブジェクトを保存します.
		/// </summary>
		/// <param name="filepath"></param>
		/// <param name="coll"></param>
		public static void Save(string filepath, GameObjectCollection coll)
		{
			Dictionary<string, string> content = new Dictionary<string, string>();
			Dictionary<string, string> objectAttr = new Dictionary<string, string>();
			for(int i=0; i<coll.Count; i++)
			{
				IGameData data = coll[i];
				int id = data.Id;
				objectAttr.Clear();
				data.Write(objectAttr);
				content[OBJECT_PREFIX + i] = id.ToString();
				StringBuilder keysBuilder = new StringBuilder();
				foreach(KeyValuePair<string, string> pair in objectAttr)
				{
					content[OBJECT_PREFIX + i + "." + pair.Key] = pair.Value;
					keysBuilder.Append(pair.Key).Append(",");
				}
				keysBuilder.Remove(keysBuilder.Length - 1, 1);
				content[OBJECT_PREFIX + i + ".Keys"] = keysBuilder.ToString();
			}
			content[OBJECT_COUNT] = coll.Count.ToString();
			Save(filepath, content);
		}
		
		/// <summary>
		/// 指定の辞書を = 区切りでファイルへ書き込みます.
		/// ただし、区切りの区切り(Key=Val自体の区切り)は改行ではなく ; で行われます。
		/// </summary>
		/// <param name="filepath"></param>
		/// <param name="content"></param>
		private static void Save(string filepath, Dictionary<string, string> content)
		{
			StringBuilder sb = new StringBuilder();
			foreach(KeyValuePair<string, string> pair in content)
			{
				sb.AppendLine(pair.Key + "=" + pair.Value + ";");
			}
			sb.Remove(sb.Length - 1, 1);
			File.WriteAllText(filepath, sb.ToString(), Encoding.UTF8);
		}

		/// <summary>
		/// 指定のパスの文字列を読み込んでゲームデータとして返します.
		/// </summary>
		/// <param name="filepath"></param>
		/// <returns></returns>
		public static List<IGameData> Load(string filepath)
		{
			//ファイル中の文字列をDictionaryへ変換
			//許容される形式
			//Key=Val;Key=Val;
			List<IGameData> objectList = new List<IGameData>();
			Dictionary<string, string> content = new Dictionary<string, string>();
			string source = File.ReadAllText(filepath, Encoding.UTF8).Replace(Environment.NewLine, "");
			string[] keyValuePairList = source.Split(';');
			foreach(string pairSource in keyValuePairList)
			{
				string[] pair = pairSource.Split('=');
				if(pair.Length != 2)
				{
					continue;
				}
				content[pair[0]] = pair[1];
			}
			//全てのオブジェクトの数
			GameObjectRegistry reg = GameObjectRegistry.GetInstance();
			Dictionary<string, string> objectAttr = new Dictionary<string, string>();
			int objCount = content.ParseInteger(OBJECT_COUNT);
			for(int i=0; i<objCount; i++)
			{
				objectAttr.Clear();
				IGameData data = LoadImpl(content, objectAttr, i);
				//カメラが複数生成されないように
				if(data is Camera && objectList.Where(exp => exp is Camera).ToArray().Length > 0)
				{
					continue;
				}
				objectList.Add(data);
			}
			return objectList;
		}

		private static IGameData LoadImpl(Dictionary<string, string> content, Dictionary<string, string> objectAttr, int index)
		{
			//オブジェクト番号に紐づけられたIDを取得
			//Object1 = 0;
			string objKey = OBJECT_PREFIX + index;
			string objIdStr = content[objKey];
			int id = content.ParseInteger(objKey);
			IGameData gObj = GameObjectRegistry.GetInstance()[id]();
			gObj.Initialize(id);
			//全てのキーを取得
			//Object1.Keys = Hoge,Huga;
			string objKeysKey = OBJECT_PREFIX + index + OBJECT_KEYS_SUFFIX;
			string[] objKeys = content[objKeysKey].Split(',');
			for(int j = 0; j < objKeys.Length; j++)
			{
				//Object1.Hoge = Hoge;
				string objKeyKey = OBJECT_PREFIX + index + "." + objKeys[j];
				string objKeyVal = content[objKeyKey];
				objectAttr[objKeys[j]] = objKeyVal;
			}
			gObj.Read(objectAttr);
			return gObj;
		}
		
	}
}
