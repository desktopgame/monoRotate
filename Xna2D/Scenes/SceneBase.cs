using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Scenes
{
	/// <summary>
	/// シーンの基底クラス.
	/// </summary>
	public class SceneBase : IScene
	{
		public bool IsEnd { protected set; get; }

		public int Next { protected set; get; }

		public virtual void Show()
		{
			this.IsEnd = false;
		}

		public virtual void Hide()
		{
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(GameTime gameTime, Renderer renderer)
		{
		}
	}
}
