using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game {
	/// <summary>
	/// 互換性のために残されているクラスに付与する属性です.
	/// この属性が付いたクラスは正常に動作しないか既にシステムとして廃止されたものです。
	/// そのようなクラスを残しておく必要があるのは、以前のステージをロード可能にするため
	/// つまり互換性のためです。
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	public class Compatibility : Attribute {
	}
}
