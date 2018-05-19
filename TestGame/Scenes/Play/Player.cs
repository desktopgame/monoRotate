using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Input;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Xna2D.Game.Blocks;
using Xna2D.Utilities;
using TestGame.Scenes;
using TestGame.Scenes.Play.Blocks;
using TestGame.Scenes.Play;

namespace Xna2D.Game
{
	/// <summary>
	/// 操作可能なオブジェクト.
	/// </summary>
	public class Player : ColliderBase, IPlayer
	{
		public bool IsDie
		{
			set
			{
				this._isDie = value;
				this.VX = 0f;
				this.VY = 0f;
			}
			get { return _isDie; }
		}
		private bool _isDie;
		
		private FrameTimer jumpTimer;
		private BlinkTimer blinkTimer;
		private Direction direction;
		private bool isJumpNow;
		private bool isPressJump;
		private float jumpY;
		private int siteIndex;

		private static readonly Handle MOVE_LEFT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Left) && (!key.IsKeyDown(Keys.LeftShift) && !key.IsKeyDown(Keys.RightShift)),
			(index, pad) => pad.DPad.Left == ButtonState.Pressed
		);

		private static readonly Handle MOVE_RIGHT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Right) && (!key.IsKeyDown(Keys.LeftShift) && !key.IsKeyDown(Keys.RightShift)),
			(index, pad) => pad.DPad.Right == ButtonState.Pressed
		);

		private static readonly Handle JUMP = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Space) || key.IsKeyDown(Keys.Up),
			(index, pad) => pad.Buttons.A == ButtonState.Pressed
		);

		private static readonly Handle SITE_LEFT = new Handle(
			(index, mouse, key) => (key.IsKeyDown(Keys.LeftShift) || key.IsKeyDown(Keys.RightShift)) && key.IsKeyDown(Keys.Left),
			(index, pad) => pad.ThumbSticks.Right.X < 0f
		);

		private static readonly Handle SITE_RIGHT = new Handle(
			(index, mouse, key) => (key.IsKeyDown(Keys.LeftShift) || key.IsKeyDown(Keys.RightShift)) && key.IsKeyDown(Keys.Right),
			(index, pad) => pad.ThumbSticks.Right.X > 0f
		);

		private static readonly Handle SITE_SHOT = new Handle(
			(index, mouse, key) => key.IsKeyDown(Keys.Z),
			(index, pad) => pad.Buttons.RightShoulder == ButtonState.Pressed
		);

		private static readonly Vector2[] SITE_POSITIONS = new Vector2[]
		{
			new Vector2(0.5f, 1), //真下
			new Vector2(0, 0.5f), //左
			new Vector2(0.5f, 0), //真上
			new Vector2(1, 0.5f), //右
		//	new Vector2(0.5f, 1), //真下
		};
		private static readonly float[] SITE_ANGLES = new float[]
		{
			180, //真下
			270, //左
			360, //真上
			90, //右
		//	360 //真下
		};
		
		public Player(string path) : base(path)
		{
			this.Width = 64;
			this.Height = 64;
			this.jumpTimer = new FrameTimer(10);
			this.blinkTimer = new BlinkTimer();
			this.siteIndex = 2;
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			Respawn(gameTime, elements);
			ControlWalk();
			ControlJump();
			ControlSite(elements);
			ControlShot(elements);
			base.Update(gameTime, elements);
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			if(!IsDie)
			{
				renderer.Draw(Path, Position, Color.White);
			} else
			{
				renderer.Draw(Path, Position, Color.White, blinkTimer.GetAlpha());
			}
			//予測線の描画
			Vector2 from, to;
			GetLine(out from, out to, elements);
			Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
			int thickness = 3;
			for(int i = 0; i < thickness; i++)
			{
				renderer.DrawLine(
					from + new Vector2(0, i),
					to + new Vector2(0, i),
					Color.Red * 0.5f
				);
			}
		}

		/// <summary>
		/// プレイヤーと発射先をつなぐ線分を取得します.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="elements"></param>
		/// <param name="to"></param>
		private void GetLine(out Vector2 from, out Vector2 to, IGameObjectReadOnlyCollection elements)
		{
			Vector2 len = GameConstants.SCREEN_SIZE;
			from = Position + (Size / 2);
			to = ExtensionTo(from, Position + (Size * SITE_POSITIONS[siteIndex]), elements);
		}

		/// <summary>
		/// 終端を画面端まで延長して返します.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="elements"></param>
		/// <returns></returns>
		private Vector2 ExtensionTo(Vector2 from, Vector2 to, IGameObjectReadOnlyCollection elements)
		{
			Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
			Vector2 scrollArea = camera.ScrollArea;
			float area = Math.Max(scrollArea.X, scrollArea.Y);
			//左を差しているなら左へ延長
			if(to.X < from.X)
			{
				to.X = 0;
			//右を差しているなら右へ延長
			} else if(to.X > from.X)
			{
				to.X = area;
			}
			//上を差しているなら上へ延長
			if(to.Y < from.Y)
			{
				to.Y = 0;
			//下を差しているなら下へ延長
			} else if(to.Y > from.Y)
			{
				to.Y = area;
			}
			return to;
		}

		private void Respawn(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			if(!IsDie)
			{
				return;
			}
			//点滅
			blinkTimer.Update();
			if(blinkTimer.Count == 5)
			{
				Vector2 respawnPosition = elements.FindObject<CheckPointBlock>(elem => elem is CheckPointBlock && ((CheckPointBlock)elem).CheckPlayer).Position;
				this.IsDie = false;
				this.X = respawnPosition.X;
				this.Y = respawnPosition.Y - Height;
				blinkTimer.Clear();
			}
			return;
		}

		private void ControlWalk()
		{
			if(IsDie)
			{
				return;
			}
			Detector detector = Detector.GetInstance();
			//横方向の移動
			float walkSpeed = 10f;
			if(MOVE_LEFT.IsDetect())
			{
				this.VX = -walkSpeed;
				this.direction = Direction.Left;
			}
			else if(MOVE_RIGHT.IsDetect())
			{
				this.VX = walkSpeed;
				this.direction = Direction.Right;
			}
			else
			{
				this.VX = 0;
			}
		}

		private void ControlJump()
		{
			if(IsDie)
			{
				return;
			}
			//縦方向の移動
			bool isDetectJump = JUMP.IsDetect();
			if(!isJumpNow && isDetectJump)
			{
				this.VY = -8f;
				this.jumpY = Y - 8f;
				this.isJumpNow = true;
				this.isPressJump = true;
				jumpTimer.Clear();
			}
			//既にジャンプしている場合には
			if(!isJumpNow || !isPressJump)
			{
				return;
			}
			//キーが離されたので上方向への加速を終了
			if(!isDetectJump)
			{
				this.isPressJump = false;
			}
			//まだキーが押されているので加速を継続
			this.VY -= 1f;
			//高さで制限すると天井にくっつけるので時間で制限
			if(jumpTimer.Update().Elapsed())
			{
				this.isPressJump = false;
			}
		}

		private void ControlSite(IGameObjectReadOnlyCollection elements)
		{
			Detector detector = Detector.GetInstance();
			bool detectLeft = detector.IsDetect(SITE_LEFT);
			bool detectRight = detector.IsDetect(SITE_RIGHT);
			//左も右も入力されていないなら終了
			if(!detectLeft && !detectRight)
			{
				return;
			}
			//左へ入力されたならインデックスを左へ
			if(detectLeft)
			{
				this.siteIndex += direction == Direction.Right ? -1 : -1;
			//右へ入力されたならインデックスを右へ
			}
			else
			{
				this.siteIndex += direction == Direction.Right ? 1 : 1;
			}
			//値をはみ出ないように
			if(siteIndex < 0)
			{
				this.siteIndex = SITE_POSITIONS.Length - 1;
			} else if(siteIndex >= SITE_POSITIONS.Length)
			{
				this.siteIndex = 0;
			}
			//左か右へ回転しているなら上下はムリ
			Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
			if((camera.State == Camera.Angle.Left ||
			   camera.State == Camera.Angle.Right) &&
			   (siteIndex == 0 || siteIndex == 2))
			{
				if(detectLeft) this.siteIndex = 1;
				else if(detectRight) this.siteIndex = 3;
			}
		}

		private void ControlShot(IGameObjectReadOnlyCollection elements)
		{
			Detector detector = Detector.GetInstance();
			if(!detector.IsDetect(SITE_SHOT))
			{
				return;
			}
			//まだ前回発射した弾が着弾していない
			GravityBullet gvObj = elements.FindObject<GravityBullet>(elem => elem is GravityBullet);
			if(gvObj != null)
			{
				return;
			}
			Vector2 from, to;
			GetLine(out from, out to, elements);
			Camera camera = elements.FindObject<Camera>(elem => elem is Camera);
			GravityBullet bullet = new GravityBullet();
			float length = SITE_ANGLES[siteIndex];
			if(camera.State == Camera.Angle.Left && siteIndex == 2)
			{
				length = 180f;
			} else if(camera.State == Camera.Angle.Right && siteIndex == 2) {
				length = 180f;
			}
			bullet.X = from.X;
			bullet.Y = from.Y;
			bullet.Target = to;
			bullet.Length = length;
			elements.AddLater(new IGameObject[] { bullet });
		}
		

		protected override void CollisionVertical(IGameObject o, Direction dir)
		{
			base.CollisionVertical(o, dir);
			if(dir.HasFlag(Direction.Bottom))
			{
				this.isJumpNow = false;
				this.isPressJump = false;
				this.jumpY = -1f;
			}
		}

		protected override IGameObject NewInstance()
		{
			return new Player(Path);
		}
	}
}
