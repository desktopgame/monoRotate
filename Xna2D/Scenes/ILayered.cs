using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Scenes
{
	/// <summary>
	/// LayeredSceneによって包含されるシーンにおいて、必要に応じてレイヤを無視したい場合に実装します.
	/// </summary>
	public interface ILayered
	{
		/// <summary>
		/// このシーンより先に処理するレイヤーの処理が不要ならfalseを返します.
		/// </summary>
		/// <param name="scene"></param>
		/// <returns></returns>
		bool IsNeedBackLayer(LayeredScene scene);

		/// <summary>
		/// このシーンより後に処理するレイヤーの処理が不要ならfalseを返します.
		/// </summary>
		/// <param name="scene"></param>
		/// <returns></returns>
		bool IsNeedFrontLayer(LayeredScene scene);
	}
}
