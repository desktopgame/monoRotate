using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using System.Diagnostics;

namespace Xna2D.Game
{
	/// <summary>
	/// IColliderの基底クラスです.
	/// </summary>
	public abstract class ColliderBase : GameObjectBase, ICollider
	{
		public float VX
		{
			set; get;
		}

		public float VY
		{
			set; get;
		}

		/// <summary>
		/// このオブジェクトが移動するときに無視できる段差の高さ.
		/// </summary>
		public float Knee
		{
			set; get;
		} = 8;

		protected static readonly string KEY_VX = "VX";
		protected static readonly string KEY_VY = "VY";
		protected static readonly string KEY_KNEE = "Knee";

		public ColliderBase(string path) : base(path)
		{
		}
		
		public ColliderBase(string path, float width, float height) : base(path)
		{
			this.Width = width;
			this.Height = height;
		}

		/// <summary>
		/// 物理演算を実行します.
		/// オーバーライドするならサブクラスは必ず最後にこのメソッドを呼び出してください.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="elements"></param>
		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			Integrate(gameTime, elements);
		}

		private void Integrate(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			//重力で落下する
			if(VY < 16)
			{
				this.VY += GameConstants.GRAVITY;
			}
			//全てのオブジェクトとの衝突を検証する
			this.X += VX;
			this.Y += VY;
			IEnumerator<IGameObject> oenums = elements.GetEnumerator();
			Direction dir;
			while(oenums.MoveNext())
			{
				IGameObject o = oenums.Current;
				if(o.Equals(this))
				{
					continue;
				}
				if(!IsCollision(o, out dir))
				{
					continue;
				}
				Collision(o, dir);
			}
		}

		/// <summary>
		/// このオブジェクトとあるオブジェクトの衝突を検証します.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		/// <returns></returns>
		protected bool IsCollision(IGameObject o, out Direction dir)
		{
			//処理を簡略出来るならそうする
			dir = Direction.None;
			ICollidable collidable = o as ICollidable;
			if(collidable != null)
			{
				return collidable.IsCollision(o, out dir);
			}
			//衝突してない
			if(!Bounds.Intersects(o.Bounds))
			{
				return false;
			}
			//縦方向のあたりを調べる
			//例えば横に移動するとき、
			//頭にオブジェクトがひっかかる場合と足にオブジェクトがひっかかる場合がある
			//重力によって常に下方向への加速度を受けているので、
			//ある程度このひっかかり度を許容しないと、移動したときにオブジェクトに衝突していると判定され、
			//例えば右に移動しているときにオブジェクトの左側に衝突していると判定され、オブジェクトの左側にスナップされる
			float tbDist = Math.Abs(Top - o.Bottom);
			float btDist = Math.Abs(Bottom - o.Top);
			float distY = VY > 0 ? btDist : tbDist;
			//横方向のあたり
			//例えば縦に移動するとき、
			//頭を天井にぶつける場合と地面に着地する場合がある
			//空中でも横移動を行うことが出来るので...
			float lrDist = Math.Abs(Left - o.Right);
			float rlDist = Math.Abs(Right - o.Left);
			float distX = VX > 0 ? rlDist : lrDist;
			//ここでは引っ掛かり度2以上のときのみ衝突
			//この数値を大きくすると段差を登れたりする
			bool rangeSafeY = distY > 31f;
			bool rangeSafeX = distX > 31f;
			if(distY > Height)
			{
				rangeSafeX = false;
			}
			if(distX > Width)
			{
				rangeSafeY = false;
			}
			//横方向のあたり判定
			if(VX < 0 && rangeSafeY)
			{
				dir |= Direction.Left;
				this.X = o.Right;
				this.VX = 0;
			} else if(VX > 0 && rangeSafeY)
			{
				dir |= Direction.Right;
				this.X = o.Left - Width;
				this.VX = 0;
			}
			//縦方向のあたり判定
			if(VY < 0 && rangeSafeX)
			{
				dir |= Direction.Top;
				this.Y = o.Bottom;
				this.VY = 0;
			} else if(VY > 0 && rangeSafeX)
			{
				dir |= Direction.Bottom;
				this.Y = o.Top - Height;
				this.VY = 0;
			}
			return true;
		}

		/// <summary>
		/// 衝突しているときに呼ばれます.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		private void Collision(IGameObject o, Direction dir)
		{
			//縦方向へ衝突
			if(dir.HasFlag(Direction.Top) || dir.HasFlag(Direction.Bottom))
			{
				CollisionVertical(o, dir);
			} else
			{
				NotCollisionVertical(o, dir);
			}
			//横方向へ衝突
			if(dir.HasFlag(Direction.Left) || dir.HasFlag(Direction.Right))
			{
				CollisionHorizontal(o, dir);
			}
			else
			{
				NotCollisionHorizontal(o, dir);
			}
		}

		/// <summary>
		/// 水平方向に衝突しているときに呼ばれます.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		protected virtual void CollisionHorizontal(IGameObject o, Direction dir)
		{
			if(dir.HasFlag(Direction.Left))
			{
				this.X = o.Right;
			}
			else if(dir.HasFlag(Direction.Right))
			{
				this.X = o.Left - Width;
			}
			(o as ICollisionCallback)?.Collision(this, dir);
			this.VX = 0;
		}

		/// <summary>
		/// 水平方向に衝突していないときに呼ばれます.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		protected virtual void NotCollisionHorizontal(IGameObject o, Direction dir)
		{
		}

		/// <summary>
		/// 垂直方向に衝突しているときに呼ばれます.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		protected virtual void CollisionVertical(IGameObject o, Direction dir)
		{
			if(dir.HasFlag(Direction.Top))
			{
				this.Y = o.Bottom;
			}
			else if(dir.HasFlag(Direction.Bottom))
			{
				this.Y = o.Top - Height - 0;
			}
			(o as ICollisionCallback)?.Collision(this, dir);
			this.VY = 0;
		}

		/// <summary>
		/// 垂直方向に衝突していないときに呼ばれます.
		/// </summary>
		/// <param name="o"></param>
		/// <param name="dir"></param>
		protected virtual void NotCollisionVertical(IGameObject o, Direction dir)
		{
		}

		//
		//IO
		//

		public override void Read(Dictionary<string, string> d)
		{
			base.Read(d);
			this.VX = d.ParseFloat(KEY_VX);
			this.VY = d.ParseFloat(KEY_VY);
			this.Knee = d.ParseFloat(KEY_KNEE);
		}

		public override void Write(Dictionary<string, string> d)
		{
			base.Write(d);
			d[KEY_VX] = VX.ToString();
			d[KEY_VY] = VY.ToString();
			d[KEY_KNEE] = Knee.ToString();
		}
	}
}
