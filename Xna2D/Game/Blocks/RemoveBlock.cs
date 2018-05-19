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
	/// 消えるブロック
	/// </summary>
	public class RemoveBlock : Block, ICollisionCallback
	{
		private bool removeStart;
		private int offset;
		private int length;

		protected static readonly string KEY_LENGTH = "RemoveLength";

		public RemoveBlock(string path, float width, float height) : base(path, width, height)
		{
		}

		public RemoveBlock(string path) : base(path)
		{
		}

		public override void Initialize(int id)
		{
			base.Initialize(id);
			this.length = 60;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			base.Update(gameTime, elements);
			if(!removeStart)
			{
				return;
			}
			if(offset++ == length)
			{
				this.IsDespawn = true;
			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			if(!removeStart)
			{
				base.Draw(gameTime, renderer, elements);
			} else
			{
				float alpha = (1f - ((float)offset / (float)length));
				renderer.Draw(Path, Position, Color.White * alpha);
			}
		}

		public void Collision(ICollider collider, Direction dir)
		{
			if(!dir.HasFlag(Direction.Bottom))
			{
				return;
			}
			//このオブジェクトに上から衝突した
			this.removeStart = true;
		}

		public override void Write(Dictionary<string, string> d)
		{
			base.Write(d);
			d[KEY_LENGTH] = length.ToString();
		}

		public override void Read(Dictionary<string, string> d)
		{
			base.Read(d);
			this.length = d.ParseInteger(KEY_LENGTH);
		}

		public override bool IsReadOnly(string key)
		{
			if(key == KEY_LENGTH)
			{
				return false;
			}
			return base.IsReadOnly(key);
		}

		protected override IGameObject NewInstance()
		{
			RemoveBlock block = new RemoveBlock(Path, Width, Height);
			block.length = length;
			return block;
		}

	}
}
