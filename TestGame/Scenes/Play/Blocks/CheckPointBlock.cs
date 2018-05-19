using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Game;
using Xna2D.Game.Blocks;
using Xna2D.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace TestGame.Scenes.Play.Blocks
{
	/// <summary>
	/// チェックポイント.
	/// </summary>
	public class CheckPointBlock : Block, ICollidable
	{
		/// <summary>
		/// プレイヤーがこのポイントを通過したならtrue.
		/// </summary>
		public bool CheckPlayer
		{
			private set; get;
		}
		private Animation2D animation;
		private FrameTimer timer;

		public CheckPointBlock(string path) : base(path)
		{
			Init();
		}

		public CheckPointBlock(string path, float width, float height) : base(path, width, height)
		{
			Init();
		}

		private void Init()
		{
			this.CheckPlayer = false;
			this.animation = new Animation2D(32, 32, 1, 2);
			this.timer = new FrameTimer(10);
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			base.Update(gameTime, elements);
			Switch(gameTime, elements);
		}

		private void Switch(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			IPlayer player = elements.FindPlayer();
			if(CheckPlayer || !player.Bounds.Intersects(Bounds) || player.IsRotateNow)
			{
				return;
			}
			//プレイヤーと触れたならこのポイントをアクティブにして、
			//他のポイントをデアクティブにする
			CheckPointBlock[] objects = elements.FindObjects<CheckPointBlock>(elem => elem is CheckPointBlock);
			Array.ForEach(objects, o => {
				if(!o.Equals(this))
				{
					o.CheckPlayer = false;
				}
			});
			this.CheckPlayer = true;
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			if(!CheckPlayer)
			{
				DrawRotate(gameTime, renderer, elements);
				//renderer.Draw("Textures/Block/CheckPoint", Position + Scroll(elements), animation.Bounds, Color.White);
			} else {
				if(timer.Update().Elapsed())
				{
					animation.Update();
					animation.Loop();
				}
				renderer.Draw("Textures/Block/CheckPoint_Blink", Position, animation.Bounds, MathHelper.ToRadians(GetRotate()), GetOrigin(elements));
				//renderer.Draw("Textures/Block/CheckPoint_Blink", Position + Scroll(elements), animation.Bounds, Color.White);
			}
		}

		public bool IsCollision(IGameObject o, out Direction dir)
		{
			dir = Direction.None;
			return false;
		}
	}
}
