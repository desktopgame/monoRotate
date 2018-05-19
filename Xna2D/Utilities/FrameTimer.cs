using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xna2D.Utilities
{
	/// <summary>
	/// フレーム単位で時間を計測するタイマーです.
	/// </summary>
	public class FrameTimer
	{
		private int length;
		private int offset;
		public FrameTimer(int length)
		{
			this.length = length;
		}

		/// <summary>
		/// タイマーの位置を戻す.
		/// </summary>
		public void Clear()
		{
			this.offset = 0;
		}

		/// <summary>
		/// タイマーを更新します.
		/// </summary>
		/// <returns></returns>
		public FrameTimer Update()
		{
			if(offset++ == length)
			{
				this.offset = 0;
			}
			return this;
		}

		/// <summary>
		/// 指定の時間が経過したならtrue.
		/// </summary>
		/// <returns></returns>
		public bool Elapsed()
		{
			return offset == 0;
		}
	}
}
