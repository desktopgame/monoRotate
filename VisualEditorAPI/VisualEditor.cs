using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualEditorAPI
{
	/// <summary>
	/// PictureBoxを高機能なUIビルダにするためのラッパー.
	/// </summary>
	public class VisualEditor
	{
		/// <summary>
		/// 現在のレイヤーの深さ.
		/// </summary>
		public float LayerDepth
		{
			set
			{
				if(value > 1f || value < 0f) {
					throw new ArgumentException();
				}
				AllocLayer(value);
				pictureBox.Refresh();
				this._layerDepth = value;
				//レイヤーの浅い順に並べる
				List<float> keyList = layerListSet.Keys.ToList();
				keyList.Sort();
				this.eventOrder = keyList.ToArray();
			}
			get { return _layerDepth; }
		}
		private float _layerDepth;
		
		/// <summary>
		/// 全てのレイヤー階層を昇順で返します.
		/// </summary>
		public float[] LayerHierarchy
		{
			get
			{
				float[] copy = new float[eventOrder.Length];
				eventOrder.CopyTo(copy, 0);
				return copy;
			}
		}

		/// <summary>
		/// 指定の深さのレイヤーを返します.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public VisualLayer this[float key]
		{
			get {
				AllocLayer(key);
				return layerListSet[key];
			}
		}

		/// <summary>
		/// 現在のレイヤーを返します.
		/// </summary>
		public VisualLayer Current
		{
			get { return layerListSet[LayerDepth]; }
		}
		
		/// <summary>
		/// ピクチャボックスの大きさを返します.
		/// </summary>
		public Size Size
		{
			get { return pictureBox == null ? Size.Empty : pictureBox.Size; }
		}

		/// <summary>
		/// ドラッグ時の挙動.
		/// </summary>
		public DragMode DragMode
		{
			set
			{
				this._dragMode = value;
				pictureBox?.Refresh();
				if(value == DragMode.Select)
				{
					return;
				}
				foreach(KeyValuePair<float, VisualLayer> pair in layerListSet)
				{
					pair.Value.ClearSelection();
				}
			}
			get { return _dragMode; }
		}
		private DragMode _dragMode;

		/// <summary>
		/// ドラッグ時に配置されるコンテンツのファクトリ.
		/// </summary>
		public VisualContentFactory Factory
		{
			set
			{
				this._factory = value;
				pictureBox?.Refresh();
			}
			get { return _factory; }
		}
		private VisualContentFactory _factory;

		/// <summary>
		/// UI情報の保存実装.
		/// </summary>
		public VisualEncoder Encoder
		{
			set; get;
		}

		/// <summary>
		/// UI情報の読込実装.
		/// </summary>
		public VisualDecoder Decoder
		{
			set; get;
		}

		private PictureBox pictureBox;
		private Dictionary<float, VisualLayer> layerListSet;
		private float[] eventOrder;

		public VisualEditor()
		{
			this.layerListSet = new Dictionary<float, VisualLayer>();
		}

		private void AllocLayer(float layerDepth)
		{
			//そこにまだレイヤーがない
			if(layerListSet.ContainsKey(layerDepth))
			{
				return;
			}
			VisualLayer layer = new VisualLayer(this);
			layerListSet[layerDepth] = layer;
			layer.OnInvalidate += Layer_OnInvalidate;
			layer.OnRefresh += Layer_OnRefresh;
		}

		/// <summary>
		/// 指定のPictureBoxにこのエディターの機能をインストールします.
		/// </summary>
		/// <param name="pictureBox"></param>
		public void Install(PictureBox pictureBox)
		{
			this.pictureBox = pictureBox;
			pictureBox.MouseDown += PictureBox_MouseDown;
			pictureBox.MouseUp += PictureBox_MouseUp;
			pictureBox.MouseMove += PictureBox_MouseMove;
			pictureBox.Paint += PictureBox_Paint;
			this.LayerDepth = 0f;
			this.DragMode = DragMode.Draw;
		}

		/// <summary>
		/// 指定のPictureBoxからこのエディターの機能をアンインストールします.
		/// </summary>
		/// <param name="pictureBox"></param>
		public void Uninstall(PictureBox pictureBox)
		{
			this.pictureBox = null;
			pictureBox.MouseDown -= PictureBox_MouseDown;
			pictureBox.MouseUp -= PictureBox_MouseUp;
			pictureBox.MouseMove -= PictureBox_MouseMove;
			pictureBox.Paint -= PictureBox_Paint;
		}
		
		/// <summary>
		/// 全てのレイヤーを初期化します.
		/// </summary>
		public void Clear()
		{
			foreach(VisualLayer layer in layerListSet.Values)
			{
				layer.Items.Clear();
			}
		}

		/// <summary>
		/// 現在のデータを保存します.
		/// </summary>
		/// <param name="filepath"></param>
		public void Save(string filepath)
		{
			if(Encoder == null)
			{
				throw new NullReferenceException();
			}
			Encoder.BeginEncode(filepath);
			for(int i=0; i<eventOrder.Length; i++)
			{
				float key = eventOrder[i];
				VisualLayer layer = layerListSet[key];
				Encoder.BeginLayer(layer, key);
				for(int j=0; j<layer.Items.Count; j++)
				{
					Encoder.EncodeContent(layer.Items[j]);
				}
				Encoder.EndLayer(layer, key);
			}
			Encoder.EndEncode(filepath);
		}

		/// <summary>
		/// 現在のレイヤーを初期化してデータを読み込みます.
		/// </summary>
		/// <param name="filepath"></param>
		public void Load(string filepath)
		{
			if(Decoder == null)
			{
				throw new NullReferenceException();
			}
			Clear();
			Decoder.BeginDecode(filepath);
			while(!Decoder.IsEndOfStream)
			{
				KeyValuePair<float, VisualContent> pair = Decoder.DecodeContent();
				this.LayerDepth = pair.Key;
				layerListSet[pair.Key].Items.Add(pair.Value);
			}
			Decoder.EndDecode(filepath);
		}

		//
		//コールバック
		//

		private void PictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			Dispatch((layer) => layer.MouseDown(sender, e));
		}

		private void PictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			Dispatch((layer) => layer.MouseUp(sender, e));
		}

		private void PictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			Dispatch((layer) => layer.MouseMove(sender, e));
		}
		
		private void Dispatch(Action<VisualLayer> a)
		{
			if(eventOrder == null)
			{
				return;
			}
			for(int i = 0; i < eventOrder.Length; i++)
			{
				float key = eventOrder[i];
				VisualLayer layer = layerListSet[key];
				if(!IsDetect(layer, key))
				{
					continue;
				}
				a(layer);
			}
		}
		
		private bool IsDetect(VisualLayer layer, float depth)
		{
			switch(layer.Mask)
			{
				case EventMask.AlwaysDetect:
					return true;
				case EventMask.AlwaysNotDetect:
					return false;
				case EventMask.DetectSelected:
					return depth == LayerDepth;
				case EventMask.DetectFronBack:
					return depth >= LayerDepth;
				case EventMask.DetectFromFront:
					return depth <= LayerDepth;
				default:
					throw new ArgumentException();
			}
		}

		private void PictureBox_Paint(object sender, PaintEventArgs e)
		{
			if(eventOrder == null)
			{
				return;
			}
			for(int i=0; i<eventOrder.Length; i++)
			{
				float key = eventOrder[i];
				VisualLayer layer = layerListSet[key];
				if(key == LayerDepth)
				{
					layer.Draw(e.Graphics);
					break;
				} else
				{
					for(int j=0; j<layer.Items.Count; j++)
					{
						layer.Items[j].Draw(e.Graphics);
					}
				}
			}
		}

		private void Layer_OnInvalidate(object sender, Rectangle e)
		{
			pictureBox?.Invalidate(e);
		//	pictureBox?.Refresh();
		}

		private void Layer_OnRefresh(object sender, EventArgs e)
		{
			pictureBox?.Refresh();
		}
	}
}
