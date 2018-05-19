using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// 画像を描画する実装です.
	/// </summary>
	public abstract class ImageContent : VisualContentBase
	{
		protected Image image;

		public ImageContent(Image image)
		{
			this.image = image;
		}

		public override void Draw(Graphics g)
		{
			g.DrawImage(image, Location);
		}
	}
}
