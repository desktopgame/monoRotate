using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// ドラッグ時の挙動を示す列挙型.
	/// </summary>
	public enum DragMode
	{
		/// <summary>
		/// オブジェクトの配置.
		/// </summary>
		Draw,

		/// <summary>
		/// 選択.
		/// </summary>
		Select
	}
}
