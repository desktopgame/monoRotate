using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// UIビルダのデータを読み込むインターフェイスです.
	/// </summary>
	public interface VisualDecoder : VisualStream
	{
		/// <summary>
		/// ストリームの終端についたならtrue.
		/// </summary>
		bool IsEndOfStream { get; }

		/// <summary>
		/// デコードを開始します.
		/// </summary>
		/// <param name="filepath"></param>
		void BeginDecode(string filepath);

		/// <summary>
		/// オブジェクトを読み込みます
		/// </summary>
		KeyValuePair<float, VisualContent> DecodeContent();

		/// <summary>
		/// デコードを終了します.
		/// </summary>
		/// <param name="filepath"></param>
		void EndDecode(string filepath);
	}
}
