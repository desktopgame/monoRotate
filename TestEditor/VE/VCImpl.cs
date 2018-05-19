using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualEditorAPI;
using Xna2D.Game;

namespace TestEditor.VE
{
	internal class VCImpl : ImageContent
	{
		public IGameObject GameObject
		{
			private set; get;
		}

		public VCImpl(IGameObject gameObject, Image image) : base(image)
		{
			this.GameObject = gameObject;
			this.X = (int)gameObject.X;
			this.Y = (int)gameObject.Y;
			this.Width = (int)gameObject.Width;
			this.Height = (int)gameObject.Height;
		}

		public void Inject()
		{
			GameObject.X = X;
			GameObject.Y = Y;
			GameObject.Width = Width;
			GameObject.Height = Height;
		}

		public override void Draw(Graphics g)
		{
			if(image == null)
			{
				return;
			}
			Rectangle srcRect = new Rectangle(0, 0, Width, Height);
			Rectangle dstRect = new Rectangle(X, Y, Width, Height);
			g.DrawImage(image, dstRect, srcRect, GraphicsUnit.Pixel);
		}

		public override object Clone()
		{
			Inject();
			return new VCImpl((IGameObject)GameObject.Clone(), image);
		}
	}
}
