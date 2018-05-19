using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using System.Diagnostics;

namespace Xna2D.Game.Blocks
{
	/// <summary>
	/// 特別な効果を持たないブロック.
	/// </summary>
	public class Block : GameObjectBase
	{
		public Block(string path, float width, float height) : base(path)
		{
			this.Width = width;
			this.Height = height;
		}

		public Block(string path) : this(path, 32, 32)
		{
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
		}

		public override bool IsReadOnly(string key)
		{
			//以前のステージデータとの互換性のために残しています
			//(このデータはもう使用していないので書き換えられても問題はないのですが念のため)
			if(key == "IsSnap" ||
			   key == "SnapID")
			{
				return false;
			}
			return base.IsReadOnly(key);
		}
		
		protected override IGameObject NewInstance()
		{
			return new Block(Path, Width, Height);
		}
		
		private bool IsVertical(Camera.Angle angle)
		{
			return angle == Camera.Angle.Normal || angle == Camera.Angle.Vertical;
		}
	}
}
