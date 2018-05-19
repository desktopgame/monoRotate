using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Game
{
	/// <summary>
	/// オブジェクトが描画をするたびにカメラのアングルを確認して描画するのは
	/// ループの回数が多くなって負荷がかかる(かもしれない)ので、
	/// アングルが変更されたらそれを監視するために実装出来ます。
	/// </summary>
	public interface ICameraObsever
	{
		/// <summary>
		/// アングルが変更されると呼ばれます.
		/// </summary>
		/// <param name="newAngle"></param>
		void AngleChanged(Camera.Angle newAngle);
	}
}
