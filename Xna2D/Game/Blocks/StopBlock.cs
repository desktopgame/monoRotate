using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Game.Blocks
{
	/// <summary>
	/// あたり判定が存在するが描画はされないブロックです.
	/// </summary>
	public class StopBlock : Block, ICollidable
	{
		public StopBlock(string path, float width, float height) : base(path, width, height)
		{
		}

		public bool IsCollision(IGameObject o, out Direction dir)
		{
			dir = Direction.None;
			return true;
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
		}
	}
}
