using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualEditorAPI;
using Xna2D.Game;

namespace TestEditor.VE
{
	internal class VEncoder : VisualEncoder
	{
		private GameObjectCollection coll;
		private float layerDepth;
		public void BeginEncode(string filepath)
		{
			this.coll = new GameObjectCollection();
		}

		public void BeginLayer(VisualLayer layer, float layerDepth)
		{
			this.layerDepth = layerDepth;
		}

		public void EncodeContent(VisualContent content)
		{
			VCImpl impl = content as VCImpl;
			impl.GameObject.LayerDepth = layerDepth;
			impl.Inject();
			coll.Add(impl.GameObject);
		}

		public void EndLayer(VisualLayer layer, float layerDepth)
		{
		}

		public void EndEncode(string filepath)
		{
			GIO.Save(filepath, coll);
		}
	}
}
