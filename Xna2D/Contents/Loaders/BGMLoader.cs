using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Xna2D.Contents.Loaders
{
	/// <summary>
	/// BGMを読み込む実装.
	/// </summary>
	public class BGMLoader : Loader
	{
		private Sound sound;
		private string startName;

		public BGMLoader(Sound sound, string startName)
		{
			this.sound = sound;
			this.startName = startName;
		}

		public bool CanLoad(string assetName)
		{
			return assetName.StartsWith(startName);
		}

		public void Load(ContentManager contentManager, string assetName)
		{
			sound.LoadBGM(assetName);
		}
	}
}
