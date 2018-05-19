using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualEditorAPI
{
	/// <summary>
	/// レイヤーのイベントの透過性を決める列挙型.
	/// </summary>
	public enum EventMask
	{
		/// <summary>
		/// 常にイベントを傍受する場合に使用出来ます.
		/// </summary>
		AlwaysDetect,

		/// <summary>
		/// 常にイベントを傍受しない場合に使用出来ます.
		/// </summary>
		AlwaysNotDetect,

		/// <summary>
		/// 自身が選択されている場合のみ傍受する場合に使用出来ます.
		/// </summary>
		DetectSelected,

		/// <summary>
		/// 自身或いはより上層のレイヤーで発生したイベントのみ傍受する場合に使用出来ます.
		/// </summary>
		DetectFromFront,

		/// <summary>
		/// 自身或いはより下層のレイヤーで発生したイベントのみ傍受する場合に使用出来ます.
		/// </summary>
		DetectFronBack
	}
}
