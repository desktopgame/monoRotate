using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xna2D.Contents.Loaders
{
	/// <summary>
	/// コンテンツをルートから全て訪問するイテレータです.
	/// </summary>
	public class ContentIterator
	{
		/// <summary>
		/// 次のコンテンツを読み込めるならtrue.
		/// </summary>
		public bool HasNext
		{
			get { return Offset < assetNameList.Count; }
		}

		/// <summary>
		/// 全てのアセットの数.
		/// </summary>
		public int Length
		{
			get { return assetNameList.Count; }
		}

		/// <summary>
		/// 現在位置.
		/// </summary>
		public int Offset
		{
			private set; get;
		}

		/// <summary>
		/// 現在の進捗を返します.
		/// </summary>
		public int Parcent
		{
			get { return (int)(((float)Offset / (float)assetNameList.Count) * 100); }
		}

		private ContentManager contentManager;
		private List<Loader> loaderList;
		private List<string> assetNameList;

		public ContentIterator(ContentManager contentManager)
		{
			this.contentManager = contentManager;
			this.loaderList = new List<Loader>();
			this.assetNameList = new List<string>();
			this.Offset = 0;
		}

		/// <summary>
		/// ローダーを追加して自身を返します.
		/// </summary>
		/// <param name="loader"></param>
		/// <returns></returns>
		public ContentIterator Add(Loader loader)
		{
			loaderList.Add(loader);
			return this;
		}
		
		/// <summary>
		/// コンテンツの名前一覧をキャッシュします.
		/// 動的にコンテンツが入れ替えられることがあるならそのたびにこのメソッドを呼び出します。
		/// そうでなくとも必ず一度は呼び出す必要があります。
		/// </summary>
		public void Initialize()
		{
			DirectoryInfo root = new DirectoryInfo(contentManager.RootDirectory);
			Initialize(root, root);
			this.Offset = 0;
		}

		private void Initialize(DirectoryInfo contentRoot, DirectoryInfo root)
		{
			//根優先
			DirectoryInfo[] directories = root.GetDirectories();
			for(int i = 0; i < directories.Length; i++)
			{
				Initialize(contentRoot, directories[i]);
			}

			FileInfo[] files = root.GetFiles();
			for(int i=0; i<files.Length; i++)
			{
				FileInfo file = files[i];
				string assetName = file.FullName.Substring(contentRoot.FullName.Length + 1);
				int period = assetName.LastIndexOf(".");
				assetName = assetName.Substring(0, period);
				assetName = assetName.Replace(Path.DirectorySeparatorChar, '/');
				assetNameList.Add(assetName);
			    //Debug.WriteLine(file.FullName);
			}
		}
		
		/// <summary>
		/// コンテンツを一つ読み込みます.
		/// </summary>
		public void Next()
		{
			string assetName = assetNameList[Offset++];
			loaderList.ForEach(loader =>
			{
				if(loader.CanLoad(assetName))
				{
					loader.Load(contentManager, assetName);
				}
			});
		}
	}
}
