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
	internal class VCFactoryImpl : VisualContentFactory
	{
		public int Width
		{
			get { return (int)gObj.Width; }
		}

		public int Height
		{
			get { return (int)gObj.Height; }
		}

		private IGameObject gObj;
		private Image image;

		public VCFactoryImpl(IGameObject gObj, Image image)
		{
			this.gObj = gObj;
			this.image = image;
		}

		public VisualContent NewInstance()
		{
			return new VCImpl((IGameObject)gObj.Clone(), image);
		}
	}
}
