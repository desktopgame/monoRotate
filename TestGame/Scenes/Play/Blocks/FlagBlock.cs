using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Game;
using Xna2D.Game.Blocks;
using Xna2D.Utilities;

namespace TestGame.Scenes.Play.Blocks
{
	/// <summary>
	/// 旗.
	/// </summary>
	public class FlagBlock : Block, ICollidable
	{
		public bool GoalPlayer
		{
			private set; get;
		}

		private Animation2D animation2D;
		private FrameTimer timer;

		public FlagBlock(string path) : base(path)
		{
			Init();
		}

		public FlagBlock(string path, float width, float height) : base(path, width, height)
		{
			Init();
		}

		private void Init()
		{
			this.animation2D = new Animation2D(32, 32, 1, 4);
			this.timer = new FrameTimer(10);
			this.GoalPlayer = false;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			base.Update(gameTime, elements);
			IPlayer player = elements.FindPlayer();
			if(!GoalPlayer && Bounds.Intersects(player.Bounds) && !player.IsRotateNow)
			{
				this.GoalPlayer = true;
			}
			if(timer.Update().Elapsed())
			{
				animation2D.Update();
				animation2D.Loop();
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			renderer.Draw(Path + "_Anime", Position, animation2D.Bounds, MathHelper.ToRadians(GetRotate()), GetOrigin(elements));
//			renderer.Draw(Path + "_Anime", Position + Scroll(elements), animation2D.Bounds, Color.White);
		}

		protected override IGameObject NewInstance()
		{
			return new FlagBlock(Path, Width, Height);
		}

		public bool IsCollision(IGameObject o, out Direction dir)
		{
			dir = Direction.None;
			return false;
		}
	}
}
