using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestEditor.VE;
using TestGame;
using TestGame.Scenes;
using VisualEditorAPI;
using Xna2D.Game;
using EditorUtilities;
using TestGame.Scenes.Play;

namespace TestEditor
{
	public partial class Form1 : Form
	{
		private SourceFile sourceFile;
		private Font listFont;
		private Dictionary<string, Image> imageDictionary;
		private VisualEditor visualEditor;
		private IGameObject editTarget;
		private static char[] ALPHA = "abcdefghijklmnopqrstuvwxyz".ToArray();
		private static char[] NUMS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		private static char[] ALNUM;
		private static readonly Random R = new Random();

		static Form1() {
			List<char> list = new List<char>();
			list.AddRange(ALPHA);
			list.AddRange(NUMS);
			ALNUM = list.ToArray();
		}

		public Form1()
		{
			InitializeComponent();
			TestGame.Program.SetupObjects();
			//全ての画像を読み込む
			this.listFont = new Font(FontFamily.GenericSansSerif, 12);
			this.imageDictionary = new Dictionary<string, Image>();
			LoadImages();
			SetupUI();
		}

		private void LoadImages()
		{
			DirectoryInfo res = new DirectoryInfo(Paths.RES);
			//	DirectoryInfo res = new DirectoryInfo(@"C:\Users\Koya\Desktop\SVN\Local\TestGame\v1\trunk\TestGame\TestGame\TestGameContent");
			Rename(res);
			LoadImages(res);
			//全てのオブジェクトをリストへ
			GameObjectRegistry reg = GameObjectRegistry.GetInstance();
			for(int i = 0; i < reg.Count; i++)
			{
				objectList.Items.Add(reg[i]());
			}
		}

		private void Rename(DirectoryInfo root)
		{
			//なんかアセット名が勝手に小文字になるので
			//根優先
			DirectoryInfo[] directories = root.GetDirectories();
			for(int i = 0; i < directories.Length; i++)
			{
				Rename(directories[i]);
			}
			FileInfo[] files = root.GetFiles("*.*");
			for(int i = 0; i < files.Length; i++)
			{
				FileInfo file = files[i];
				StringBuilder sb = new StringBuilder();
				string[] split = Path.GetFileNameWithoutExtension(file.Name).Split('_');
				Array.ForEach(split, sp =>
				{
					string head = sp.Substring(0, 1);
					string sub = sp.Substring(1);
					sb.Append(head.ToUpper() + sub);
					sb.Append('_');
				});
				string ext = Path.GetExtension(file.Name);
				sb.Remove(sb.Length - 1, 1);
				sb.Append(ext);
//				Debug.WriteLine(file.FullName + "   " + file.DirectoryName + Path.DirectorySeparatorChar + sb.ToString());
				file.MoveTo(file.DirectoryName + Path.DirectorySeparatorChar + sb.ToString());
			}
		}

		private void LoadImages(DirectoryInfo root)
		{
			//根優先
			DirectoryInfo[] directories = root.GetDirectories();
			for(int i = 0; i < directories.Length; i++)
			{
				LoadImages(directories[i]);
			}

			FileInfo[] files = root.GetFiles("*.*");
			for(int i=0; i<files.Length; i++)
			{
				FileInfo file = files[i];
				string env = Paths.RES;
				string fullName = file.FullName;
				string key = fullName.Remove(0, env.Length + 1).Replace(Path.DirectorySeparatorChar, '/');
				if(!fullName.EndsWith(".png"))
				{
					continue;
				}
				Image img = Bitmap.FromFile(fullName);
				imageDictionary[key] = img;
			}
		}
		
		private void SetupUI()
		{
			//APIのインストール
			this.visualEditor = new VisualEditor();
			visualEditor.Encoder = new VEncoder();
			visualEditor.Decoder = new VDecoder(imageDictionary, pictureBox);
			visualEditor.Install(pictureBox);
			//自分で描画する
			objectList.DrawMode = DrawMode.OwnerDrawFixed;
			objectList.DrawItem += ObjecList_DrawItem;
			objectList.ItemHeight = 32;
			objectList.SelectedIndexChanged += ObjecList_SelectedIndexChanged;
			layerComboBox.SelectedIndexChanged += LayerComboBox_SelectedIndexChanged;
			layerComboBox.SelectedIndex = 0;
			dataGridView.CellBeginEdit += DataGridView_CellBeginEdit;
			dataGridView.CellEndEdit += DataGridView_CellEndEdit;
			//編集先を決定
			this.sourceFile = new SourceFile("無題.text");
			this.Text = sourceFile.GetWindowTitle("TestEditor");
			sourceFile.OnPathChanged += (sender, e) => this.Text = sourceFile.GetWindowTitle("TestEditor");
		}

