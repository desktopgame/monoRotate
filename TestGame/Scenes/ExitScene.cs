using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;

namespace TestGame.Scenes
{
	/// <summary>
	/// 終了画面.
	/// </summary>
	public class ExitScene : SceneBase
	{
		public ExitScene()
		{
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Begin();
			renderer.Draw("Textures/Back/Black", Vector2.Zero, Color.White * 0.5f);
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
			Game1.IsExit = true;
		}
	}
}
