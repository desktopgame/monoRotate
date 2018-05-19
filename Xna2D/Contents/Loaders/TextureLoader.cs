using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Xna2D.Contents.Loaders
{
	/// <summary>
	/// 画像を読み込む実装.
	/// </summary>
	public class TextureLoader : Loader
	{
		private Renderer renderer;
		private string startName;

		public TextureLoader(Renderer renderer, string startName)
		{
			this.renderer = renderer;
			this.startName = startName;
		}

		public bool CanLoad(string assetName)
		{
			return assetName.StartsWith(startName);
		}

		public void Load(ContentManager contentManager, string assetName)
		{
			renderer.Load(assetName);
		}
	}
}
