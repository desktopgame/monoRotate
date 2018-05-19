using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// IDと紐づけて使用されるファクトリです.
	/// </summary>
	public interface IGameObjectTile : IMapEditorSupport
	{
		/// <summary>
		/// ID.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// 新しいインスタンスを生成します.
		/// </summary>
		/// <returns></returns>
		IGameObject NewInstance();
	}
}
