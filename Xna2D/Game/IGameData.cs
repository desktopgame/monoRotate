using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// 保存/読み込みの可能なオブジェクトです.
	/// </summary>
	public interface IGameData
	{
		/// <summary>
		/// このオブジェクトのID.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// このオブジェクトが読み込まれた直後に呼ばれます.
		/// フィールドにデフォルトの値を設定します.
		/// </summary>
		/// <param name="id"></param>
		void Initialize(int id);

		/// <summary>
		/// ファイルの値をフィールドへ適用します.
		/// </summary>
		/// <param name="d"></param>
		void Read(Dictionary<string, string> d);

		/// <summary>
		/// フィールドの値をファイルへ適用します.
		/// </summary>
		/// <param name="d"></param>
		void Write(Dictionary<string, string> d);
	}
}