		//
		//コールバック
		//
		
		private void LayerComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(layerComboBox.SelectedIndex == -1)
			{
				return;
			}
			visualEditor.LayerDepth = float.Parse((string)layerComboBox.SelectedItem);
			visualEditor.Current.SelectionManager.OnBoundsUpdate -= SelectionManager_OnBoundsUpdate;
			visualEditor.Current.SelectionManager.OnBoundsUpdate += SelectionManager_OnBoundsUpdate;
		}

		private void SelectionManager_OnBoundsUpdate(object sender, EventArgs e)
		{
			//全ての行をクリア
			dataGridView.Rows.Clear();
			VisualContent[] contents = visualEditor.Current.GetSelectedContents();
			if(contents.Length == 0)
			{
				this.editTarget = null;
				return;
			}
			VisualContent content = contents[contents.Length - 1];
			VCImpl impl = content as VCImpl;
			IGameObject gobj =  impl.GameObject;
			this.editTarget = gobj;
			//現在の値を書き込んでもらう
			Dictionary<string, string> values = new Dictionary<string, string>();
			gobj.Write(values);
			impl.Inject();
			//キーは書き換え不可
			dataGridView.Columns[0].ReadOnly = true;
			//要素を書き込む
			foreach(KeyValuePair<string, string> pair in values)
			{
				dataGridView.Rows.Add(new object[] { pair.Key, pair.Value });
				DataGridViewCell current = dataGridView.Rows[dataGridView.RowCount - 1].Cells[1];
				Type contentType = gobj.GetContentType(pair.Key);
				if(contentType == null)
				{
					contentType = typeof(string);
				}
			//	current.ReadOnly = gobj.IsReadOnly(pair.Key);
				current.ValueType = contentType;
			}
		}

