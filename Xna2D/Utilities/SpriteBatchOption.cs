using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Utilities
{
	public class SpriteBatchOption
	{
		public Vector2 Position { set; get; } = Vector2.Zero;

		public Rectangle? SourceRectangle { set; get; } = null;

		public Color Color { set; get; } = Color.White;

		public float Rotate { set; get; } = 0f;

		public Vector2 Origin { set; get; } = Vector2.Zero;

		public Vector2 Scale { set; get; } = new Vector2(1f, 1f);

		public SpriteEffects Effects { set; get; } = SpriteEffects.None;

		public float LayerDepth { set; get; } = 0f;


		public SpriteBatchOption()
		{
		}

		public SpriteBatchOption SetPosition(Vector2 position)
		{
			this.Position = position;
			return this;
		}

		public SpriteBatchOption SetSourceRectangle(Rectangle? sourceRectangle)
		{
			this.SourceRectangle = sourceRectangle;
			return this;
		}

		public SpriteBatchOption SetColor(Color color)
		{
			this.Color = color;
			return this;
		}

		public SpriteBatchOption SetRotate(float rotate)
		{
			this.Rotate = rotate;
			return this;
		}

		public SpriteBatchOption SetOrigin(Vector2 origin)
		{
			this.Origin = origin;
			return this;
		}

		public SpriteBatchOption SetScale(Vector2 scale)
		{
			this.Scale = scale;
			return this;
		}

		public SpriteBatchOption SetScale(float scale)
		{
			return SetScale(new Vector2(scale, scale));
		}

		public SpriteBatchOption SetEffects(SpriteEffects effects)
		{
			this.Effects = effects;
			return this;
		}

		public SpriteBatchOption SetLayerDepth(float layerDepth)
		{
			this.LayerDepth = layerDepth;
			return this;
		}

		public void Draw(SpriteBatch spriteBatch, Texture2D texture)
		{
			spriteBatch.Draw(texture, Position, SourceRectangle, Color, Rotate, Origin, Scale, Effects, LayerDepth);
		}
	}
}
