using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xna2D.Game;
using Xna2D.Utilities;

namespace TestGame.Scenes.Play
{
	/// <summary>
	/// 回転弾.
	/// </summary>
	public class GravityBullet : GameObjectBase
	{
		/// <summary>
		/// 到達地点.
		/// </summary>
		public Vector2 Target
		{
			set; get;
		}

		/// <summary>
		/// 回転量.
		/// </summary>
		public float Length
		{
			set; get;
		}

		private Vector2 origin;
		private bool animationNow;
		private float offset;
		private FrameTimer timer;

		public GravityBullet() : base("Textures/GShot")
		{
			this.Width = 32;
			this.Height = 32;
			this.timer = new FrameTimer(10);
			this.animationNow = false;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			Move(elements);
			DoRotate(elements, gameTime);
		}

		private void Move(IGameObjectReadOnlyCollection elements)
		{
			if(animationNow)
			{
				return;
			}
			Vector2 period = Vector2.Lerp(Position, Target, 0.1f);
			this.X = period.X;
			this.Y = period.Y;
			bool exit = false;
			elements.ForEach((obj) =>
			{
				if(exit || obj is IPlayer || obj.Equals(this) || obj is Background)
				{
					return;
				}
				if(!obj.Bounds.Intersects(Bounds))
				{
					return;
				}
				//this.pc = Position;
				Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
				IPlayer player = elements.FindObject<IPlayer>(elem => elem is IPlayer);
				//this.pc = (camera.ScrollArea / 2);
				this.origin = GameConstants.SCREEN_SIZE / 2;
				this.animationNow = true;
				timer.Clear();
				BeginRotateImpl(elements);
				exit = true;
			});
		}

		private void DoRotate(IGameObjectReadOnlyCollection elements, GameTime gameTime)
		{
			//アニメーション中でないなら無視
			if(!animationNow)
			{
				return;
			}
			//少しずつ回転を進める
			float R = MathHelper.ToRadians(offset);
			DoRotateImpl(elements, R);
			//回転量を計測
			this.offset += 2;
			CheckEnd(elements);
		}

		private void BeginRotateImpl(IGameObjectReadOnlyCollection elements) {
			elements.ForEach((elem) =>
			{
				IRotetable rObj = elem as IRotetable;
				if(rObj != null && rObj.CanRotate) {
					rObj.BeginRotate(elements, Length);
				}
			});
		}

		private void EndRotateImpl(IGameObjectReadOnlyCollection elements) {
			elements.ForEach((elem) => {
				IRotetable rObj = elem as IRotetable;
				if(rObj != null && rObj.CanRotate) {
					rObj.EndRotate(elements);
				}
			});
		}

		private void DoRotateImpl(IGameObjectReadOnlyCollection elements, float R) {
			float cos = (float)Math.Cos(R);
			float sin = (float)Math.Sin(R);
			elements.ForEach((obj) => {
				IRotetable rObj = obj as IRotetable;
				if(rObj == null || !rObj.CanRotate) {
					return;
				}
				Vector2 p1 = rObj.RotateOrigin;
				Vector2 pd = Vector2.Zero;
				pd.X = (p1.X - origin.X) * cos - (p1.Y - origin.Y) * sin + origin.X;
				pd.Y = (p1.X - origin.X) * sin - (p1.Y - origin.Y) * cos + origin.Y;

				//pd = Vector2.Transform(p1 - pc, Matrix.CreateRotationZ(R));
				rObj.Progress(elements, offset, pd);
			});
		}

		private void CheckEnd(IGameObjectReadOnlyCollection elements)
		{
			if(offset < Length)
			{
				return;
			}
			DoRotateImpl(elements, MathHelper.ToRadians(Length));
			this.animationNow = false;
			this.IsDespawn = true;
			EndRotateImpl(elements);
		}
		
		protected override IGameObject NewInstance()
		{
			return new GravityBullet();
		}
	}
}
