using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xna2D.Utilities;

namespace Xna2D.Contents
{
	/// <summary>
	/// テクスチャを内包するSpriteBatchのラッパーです.
	/// </summary>
	public class Renderer
	{
		/// <summary>
		/// 指定のパスのテクスチャを返します.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public Texture2D this[string path]
		{
			get { return textureDictionary[path]; }
		}
		private ContentManager contentManager;
		private SpriteBatch spriteBatch;
		private Texture2D emptyTexture;
		private Dictionary<string, Texture2D> textureDictionary;

		public static readonly float LAYER_BACK = 0f;
		public static readonly float LAYER_FRONT = 1f;
		
		public Renderer(ContentManager contentManager, SpriteBatch spriteBatch)
		{
			this.contentManager = contentManager;
			this.spriteBatch = spriteBatch;
			this.textureDictionary = new Dictionary<string, Texture2D>();
		}

		public Renderer(ContentManager contentManager, GraphicsDevice graphicsDevice)
			: this(contentManager, new SpriteBatch(graphicsDevice))
		{
		}

		/// <summary>
		/// 指定のアセットを画像として読み込みます.
		/// </summary>
		/// <param name="assetName"></param>
		public void Load(string assetName)
		{
			Texture2D texture = contentManager.Load<Texture2D>(assetName);
			textureDictionary[assetName] = texture;
		}

		/// <summary>
		/// 全てのリソースを開放します.
		/// </summary>
		public void Unload()
		{
			textureDictionary.Clear();
		}

		/// <summary>
		/// 画像の描画を開始します.
		/// </summary>
		public void Begin()
		{
			spriteBatch.Begin();
		}

		/// <summary>
		/// 指定の行列を使用して描画します.
		/// </summary>
		/// <param name="transform"></param>
		public void Begin(Matrix transform) {
			spriteBatch.Begin(SpriteSortMode.BackToFront,
				BlendState.AlphaBlend,
				null,
				null,
				null,
				null,
				transform
			);
		}

		/// <summary>
		/// 空のテクスチャを返します.
		/// </summary>
		/// <returns></returns>
		protected virtual Texture2D GetEmptyTexture()
		{
			if(emptyTexture != null)
			{
				return emptyTexture;
			}
			this.emptyTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
			emptyTexture.SetData(new Color[] { Color.White });
			return emptyTexture;
		}

