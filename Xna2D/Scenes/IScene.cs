using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xna2D.Contents;

namespace Xna2D.Scenes
{
	/// <summary>
	/// 画面の単位を定義します.
	/// </summary>
	public interface IScene
	{
		/// <summary>
		/// このシーンが終了したならtrue.
		/// </summary>
		bool IsEnd { get; }

		/// <summary>
		/// 次のシーンの識別子を返します.
		/// </summary>
		int Next { get; }

		/// <summary>
		/// このシーンを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		void Update(GameTime gameTime);

		/// <summary>
		/// このシーンを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		void Draw(GameTime gameTime, Renderer renderer);

		/// <summary>
		/// このシーンが表示されるときに呼ばれます.
		/// </summary>
		void Show();

		/// <summary>
		/// このシーンが非表示にされるときに呼ばれます.
		/// </summary>
		void Hide();
	}
}
