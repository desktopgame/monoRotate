using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Utilities;

namespace TestGame.Scenes
{
	/// <summary>
	/// 点滅処理を実装するためのタイマーを返します.
	/// </summary>
	public class BlinkTimer
	{
		private FrameTimer timer;
		/// <summary>
		/// タイマーが回った回数.
		/// </summary>
		public int Count
		{
			set; get;
		}

		public BlinkTimer()
		{
			this.timer = new FrameTimer(10);
			this.Count = 0;
		}

		/// <summary>
		/// タイマーを初期化します.
		/// </summary>
		public void Clear()
		{
			this.timer.Clear();
			this.Count = 0;
		}

		/// <summary>
		/// タイマーを更新します.
		/// </summary>
		public void Update()
		{
			if(timer.Update().Elapsed())
			{
				this.Count++;
			}
		}

		/// <summary>
		/// 透明度を返します.
		/// </summary>
		/// <returns></returns>
		public float GetAlpha()
		{
			return Count % 2 == 0 ? 0.5f : 1f;
		}
	}
}
