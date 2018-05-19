using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Scenes;
using Xna2D.Utilities;
using Xna2D.Game;
using Xna2D.Input;

namespace TestGame.Scenes
{
	/// <summary>
	/// クレジット画面.
	/// </summary>
	public class CreditScene : SceneBase, ILayered
	{
		private static readonly Vector2 FONT_INFO_SIZE = new Vector2(384, 40);

		public CreditScene()
		{
			this.Next = (int)SceneTypes.Title;
		}

		public override void Update(GameTime gameTime)
		{
			if(Detector.GetInstance().IsDetect(HandleConstants.ENTER)) {
				this.IsEnd = true;
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer)
		{
			renderer.Begin();
			renderer.Draw("Textures/Back/Black", Vector2.Zero, Color.White);
			renderer.Draw("Textures/FontInfo", (GameConstants.SCREEN_SIZE - FONT_INFO_SIZE) / 2, Color.White);
			renderer.End();
		}

		public override void Show()
		{
			base.Show();
		}

		public bool IsNeedBackLayer(LayeredScene scene)
		{
			return false;
		}

		public bool IsNeedFrontLayer(LayeredScene scene)
		{
			return false;
		}
	}
}
