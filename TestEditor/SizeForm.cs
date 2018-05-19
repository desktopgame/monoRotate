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
	public partial class SizeForm : Form
	{
		public SizeForm()
		{
			InitializeComponent();
		}

		public int GetWidth()
		{
			return (int)widthNumericUpdown.Value;
		}

		public int GetHeight()
		{
			return (int)heightNumericUpdown.Value;
		}
	}
}