		private void DataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			//DataGridViewCell#ReadOnlyが動作しないのでこんなことに
			if(editTarget == null)
			{
				return;
			}
			object keyObj = dataGridView.Rows[e.RowIndex].Cells[0].Value;
			if(keyObj == null)
			{
				return;
			}
			//書き換え禁止なのでキャンセル
			string key = keyObj.ToString();
			if(editTarget.IsReadOnly(key))
			{
				e.Cancel = true;
			}
		}

		private void DataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if(editTarget == null)
			{
				return;
			}
			Dictionary<string, string> d = new Dictionary<string, string>();
			editTarget.Write(d);
			object keyObj = dataGridView.Rows[e.RowIndex].Cells[0].Value;
			object valObj = dataGridView.Rows[e.RowIndex].Cells[1].Value;
			if(keyObj == null || valObj == null)
			{
				return;
			}
			d[keyObj.ToString()] = valObj.ToString();
			editTarget.Read(d);
		}

		private void ObjecList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(objectList.SelectedIndex == -1)
			{
				return;
			}
			IGameObject gObj = objectList.Items[objectList.SelectedIndex] as IGameObject;
			visualEditor.Factory = new VCFactoryImpl(gObj, imageDictionary[gObj.Path + ".png"]);
		}

		private void ObjecList_DrawItem(object sender, DrawItemEventArgs e)
		{
			IGameObject gObj = objectList.Items[e.Index] as IGameObject;
			e.DrawBackground();
			Graphics g = e.Graphics;
			System.Drawing.Rectangle bounds = e.Bounds;
			System.Drawing.Rectangle dstRect = new System.Drawing.Rectangle(bounds.Location + bounds.Size - new Size(32, 32), new Size(32, 32));
			//g.DrawImage(null, new Rectangle(), new Rectangle(), )
			g.DrawString(gObj.Id.ToString(), listFont, Brushes.Black, bounds.Location);
			g.DrawImage(imageDictionary[gObj.Path + ".png"], dstRect, new System.Drawing.Rectangle(0, 0, 32, 32), GraphicsUnit.Pixel);
			e.DrawFocusRectangle();
		}

		private void drawRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if(drawRadioButton.Checked)
			{
				visualEditor.DragMode = DragMode.Draw;
			}
		}

		private void selectRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			if(selectRadioButton.Checked)
			{
				visualEditor.DragMode = DragMode.Select;
			}
		}

		//
		//選択領域
		//
		
		private void minXFlattenButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.FlattenX((nums) => nums.Min());
		}

		private void avgXFlattenButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.FlattenX((nums) => (int)Math.Round(nums.Average()));
		}

		private void maxXFlattenButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.FlattenX((nums) => nums.Max());
		}

		private void minYFlattenButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.FlattenY((nums) => nums.Max());
		}

		private void avgYFlattenButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.FlattenY((nums) => (int)Math.Round(nums.Average()));
		}

		private void maxYFlattenButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.FlattenY((nums) => nums.Min());
		}
		
		private void removeButton_Click(object sender, EventArgs e)
		{
			visualEditor.Current.Items.RemoveAll((elem) => elem.IsSelected);
		}

		private void gridButton_Click(object sender, EventArgs e)
		{
			int index = objectList.SelectedIndex;
			if(index == -1)
			{
				return;
			}
			IGameObject obj = (IGameObject)objectList.Items[index];
			for(int i=0; i<visualEditor.Size.Height; i += (int)obj.Height)
			{
				for(int j=0; j<visualEditor.Size.Width; j += (int)obj.Width)
				{
					VisualContent clone = visualEditor.Factory.NewInstance();
					clone.Y = i;
					clone.X = j;
					visualEditor.Current.Items.Add(clone);
				//	Debug.WriteLine(i + "/" + j);
				}
			}
		}
		
		private void frameButton_Click(object sender, EventArgs e)
		{
			int index = objectList.SelectedIndex;
			if(index == -1)
			{
				return;
			}
			IGameObject obj = (IGameObject)objectList.Items[index];
			//上
			for(int i=0; i<visualEditor.Size.Width; i += (int)obj.Width)
			{
				VisualContent clone = visualEditor.Factory.NewInstance();
				clone.Y = 0;
				clone.X = i;
				visualEditor.Current.Items.Add(clone);
			}
			//下
			for(int i=0; i<visualEditor.Size.Width; i += (int)obj.Width)
			{
				VisualContent clone = visualEditor.Factory.NewInstance();
				clone.Y = visualEditor.Size.Height - clone.Height;
				clone.X = i;
				visualEditor.Current.Items.Add(clone);
			}
			//左
			for(int i = 0; i < visualEditor.Size.Height; i += (int)obj.Height)
			{
				VisualContent clone = visualEditor.Factory.NewInstance();
				clone.Y = i;
				clone.X = 0;
				visualEditor.Current.Items.Add(clone);
			}
			//右
			for(int i = 0; i < visualEditor.Size.Height; i += (int)obj.Height)
			{
				VisualContent clone = visualEditor.Factory.NewInstance();
				clone.Y = i;
				clone.X = visualEditor.Size.Width - clone.Width;
				visualEditor.Current.Items.Add(clone);
			}
		}

		private void editAllButton_Click(object sender, EventArgs e)
		{
			VisualContent[] selectedItems = visualEditor.Current.GetSelectedContents();
			if(selectedItems.Length == 0)
			{
				return;
			}
			//選択されているオブジェクト全てのキー一覧をコンボボックスへ適用
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> keyList = new List<string>();
			Array.ForEach(selectedItems, item =>
			{
				IGameObject gObj = AsGameObject(item);
				gObj.Write(dictionary);
				foreach(KeyValuePair<string, string> pair in dictionary)
				{
					if(!keyList.Contains(pair.Key))
					{
						keyList.Add(pair.Key);
					}
				}
				dictionary.Clear();
			});
			EditAllForm editAllForm = new EditAllForm();
			editAllForm.SetKeyList(keyList.ToArray());
			DialogResult res = editAllForm.ShowDialog();
			if(res != DialogResult.OK)
			{
				return;
			}
			//選択されているコンテンツに適用する
			string key = editAllForm.GetSelectedKey();
			string val = editAllForm.GetInputValue();
			Array.ForEach(selectedItems, item =>
			{
				IGameObject gObj = AsGameObject(item);
				gObj.Write(dictionary);
				dictionary[key] = val;
				gObj.Read(dictionary);
				((VCImpl)item).Inject();
				dictionary.Clear();
			});
			pictureBox.Refresh();
		}

		private string RandomString()
		{
			StringBuilder sb = new StringBuilder();
			for(int i=0; i<10; i++)
			{
				sb.Append(ALNUM[R.Next(0, ALNUM.Length)]);
			}
			return sb.ToString();
		}

		//
		//ユーティリティ
		//

		private void Open()
		{
			openFileDialog.InitialDirectory = Environment.CurrentDirectory;
			openFileDialog.AddExtension = true;
			openFileDialog.DefaultExt = "text";
			DialogResult res = openFileDialog.ShowDialog();
			if(res != DialogResult.OK)
			{
				return;
			}
			//List<IGameData> dataList = GIO.Load(openFileDialog.FileName);
			//	drawItems.Clear();
			//	drawItems.AddRange(dataList.Where((elem) => elem is IGameObject).Select((elem) => new SelectableItem<IGameObject>(elem as IGameObject)));
			visualEditor.Load(openFileDialog.FileName);
			pictureBox.Refresh();
		}
		
		private void SizeEdit() {
			SizeForm sizeForm = new SizeForm();
			DialogResult res = sizeForm.ShowDialog();
			if(res != DialogResult.OK)
			{
				return;
			}
			pictureBox.Size = NewSize(sizeForm.GetWidth(), sizeForm.GetHeight());
			pictureBox.Refresh();
		}

		private IGameObject AsGameObject(VisualContent content)
		{
			VCImpl impl = content as VCImpl;
			return impl.GameObject;
		}

		private System.Drawing.Size NewSize(int w, int h)
		{
			return new System.Drawing.Size(w, h);
		}

		#region メニュー処理
		private void newMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				sourceFile.New("無題.text");
				visualEditor.Clear();
			} catch(NotSavedSourceException nsse)
			{
				DialogResult result = MessageBox.Show("", "変更を破棄しますか？", MessageBoxButtons.YesNo);
				if(result == DialogResult.Yes)
				{
					sourceFile.Clear();
					newMenuItem_Click(sender, e);
				}
			}
		}

		private void openMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				sourceFile.Throw();
				openFileDialog.InitialDirectory = Environment.CurrentDirectory;
				DialogResult result = openFileDialog.ShowDialog();
				if(result != DialogResult.OK)
				{
					return;
				}
				sourceFile.Open(openFileDialog.FileName);
				visualEditor.Load(openFileDialog.FileName);
			} catch(NotSavedSourceException nsse)
			{
				DialogResult result = MessageBox.Show("", "変更を破棄しますか？", MessageBoxButtons.YesNo);
				if(result == DialogResult.Yes)
				{
					sourceFile.Clear();
					openMenuItem_Click(sender, e);
				}
			}
		}

		private void saveAsMenuItem_Click(object sender, EventArgs e)
		{
			saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
			saveFileDialog.AddExtension = true;
			saveFileDialog.DefaultExt = "text";
			DialogResult res = saveFileDialog.ShowDialog();
			if(res != DialogResult.OK)
			{
				return;
			}
			sourceFile.SaveAs(saveFileDialog.FileName);
			saveMenuItem_Click(sender, e);
		}

		private void saveMenuItem_Click(object sender, EventArgs e)
		{
			if(!sourceFile.Exists)
			{
				saveAsMenuItem_Click(sender, e);
			}
			//カメラオブジェクトを検索
			VisualLayer layer = visualEditor[0.0f];
			IEnumerator<VisualContent> cameraEnums = layer.Items.Where((elem) => elem is Camera).GetEnumerator();
			Camera camera = cameraEnums.MoveNext() ? (Camera)((VCImpl)cameraEnums.Current).GameObject : null;
			//タイムオブジェクトを検索
			layer = visualEditor[1.0f];
			IEnumerator<VisualContent> timeEnums = layer.Items.Where((elem) => elem is TimeObject).GetEnumerator();
			TimeObject time = timeEnums.MoveNext() ? (TimeObject)((VCImpl)timeEnums.Current).GameObject : null;
			//カメラが含まれないので追加
			if(camera == null)
			{
				camera = new Camera("Textures/CameraObject");
				camera.Initialize(1);
				layer.Items.Add(new VCImpl(camera, imageDictionary[camera.Path + ".png"]));
			}
			//タイムが含まれないので追加
			if(time == null)
			{
				time = new TimeObject("Textures/TimeObject");
				time.Initialize(2);
				time.MaximumTime = 200;
				layer.Items.Add(new VCImpl(time, null));
			}
			//カメラ設定
			camera.ScrollWidth = pictureBox.Width;
			camera.ScrollHeight = pictureBox.Height;
			camera.VisibleWidth = 1280;
			camera.VisibleHeight = 720;
			sourceFile.Save();
			visualEditor.Save(sourceFile.Path);
		}

		private void cutMenuItem_Click(object sender, EventArgs e)
		{
			visualEditor.Current.Cut();
		}

		private void pasteMenuItem_Click(object sender, EventArgs e)
		{
			visualEditor.Current.Paste();
		}

		private void copyMenuItem_Click(object sender, EventArgs e)
		{
			visualEditor.Current.Copy();
		}

		private void undoMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void redoMenuItem_Click(object sender, EventArgs e)
		{

		}
		private void sizeEditMenuItem_Click(object sender, EventArgs e)
		{
			SizeEdit();
		}
		#endregion


		#region 廃止

		//廃止
		private void tagClearButton_Click(object sender, EventArgs e) {
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Array.ForEach(visualEditor.LayerHierarchy, i => {
				VisualLayer layer = visualEditor[i];
				for(int j = 0; j < layer.Items.Count; j++) {
					IGameObject gObj = AsGameObject(layer.Items[j]);
					gObj.Write(dictionary);
					dictionary["Tag"] = "null";
					gObj.Read(dictionary);
					dictionary.Clear();
				}
			});
		}

		private void tagSelectButton_Click(object sender, EventArgs e) {
			VisualContent[] selectedItems = visualEditor.Current.GetSelectedContents();
			if(selectedItems.Length == 0) {
				return;
			}
			List<string> keyList = new List<string>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			//全てのレイヤーの
			Array.ForEach(visualEditor.LayerHierarchy, i => {
				//全てのアイテムの
				VisualLayer layer = visualEditor[i];
				Debug.WriteLine("Layer -> " + i);
				for(int j = 0; j < layer.Items.Count; j++) {
					//タグ要素をリストへ追加
					IGameObject gObj = AsGameObject(layer.Items[j]);
					gObj.Write(dictionary);
					string eKey = dictionary["Tag"];
					Debug.WriteLine(eKey);
					if(!keyList.Contains(eKey)) {
						keyList.Add(eKey);
					}
					dictionary.Clear();
				}
			});
			TagInputForm tagInputForm = new TagInputForm();
			tagInputForm.SetKeyList(keyList.ToArray());
			DialogResult res = tagInputForm.ShowDialog();
			if(res != DialogResult.OK) {
				return;
			}
			Array.ForEach(selectedItems, item => {
				IGameObject gObj = AsGameObject(item);
				gObj.Write(dictionary);
				dictionary["Tag"] = tagInputForm.GetGeneratedTag();
				gObj.Read(dictionary);
				dictionary.Clear();
			});
		}

		private void snapButton_Click(object sender, EventArgs e) {
			VisualContent[] items = visualEditor.Current.GetSelectedContents();
			Dictionary<string, string> di = new Dictionary<string, string>();
			string key = "";
			bool nameOk = true;
			bool breakFor = false;
			do {
				key = RandomString();
				for(int i = 0; i < visualEditor.Current.Items.Count; i++) {
					VisualContent content = visualEditor.Current.Items[i];
					VCImpl impl = content as VCImpl;
					IGameObject gObj = impl.GameObject;
					di.Clear();
					gObj.Write(di);
					breakFor = false;
					nameOk = true;
					foreach(KeyValuePair<string, string> pair in di) {
						if(pair.Key == "SnapID" && pair.Value == key) {
							breakFor = true;
							nameOk = false;
							break;
						}
					}
					if(breakFor) {
						break;
					}
				}
			} while(!nameOk);
			Array.ForEach(items, item => {
				di.Clear();
				VCImpl vc = item as VCImpl;
				IGameObject gObj = vc.GameObject;
				gObj.Write(di);
				di["SnapID"] = key;
				gObj.Read(di);
			});

		}

		#endregion
	}
}
