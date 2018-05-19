using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Xna2D.Contents;
using Xna2D.Game;
using Xna2D.Utilities;

namespace TestGame.Scenes.Play
{
	/// <summary>
	/// 残り時間を制御するオブジェクト.
	/// </summary>
	public class TimeObject : GameObjectBase
	{
		/// <summary>
		/// 最大残り時間.
		/// </summary>
		public int MaximumTime
		{
			set
			{
				this._maximumTime = value;
				this.CurrentTime = value;
			}
			get { return _maximumTime; }
		}
		private int _maximumTime;

		/// <summary>
		/// 現在の残り時間.
		/// </summary>
		public int CurrentTime
		{
			private set; get;
		}

		private FrameTimer timer;

		protected static readonly string MAXIMUM_TIME = "MaximumTime";
		protected static readonly string CURRENT_TIME = "CurrentTime";

		public TimeObject(string path) : base(path)
		{
			this.timer = new FrameTimer(30);
		}

		public override void Update(GameTime gameTime, IGameObjectReadOnlyCollection elements)
		{
			if(CurrentTime > 0 && timer.Update().Elapsed())
			{
				this.CurrentTime--;
			}
			if(CurrentTime == 0)
			{

			}
		}

		public override void Draw(GameTime gameTime, Renderer renderer, IGameObjectReadOnlyCollection elements)
		{
			renderer.FillRectangle(new Rectangle(0, 0, 100, 100), Color.Black);
			renderer.DrawNumber("Textures/NumberWhite30", Vector2.Zero, Color.White, CurrentTime, Resource.NUMBER_RECTANGLES);
		}

		public override void Initialize(int id)
		{
			base.Initialize(id);
			this.MaximumTime = 100;
			this.CurrentTime = MaximumTime;
		}

		public override void Write(Dictionary<string, string> d)
		{
			base.Write(d);
			d[MAXIMUM_TIME] = MaximumTime.ToString();
			d[CURRENT_TIME] = CurrentTime.ToString();
		}

		public override void Read(Dictionary<string, string> d)
		{
			base.Read(d);
			this.MaximumTime = d.ParseInteger(MAXIMUM_TIME);
			this.CurrentTime = d.ParseInteger(CURRENT_TIME);
		}

		protected override IGameObject NewInstance()
		{
			return new TimeObject(Path);
		}
	}
}
