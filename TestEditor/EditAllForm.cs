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
	public partial class EditAllForm : Form
	{
		public EditAllForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンボボックスのアイテム一覧を設定します.
		/// </summary>
		/// <param name="items"></param>
		public void SetKeyList(string[] items)
		{
			comboBox.Items.Clear();
			comboBox.Items.AddRange(items);
			comboBox.SelectedIndex = 0;
		}

		/// <summary>
		/// 選択されているキーを返します.
		/// </summary>
		/// <returns></returns>
		public string GetSelectedKey()
		{
			return (string)comboBox.SelectedItem;
		}

		/// <summary>
		/// 値を文字列で返します.
		/// </summary>
		/// <returns></returns>
		public string GetInputValue()
		{
			return textBox.Text;
		}
	}
}
