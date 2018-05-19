using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Game
{
	/// <summary>
	/// 背景.
	/// </summary>
	public class Background : GameObjectBase, ICollidable
	{
		public Background(string path, float width, float height) : base(path)
		{
			this.Width = width;
			this.Height = height;
			this.CanRotate = true;
		}

		public bool IsCollision(IGameObject o, out Direction dir)
		{
			dir = Direction.None;
			return false;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			DrawRotate(gameTime, renderer, elements);
		}

		protected override IGameObject NewInstance()
		{
			return new Background(Path, Width, Height);
		}
	}
}
