using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// UIビルダのデータを保存するインターフェイスです.
	/// </summary>
	public interface VisualEncoder : VisualStream
	{
		/// <summary>
		/// エンコードを開始します.
		/// </summary>
		/// <param name="filepath"></param>
		void BeginEncode(string filepath);

		/// <summary>
		/// これから保存/読込するオブジェクトの一覧を持つレイヤーを訪問します.
		/// </summary>
		/// <param name="layer"></param>
		/// <param name="layerDepth"></param>
		void BeginLayer(VisualLayer layer, float layerDepth);


		/// <summary>
		/// オブジェクトを保存します.
		/// </summary>
		/// <param name="content"></param>
		void EncodeContent(VisualContent content);

		/// <summary>
		/// オブジェクトの一覧の保存/読込を終了します.
		/// </summary>
		/// <param name="layer"></param>
		/// <param name="layerDepth"></param>
		void EndLayer(VisualLayer layer, float layerDepth);
		
		/// <summary>
		/// エンコードを終了します.
		/// </summary>
		/// <param name="filepath"></param>
		void EndEncode(string filepath);
	}
}
