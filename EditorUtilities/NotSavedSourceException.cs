using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorUtilities
{
	/// <summary>
	/// 新規作成, 開くが実行されたときにまだ以前のデータが保存されていない場合に報告されます.
	/// </summary>
	public class NotSavedSourceException : Exception
	{
	}
}
