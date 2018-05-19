using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xna2D.Contents.Loaders
{
	/// <summary>
	/// コンテンツを読み込むインターフェイス.
	/// </summary>
	public interface Loader
	{
		/// <summary>
		/// 読み込みを実行します.
		/// </summary>
		/// <param name="contentManager"></param>
		/// <param name="assetName"></param>
		void Load(ContentManager contentManager, string assetName);

		/// <summary>
		/// 読みこめるならtrue.
		/// </summary>
		/// <param name="assetName"></param>
		/// <returns></returns>
		bool CanLoad(string assetName);
	}
}
