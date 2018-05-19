using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Contents;
using Xna2D.Views;

namespace TestGame.Scenes.Title
{
	public class Button : View
	{
		public string AssetName { private set; get; }
		public SceneTypes SceneType { private set; get; }

		public Button(string assetName, SceneTypes sceneType)
		{
			this.AssetName = assetName;
			this.SceneType = sceneType;
		}

		public override void Update(GameTime gameTime)
		{
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Draw(AssetName, Position, Color.White);
		}
	}
}
