using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna2D.Contents;

namespace Xna2D.Game
{
	/// <summary>
	/// IGameObjectの基底クラスです.
	/// </summary>
	public abstract class GameObjectBase : IGameObject, IRotetable, ICameraObsever
	{
		public string Path { protected set; get; }

		public float X { set; get; }

		public float Y { set; get; }

		public float Width { set; get; }

		public float Height { set; get; }

		public float Left
		{
			get { return X; }
		}

		public float Right
		{
			get { return X + Width; }
		}

		public float Top
		{
			get { return Y; }
		}

		public float Bottom
		{
			get { return Y + Height; }
		}

		public float CenterX
		{
			get { return X + (Width / 2); }
		}

		public float CenterY
		{
			get { return Y + (Height / 2); }
		}
		
		public bool IsRotateNow
		{
			protected set; get;
		}

		public Vector2 Position
		{
			get { return new Vector2(X, Y); }
		}
		
		public Vector2 Center
		{
			get { return new Vector2(CenterX, CenterY); }
		}

		public Vector2 Size
		{
			get { return new Vector2(Width, Height); }
		}

		public Rectangle Bounds { get { return new Microsoft.Xna.Framework.Rectangle((int)X, (int)Y, (int)Width, (int)Height); } }

		public float LayerDepth { set; get; }

		public bool IsDespawn { protected set; get; }

		public int Id { private set; get; } = -1;

		public bool CanRotate
		{
			protected set; get;
		} = true;

		public Vector2 RotateOrigin
		{
			protected set; get;
		}
		
		protected Camera.Angle angle;

		protected static readonly string KEY_X = "X";
		protected static readonly string KEY_Y = "Y";
		protected static readonly string KEY_WIDTH = "Width";
		protected static readonly string KEY_HEIGHT = "Height";
		protected static readonly string KEY_LAYER_DEPTH = "LayerDepth";

		protected GameObjectBase(string path)
		{
			this.Path = path;
		}

		public override string ToString()
		{
			return "Name=" + GetType().Name + ", X=" + X + ", Y=" + Y + ", Width=" + Width + ", Height=" + Height;
		}

		public abstract void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements);

		public virtual void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
		//	renderer.Draw(Path, Position + Scroll(elements), MathHelper.ToRadians(Rotate), Size / 2);
			renderer.Draw(Path, Position, Color.White);
		}
		
		/// <summary>
		/// カメラアングルに合わせてオブジェクトを回転描画します.
		/// </summary>
		/// <param name="gameTime"></param>
		/// <param name="renderer"></param>
		/// <param name="elements"></param>
		protected void DrawRotate(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			float rotate = GetRotate();
			renderer.Draw(Path, Position, MathHelper.ToRadians(rotate), Size / 2);
		}
		
		#region IGameData
		//FIXME:テンプレートメソッド
		/// <summary>
		/// サブクラスはオーバーライドするなら必ずこのメソッドを呼び出して下さい.
		/// </summary>
		/// <param name="id"></param>
		public virtual void Initialize(int id)
		{
			this.Id = id;
		}

		public virtual void Read(Dictionary<string, string> d)
		{
			this.X = d.ParseFloat(KEY_X);
			this.Y = d.ParseFloat(KEY_Y);
			this.Width = d.ParseFloat(KEY_WIDTH);
			this.Height = d.ParseFloat(KEY_HEIGHT);
			this.LayerDepth = d.ParseFloat(KEY_LAYER_DEPTH);
		}

		public virtual void Write(Dictionary<string, string> d)
		{
			d[KEY_X] = X.ToString();
			d[KEY_Y] = Y.ToString();
			d[KEY_WIDTH] = Width.ToString();
			d[KEY_HEIGHT] = Height.ToString();
			d[KEY_LAYER_DEPTH] = LayerDepth.ToString();
		}

		/// <summary>
		/// サブクラス自身を生成して返します.
		/// </summary>
		/// <returns></returns>
		protected abstract IGameObject NewInstance();

		public virtual object Clone()
		{
			IGameObject self = NewInstance();
			Dictionary<string, string> content = new Dictionary<string, string>();
			self.Initialize(Id);
			Write(content);
			self.Read(content);
			return self;
		}

		public virtual Type GetContentType(string key)
		{
			if(key == KEY_X ||
			   key == KEY_Y ||
			   key == KEY_WIDTH ||
			   key == KEY_HEIGHT ||
			   key == KEY_LAYER_DEPTH)
			{
				return typeof(float);
			}
			return null;
		}

		public virtual bool IsReadOnly(string key)
		{
			return key != "Tag";
		}
		#endregion

		#region IRotetable
		public virtual void BeginRotate(IGameObjectReadOnlyCollection elements, float len)
		{
			this.RotateOrigin = Position;
			this.IsRotateNow = true;
		}

		public virtual void Progress(IGameObjectReadOnlyCollection elements, float rad, Vector2 pos)
		{
			this.X = pos.X;
			this.Y = pos.Y;
		}

		public virtual void EndRotate(IGameObjectReadOnlyCollection elements)
		{
			this.IsRotateNow = false;
		}
		#endregion

		#region ICameraObserver
		public virtual void AngleChanged(Camera.Angle newAngle)
		{
			this.angle = newAngle;
		}

		/// <summary>
		/// カメラアングルを方角に変換します.
		/// </summary>
		/// <param name="newAngle"></param>
		/// <returns></returns>
		protected Direction GetDirection(Camera.Angle newAngle)
		{
			switch(newAngle)
			{
				case Camera.Angle.Normal: return Direction.Top;
				case Camera.Angle.Vertical: return Direction.Bottom;
				case Camera.Angle.Left: return Direction.Left;
				case Camera.Angle.Right: return Direction.Right;
			}
			return Direction.None;
		}

		/// <summary>
		/// カメラアングルに対応して画像を回転しなければいけない場合に使用出来ます.
		/// 描画に使用するときはMethHelper.ToRadiunsで変換する必要があります。
		/// </summary>
		/// <returns></returns>
		protected float GetRotate()
		{
			switch(angle)
			{
				case Camera.Angle.Normal: return 0;
				case Camera.Angle.Vertical: return 180;
				case Camera.Angle.Left: return 270;
				case Camera.Angle.Right: return 90;
			}
			return 0f;
		}

		/// <summary>
		/// カメラアングルに対応して画像を回転しなければいけない場合に使用出来ます.
		/// </summary>
		/// <param name="elements"></param>
		/// <returns></returns>
		protected Vector2 GetOrigin(IGameObjectReadOnlyCollection elements)
		{
			Vector2 origin = Vector2.Zero;
			Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
			if(camera.State == Camera.Angle.Vertical)
			{
				origin = Size;
			}
			else if(camera.State == Camera.Angle.Right)
			{
				//origin.X = Size.X;
				origin.Y = Size.Y;
			}
			return origin;
		}
		#endregion
	}
}
