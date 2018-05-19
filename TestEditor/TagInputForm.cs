using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestEditor
{
	public partial class TagInputForm : Form
	{
		private List<string> keyList;
		public TagInputForm()
		{
			InitializeComponent();
			this.keyList = new List<string>();
		}

		/// <summary>
		/// キー一覧を更新します.
		/// 生成されるキーはこれと重複しないようになります。
		/// </summary>
		/// <param name="keys"></param>
		public void SetKeyList(string[] keys)
		{
			keyList.Clear();
			keyList.AddRange(keys);
		}

		/// <summary>
		/// 生成されたタグを返します.
		/// </summary>
		/// <returns></returns>
		public string GetGeneratedTag()
		{
			return textBox.Text;
		}

		private void xLayoutCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			UpdateText();
		}

		private void yLayoutCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			UpdateText();
		}

		private void UpdateText()
		{
			bool checkX = xLayoutCheckBox.Checked;
			bool checkY = yLayoutCheckBox.Checked;
			string baseKey = "Layout";
			if(checkX) baseKey += "X";
			if(checkY) baseKey += "Y";
			int count = 0;
			//キーが重複する限りナンバリング
			string newKey = baseKey;
			while(keyList.Contains(newKey))
			{
				newKey = baseKey + (count++);
			}
			textBox.Text = newKey;
		}
	}
}
