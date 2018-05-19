using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;

namespace Xna2D.Scenes
{
	/// <summary>
	/// 委譲を行う実装です.
	/// </summary>
	public abstract class LayeredScene : IScene
	{
		/// <summary>
		/// このクラス自身が処理を行うべきタイミングを指定します.
		/// このレイヤが委譲先より前にあるならこのレイヤが終了してから委譲し、
		/// このレイヤが委譲先より後にあるなら委譲先が終了してからレイヤを実行します。
		/// </summary>
		public enum MaskType
		{
			Back, Front
		}

		protected internal readonly IScene delegatez;
		private MaskType maskType;

		public bool IsEnd
		{
			get { return (maskType == MaskType.Back && delegatez.IsEnd)|| (maskType == MaskType.Front && layerEnd); }
		}
		protected bool layerEnd;

		public int Next
		{
			get { return delegatez.Next; }
		}

		protected LayeredScene(MaskType maskType, IScene delegatez)
		{
			this.maskType = maskType;
			this.delegatez = delegatez;
		}

		public void Update(GameTime gameTime)
		{
			CheckBackUpdate(gameTime);
			CheckFrontUpdate(gameTime);
		}

		private void CheckBackUpdate(GameTime gameTime)
		{
			if(maskType != MaskType.Back)
			{
				return;
			}
			if(!layerEnd)
			{
				//レイヤーを拒否
				ILayered layered = delegatez as ILayered;
				if(layered != null && !layered.IsNeedBackLayer(this))
				{
					this.layerEnd = true;
					delegatez.Update(gameTime);
				} else
				{
					UpdateLayer(gameTime);
				}
			} else
			{
				delegatez.Update(gameTime);
			}
		}

		private void CheckFrontUpdate(GameTime gameTime)
		{
			if(maskType != MaskType.Front)
			{
				return;
			}
			if(!delegatez.IsEnd)
			{
				delegatez.Update(gameTime);
			} else
			{
				//レイヤーを拒否
				ILayered layered = delegatez as ILayered;
				if(layered != null && !layered.IsNeedFrontLayer(this))
				{
					this.layerEnd = true;
				} else
				{
					UpdateLayer(gameTime);
				}
			}
		}

		/// <summary>
		/// 更新処理を委譲によって処理します.
		/// </summary>
		/// <param name="gameTime"></param>
		protected void UpdateDelegate(GameTime gameTime)
		{
			delegatez.Update(gameTime);
		}

		/// <summary>
		/// MaskがBackならこの処理によってlayerEndがtrueになってから委譲を開始します　委譲先が終了するとこのシーンが終了します.
		/// MaskがFrontなら委譲が終了してからこの処理によってlayerEndがtrueになり、シーンが終了します。
		/// </summary>
		/// <param name="game"></param>
		protected virtual void UpdateLayer(GameTime game)
		{
		}
		
		public void Draw(GameTime gameTime, Renderer renderer)
		{
			CheckBackDraw(gameTime, renderer);
			CheckFrontDraw(gameTime, renderer);
		}
		
		private void CheckBackDraw(GameTime gameTime, Renderer renderer)
		{
			if(maskType != MaskType.Back)
			{
				return;
			}
			if(!layerEnd)
			{
				//レイヤーを拒否
				ILayered layered = delegatez as ILayered;
				if(layered != null && !layered.IsNeedBackLayer(this))
				{
					this.layerEnd = true;
					delegatez.Draw(gameTime, renderer);
				} else
				{
					DrawLayer(gameTime, renderer);
				}
			}
			else
			{
				delegatez.Draw(gameTime, renderer);
			}
		}

		private void CheckFrontDraw(GameTime gameTime, Renderer renderer)
		{
			if(maskType != MaskType.Front)
			{
				return;
			}
			if(!delegatez.IsEnd)
			{
				delegatez.Draw(gameTime, renderer);
			}
			else
			{
				//レイヤーを拒否
				ILayered layered = delegatez as ILayered;
				if(layered != null && !layered.IsNeedFrontLayer((this)))
				{
					this.layerEnd = true;
					delegatez.Draw(gameTime, renderer);
				} else
				{
					DrawLayer(gameTime, renderer);
				}
			}
		}
		
		/// <summary>
		/// 描画処理を委譲によって処理します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		protected void DrawDelegate(GameTime gameTime, Renderer renderer)
		{
			delegatez.Draw(gameTime, renderer);
		}

		/// <summary>
		/// このレイヤーを描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		protected virtual void DrawLayer(GameTime gameTime, Renderer renderer)
		{
		}

		public virtual void Show()
		{
			delegatez.Show();
			this.layerEnd = false;
		}

		public virtual void Hide()
		{
			delegatez.Hide();
		}
		
	}
}
