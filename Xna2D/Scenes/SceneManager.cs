using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xna2D.Contents;
using Xna2D.Utilities;

namespace Xna2D.Scenes
{
	/// <summary>
	/// シーンを識別子と紐づけて一括で管理するクラスです.
	/// </summary>
	public class SceneManager
	{
		public IScene this[int key]
		{
			set { this.sceneDictionary[key] = value; }
			get { return sceneDictionary[key]; }
		}

		public int Current
		{
			set; get;
		}

		private Dictionary<int, IScene> sceneDictionary;
		private FrameTimer timer;

		public SceneManager()
		{
			this.sceneDictionary = new Dictionary<int, IScene>();
			this.Current = -1;
		}

		/// <summary>
		/// 現在のシーンを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			if(timer != null)
			{
				if(!timer.Update().Elapsed())
				{
					return;
				}
				this.timer = null;
			}
			if(!sceneDictionary.ContainsKey(Current))
			{
				return;
			}
			sceneDictionary[Current].Update(gameTime);
			if(sceneDictionary[Current].IsEnd)
			{
				sceneDictionary[Current].Hide();
				this.Current = sceneDictionary[Current].Next;
				sceneDictionary[Current].Show();
				this.timer = new FrameTimer(10);
			}
		}

		/// <summary>
		/// 現在のシーンを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		public void Draw(GameTime gameTime, Renderer renderer)
		{
			if(!sceneDictionary.ContainsKey(Current))
			{
				return;
			}
			sceneDictionary[Current].Draw(gameTime, renderer);
		}
	}
}
