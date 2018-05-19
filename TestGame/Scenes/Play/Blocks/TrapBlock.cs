using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Game;
using Xna2D.Game.Blocks;
using System.Diagnostics;

namespace TestGame.Scenes.Play.Blocks
{
	/// <summary>
	/// 罠.
	/// </summary>
	public class TrapBlock : Block, ICollisionCallback
	{
		private Direction needleDirInit;
		private Direction needleDir;
		public TrapBlock(string path, Direction dir) : base(path)
		{
			this.needleDir = dir;
			this.needleDirInit = dir;
		}

		public TrapBlock(string path, float width, float height, Direction dir) : base(path, width, height)
		{
			this.needleDir = dir;
			this.needleDirInit = dir;
		}

		public void Collision(ICollider collider, Direction dir)
		{
			if(!(collider is IPlayer))
			{
				return;
			}
			IPlayer player = collider as IPlayer;
			if(!player.IsRotateNow)
			{
				player.IsDie = true;
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			float rotate = GetRotate();
			Vector2 origin = GetOrigin(elements);
			if((needleDirInit == Direction.Left || needleDirInit == Direction.Right) && (rotate % 180) == 0)
			{
				rotate = 0f;
			}
			renderer.Draw(Path, Position, MathHelper.ToRadians(rotate), origin);
			//renderer.Draw(Path, Position + Scroll(elements), Color.White);
		}

		public override void AngleChanged(Camera.Angle newAngle)
		{
			base.AngleChanged(newAngle);
			this.needleDir = GetDirection(newAngle).Reverse();
		}
	}
}
