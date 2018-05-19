using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// ドラッグ時に生成されるコンテンツのファクトリです.
	/// </summary>
	public interface VisualContentFactory
	{
		/// <summary>
		/// 生成されるオブジェクトの横幅.
		/// </summary>
		int Width { get; }

		/// <summary>
		/// 生成されるオブジェクトの縦幅.
		/// </summary>
		int Height { get; }

		/// <summary>
		/// 新しいインスタンスを生成します.
		/// </summary>
		/// <returns></returns>
		VisualContent NewInstance();
	}
}