		/// <summary>
		/// 線分を描画します.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="color"></param>
		public void DrawLine(Vector2 from, Vector2 to, Color color)
		{
			float length = (to - from).Length();
			float rotation = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
			spriteBatch.Draw(GetEmptyTexture(), from, null, color, rotation, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
		}

		/// <summary>
		/// 矩形を描画します.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void DrawRectangle(Rectangle rect, Color color)
		{
			Texture2D nullTexture = GetEmptyTexture();
			spriteBatch.Draw(nullTexture, new Rectangle(rect.X, rect.Y, rect.Width, 1), color);
			spriteBatch.Draw(nullTexture, new Rectangle(rect.X, rect.Y + rect.Height, rect.Width, 1), color);
			spriteBatch.Draw(nullTexture, new Rectangle(rect.X, rect.Y, 1, rect.Height), color);
			spriteBatch.Draw(nullTexture, new Rectangle(rect.X + rect.Width, rect.Y, 1, rect.Height), color);
		}

		/// <summary>
		/// 矩形を塗りつぶします.
		/// </summary>
		/// <param name="rect"></param>
		/// <param name="color"></param>
		public void FillRectangle(Rectangle rect, Color color)
		{
			spriteBatch.Draw(GetEmptyTexture(), rect, color);
		}

		/// <summary>
		/// 全てのパラメータを指定して描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="sourceRect"></param>
		/// <param name="color"></param>
		/// <param name="rotation"></param>
		/// <param name="origin"></param>
		/// <param name="scale"></param>
		/// <param name="effects"></param>
		/// <param name="layerDepth"></param>
		public void Draw(string assetName, Vector2 position, Rectangle? sourceRect, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			spriteBatch.Draw(textureDictionary[assetName], position, sourceRect, color, rotation, origin, scale, effects, layerDepth);
		}

		/// <summary>
		/// 切り抜き範囲を指定して描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="sourceRect"></param>
		/// <param name="color"></param>
		public void Draw(string assetName, Vector2 position, Rectangle? sourceRect, Color color)
		{
			Draw(assetName, position, sourceRect, color, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, LAYER_BACK);
		}

		/// <summary>
		/// 透明度を指定して描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="alpha"></param>
		public void Draw(string assetName, Vector2 position, Color color, float alpha)
		{
			spriteBatch.Draw(textureDictionary[assetName], position, color * alpha);
		}

		/// <summary>
		/// 完全な不透明によって描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		public void Draw(string assetName, Vector2 position, Color color)
		{
			Draw(assetName, position, color, 1f);
		}
		
		/// <summary>
		/// 倍率を指定して描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="scale"></param>
		public void Draw(string assetName, Vector2 position, Vector2 scale)
		{
			Draw(assetName, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, LAYER_BACK);
		}

		/// <summary>
		/// 回転を指定して描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="rotate"></param>
		/// <param name="origin"></param>
		public void Draw(string assetName, Vector2 position, float rotate, Vector2 origin)
		{
			Draw(
				assetName,
				position,
				null,
				Color.White,
				rotate,
				Math.Abs(rotate) < 1f ? Vector2.Zero : origin,
				new Vector2(1, 1),
				SpriteEffects.None,
				1f
			);
		}

		/// <summary>
		/// 回転と切り抜き範囲を指定して描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="sourceRect"></param>
		/// <param name="rotate"></param>
		/// <param name="origin"></param>
		public void Draw(string assetName, Vector2 position, Rectangle? sourceRect, float rotate, Vector2 origin)
		{
			Draw(
				assetName,
				position, 
				sourceRect,
				Color.White,
				rotate,
				origin,
				new Vector2(1, 1),
				SpriteEffects.None,
				1f
			);
		}

		/// <summary>
		/// 指定の設定で画像を描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="option"></param>
		public void Draw(string assetName, SpriteBatchOption option)
		{
			option.Draw(spriteBatch, textureDictionary[assetName]);
		}

		/// <summary>
		/// 数字を描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="number"></param>
		public void DrawNumber(string assetName, Vector2 position, Color color, int number)
		{
			if(number < 0)
			{
				number = 0;
			}
			string numberStr = number.ToString();
			Vector2 basePoint = position;
			Texture2D texture = textureDictionary[assetName];
			int characterSize = texture.Width / 10;
			Rectangle rect = new Rectangle(0, 0, characterSize, texture.Height);
			for(int i = 0; i < numberStr.Length; i++)
			{
				int numberAt = int.Parse(numberStr.Substring(i, 1));
				//Draw(renderer, basePoint, color, rotate, origin, scale, effect, layerDepth, numberAt);
				rect.X = numberAt * characterSize;
				Draw(assetName, basePoint, rect, color);
				basePoint.X += characterSize;
			}
		}

		/// <summary>
		/// 数字と切り抜き範囲の対応を配列で渡して、より厳密に描画します.
		/// </summary>
		/// <param name="assetName"></param>
		/// <param name="position"></param>
		/// <param name="color"></param>
		/// <param name="number"></param>
		/// <param name="rectangles"></param>
		public void DrawNumber(string assetName, Vector2 position, Color color, int number, Rectangle[] rectangles)
		{
			if(number < 0)
			{
				number = 0;
			}
			Vector2 basePoint = position;
			string numberStr = number.ToString();
			for(int i=0; i<numberStr.Length; i++)
			{
				int index = int.Parse(numberStr.Substring(i, 1));
				Rectangle rect = rectangles[index];
				Draw(assetName, basePoint, rect, color);
				basePoint.X += rect.Width;
			}
		}

		/// <summary>
		/// 画像の描画を終了します.
		/// </summary>
		public void End()
		{
			spriteBatch.End();
		}
	}
}
