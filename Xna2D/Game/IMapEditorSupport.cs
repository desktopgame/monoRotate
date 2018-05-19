using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// マップエディタで編集可能なオブジェクトが実装するインターフェイス.
	/// </summary>
	public interface IMapEditorSupport : IGameData
	{
		/// <summary>
		/// 実行時ディレクトリからの相対パスでこのオブジェクトを表示するための画像を示します.
		/// </summary>
		string Path { get; }

		/// <summary>
		/// このデータの持つプロパティを書き換えるためのエディターが提供されるとき、
		/// そのセルエディターの種類を設定するために呼び出されます。
		/// 例えば整数型の値を編集する場合にはスピナーが、真偽値型の値を編集する場合にはチェックボックスが割り当てられます。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		Type GetContentType(string key);

		/// <summary>
		/// このデータのプロパティを書き換えるためのエディターが提供されるとき、
		/// 一部のプロパティを書き換え不可能に設定するために呼び出されます。
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool IsReadOnly(string key);
	}
}
