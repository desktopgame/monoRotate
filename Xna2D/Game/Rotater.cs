using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using System.Diagnostics;

namespace Xna2D.Game
{
	[Compatibility]
	public class Rotater : GameObjectBase
	{
		public Rotater() : base(null)
		{

		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
		}

		public override void EndRotate(IGameObjectReadOnlyCollection elements)
		{
		}
		
		protected override IGameObject NewInstance()
		{
			return new Rotater();
		}
	}
}
