using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualEditorAPI
{
	/// <summary>
	/// マウスイベントを監視するリスナーです.
	/// </summary>
	public interface MouseListener
	{
		/// <summary>
		/// マウスが押されると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MouseDown(object sender, MouseEventArgs e);

		/// <summary>
		/// マウスが離されると呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MouseUp(object sender, MouseEventArgs e);

		/// <summary>
		/// マウスが動くと呼ばれます.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void MouseMove(object sender, MouseEventArgs e);
	}
}
