using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Contents;

namespace Xna2D.Utilities
{
	/// <summary>
	/// 爆発エフェクト.
	/// </summary>
	public class Explode
	{
		/// <summary>
		/// 爆発エフェクトの描画が終了したならtrue.
		/// </summary>
		public bool IsComplete
		{
			get
			{
				for(int i = 0; i < width * height; i++)
				{
					if(tips[i].Time > 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		private int width;
		private int height;
		private Tip[] tips;

		public static readonly Random RANDOM = new Random();


		public Explode(int width, int height, Tip[] tips)
		{
			this.width = width;
			this.height = height;
			this.tips = tips;
		}

		/// <summary>
		/// 指定の画像から爆発エフェクトを生成します.
		/// </summary>
		/// <param name="texture"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public static Explode Create(Texture2D texture, Vector2 position)
		{
			int width = texture.Width;
			int height = texture.Height;
			//色情報を取得
			Color[] pixels = new Color[width * height];
			Tip[] tips = new Tip[width * height];
			texture.GetData<Color>(pixels);
			//チップを生成
			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
				{
					int index = i * height + j;
					Color pixel = pixels[index];
					float angle = RANDOM.Next(1, 360);
					float speed = 10 / RANDOM.Next(1, 30);
					Tip tip = new Tip(pixel);
					tip.PositionX = position.X + i;
					tip.PositionY = position.Y + j;
					//tip.AccelerationX = (float)Math.Cos(angle * Math.PI / 180) * speed;
					//tip.AccelerationY = (float)Math.Cos(angle * Math.PI / 180) * speed;
					//tip.AccelerationX = RANDOM.Next(-3, 4);
					//tip.AccelerationY = RANDOM.Next(-3, 4);
					tip.Time = RANDOM.Next(50, 100);
					tips[index] = tip;
				}
			}
			tips[(width * height) - 1].Time = 120;
			return new Explode(width, height, tips);
		}

		/// <summary>
		/// 全てのチップを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public void Update(GameTime gameTime)
		{
			for(int i = 0; i < width * height; i++)
			{
				tips[i].Update(gameTime);
			}
		}

		/// <summary>
		/// 全てのチップを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		public void Draw(GameTime gameTime, Renderer renderer)
		{
			for(int i = 0; i < width * height; i++)
			{
				tips[i].Draw(gameTime, renderer);
			}
		}
	}

	/// <summary>
	/// 爆発の一部.
	/// </summary>
	public class Tip
	{
		/// <summary>
		/// X座標.
		/// </summary>
		public float PositionX
		{
			set; get;
		}

		/// <summary>
		/// Y座標.
		/// </summary>
		public float PositionY
		{
			set; get;
		}

		/// <summary>
		/// X方向の加速度.
		/// </summary>
		public float AccelerationX
		{
			set; get;
		}

		/// <summary>
		/// Y方向の加速度.
		/// </summary>
		public float AccelerationY
		{
			set; get;
		}

		/// <summary>
		/// 消えるまでの時間.
		/// </summary>
		public int Time
		{
			set; get;
		}

		/// <summary>
		/// 描画色.
		/// </summary>
		public Color Color
		{
			private set; get;
		}


		public Tip(Color color)
		{
			this.Color = color;
		}

		/// <summary>
		/// チップを更新します.
		/// </summary>
		/// <param name="gameTime"></param>
		public virtual void Update(GameTime gameTime)
		{
			if(Time == 0)
			{
				return;
			}
			PositionX += AccelerationX;
			PositionY += AccelerationY;
			//AccelerationX = Explode.RANDOM.Next(-3, 3);
			//AccelerationY = Explode.RANDOM.Next(-3, 3);
			Time--;
		}

		/// <summary>
		/// チップを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		public virtual void Draw(GameTime gameTime, Renderer renderer)
		{
			if(Time == 0)
			{
				return;
			}
			renderer.DrawRectangle(new Rectangle((int)PositionX, (int)PositionY, 1, 1), Color);
		}
	}
}
