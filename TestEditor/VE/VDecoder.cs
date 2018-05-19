using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestGame.Scenes;
using VisualEditorAPI;
using Xna2D.Game;

namespace TestEditor.VE
{
	internal class VDecoder : VisualDecoder
	{
		public bool IsEndOfStream { get { return offset >= gameObjectList.Count; } }

		private Dictionary<string, Image> imageDictionary;
		private PictureBox pictureBox;
		private List<IGameData> gameObjectList;
		private int offset;

		public VDecoder(Dictionary<string, Image> imageDictionary, PictureBox pictureBox)
		{
			this.imageDictionary = imageDictionary;
			this.pictureBox = pictureBox;
		}

		public void BeginDecode(string filepath)
		{
			this.gameObjectList = GIO.Load(filepath);
			this.offset = 0;
		}

		public KeyValuePair<float, VisualContent> DecodeContent()
		{
			IGameData data = gameObjectList[offset++];
			IGameObject obj = data as IGameObject;
			CheckEmptyObject(obj);
			return new KeyValuePair<float, VisualContent>(obj.LayerDepth, new VCImpl(obj, imageDictionary[obj.Path + ".png"]));
		}

		private void CheckEmptyObject(IGameObject obj)
		{
			if(!(obj is Camera))
			{
				return;
			}
			Camera camera = obj as Camera;
			pictureBox.Width = (int)camera.ScrollWidth;
			pictureBox.Height = (int)camera.ScrollHeight;
		}

		public void EndDecode(string filepath)
		{
		}
	}
}
