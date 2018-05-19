using Microsoft.Xna.Framework;
using System;
using TestGame.Scenes.Play;
using TestGame.Scenes.Play.Blocks;
using Xna2D;
using Xna2D.Game;
using Xna2D.Game.Blocks;

namespace TestGame
{
#if WINDOWS || XBOX
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }

		/// <summary>
		/// 全てのオブジェクトを登録します.
		/// </summary>
		public static void SetupObjects()
		{
			GameObjectRegistry reg = GameObjectRegistry.GetInstance();
			reg.Reg(() => new Player("Textures/Player"));
			reg.Reg(() => new Camera("Textures/CameraObject"));
			reg.Reg(() => new TimeObject("Textures/TimeObject"));
			//通常ブロック
			reg.Reg(() => new Block("Textures/Block/Red_V6"));
			reg.Reg(() => new Block("Textures/Block/Blue_V6"));
			reg.Reg(() => new Block("Textures/Block/Green_V6"));
			reg.Reg(() => new Block("Textures/Block/Yellow_V6"));
			reg.Reg(() => new Block("Textures/Block/Red_V6_Big", 48, 48));
			reg.Reg(() => new Block("Textures/Block/Blue_V6_Big", 48, 48));
			reg.Reg(() => new Block("Textures/Block/Green_V6_Big", 48, 48));
			reg.Reg(() => new Block("Textures/Block/Yellow_V6_Big", 48, 48));
			//背景
			reg.Reg(() => new Background("Textures/Back/Black", 1280, 720));
			reg.Reg(() => new Background("Textures/Back/Green", 64, 64));
			reg.Reg(() => new FlowBackground("Textures/Back/Red_Rect", 64, 32));
			reg.Reg(() => new FlowBackground("Textures/Back/Blue_Rect", 64, 32));
			reg.Reg(() => new FlowBackground("Textures/Back/Green_Rect", 64, 32));
			reg.Reg(() => new FlowBackground("Textures/Back/Yellow_Rect", 64, 32));
			//ギミック
			reg.Reg(() => new RemoveBlock("Textures/Block/Remove"));
			reg.Reg(() => new MoveBlock("Textures/Block/Move", new Vector2(0, 1), new Vector2(0, 128)));
			reg.Reg(() => new CheckPointBlock("Textures/Block/CheckPoint"));
			reg.Reg(() => new FlagBlock("Textures/Block/Flag"));
			//罠
			reg.Reg(() => new TrapBlock("Textures/Block/Needle_Top", Direction.Top));
			reg.Reg(() => new TrapBlock("Textures/Block/Needle_Bottom", Direction.Bottom));
			reg.Reg(() => new TrapBlock("Textures/Block/Needle_Left", Direction.Left));
			reg.Reg(() => new TrapBlock("Textures/Block/Needle_Right", Direction.Right));
			//背景2
			reg.Reg(() => new Background("Textures/Back/ControlHelp", 512, 128));
			reg.Reg(() => new Background("Textures/Back/FlagHelp", 256, 128));
			reg.Reg(() => new Background("Textures/Back/SiteHelp", 384, 128));
			//スクロール
			reg.Reg(() => new Scroller("Textures/Scroll"));
			//通常ブロック
			reg.Reg(() => new Block("Textures/Block/Red"));
			reg.Reg(() => new Block("Textures/Block/Blue"));
			reg.Reg(() => new Block("Textures/Block/Green"));
			reg.Reg(() => new Block("Textures/Block/Yellow"));
		}
	}
#endif
}

