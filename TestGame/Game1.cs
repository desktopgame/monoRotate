using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Xna2D.Contents.Loaders;
using System.Diagnostics;
using Xna2D.Contents;
using Xna2D.Scenes;
using TestGame.Scenes;
using Xna2D.Game;
using TestGame.Scenes.Play;
using Xna2D.Game.Blocks;
using TestGame.Scenes.Title;
using TestGame.Scenes.Select;
using TestGame.Scenes.GameOver;
using TestGame.Scenes.Pause;
using TestGame.Scenes.Clear;

namespace TestGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		private GraphicsDeviceManager graphics;
		private SceneManager sceneManager;
		private Renderer renderer;
		private Sound sound;
		/// <summary>
		/// ÉQÅ[ÉÄÇèIóπÇ∑ÇÈÇ»ÇÁtrue.
		/// </summary>
		public static bool IsExit
		{
			set; get;
		}

		public Game1()
		{
			this.graphics = new GraphicsDeviceManager(this);
			this.Content.RootDirectory = "Content";
			this.IsMouseVisible = true;
			Program.SetupObjects();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			graphics.PreferredBackBufferWidth = GameConstants.SCREEN_WIDTH;
			graphics.PreferredBackBufferHeight = GameConstants.SCREEN_HEIGHT;
			graphics.ApplyChanges();
			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			//spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			this.renderer = new Renderer(Content, GraphicsDevice);
			this.sound = new Sound(Content);
			this.sceneManager = new SceneManager();
			Func<IScene, IScene> wrap = (scene) => new FadeInScene(new FadeOutScene(scene, GameConstants.SCREEN_SIZE), GameConstants.SCREEN_SIZE);
			StageSelector stageSelector = new StageSelector();
			IScene playScene = new PlayScene(stageSelector, sound);
			sceneManager[(int)SceneTypes.Load] = new LoadScene(
					new ContentIterator(Content)
						.Add(new TextureLoader(renderer, "Textures"))
						.Add(new SoundEffectLoader(sound, "Sound/Effect"))
						.Add(new BGMLoader(sound, "Sound/Song"))
			);
			sceneManager[(int)SceneTypes.Title] = new FadeOutScene(new TitleScene(sound), GameConstants.SCREEN_SIZE);
			sceneManager[(int)SceneTypes.Select] = wrap(new SelectScene(stageSelector, sound));
			sceneManager[(int)SceneTypes.Play] = wrap(playScene);
			sceneManager[(int)SceneTypes.Clear] = new ClearScene(playScene, stageSelector, sound);
			sceneManager[(int)SceneTypes.GameOver] = new GameOverScene(playScene, sound);
			sceneManager[(int)SceneTypes.Exit] = new ExitScene();
			sceneManager[(int)SceneTypes.Credit] = wrap(new CreditScene());
			sceneManager[(int)SceneTypes.Pause] = new PauseScene(playScene, stageSelector, sound);
			sceneManager.Current = (int)SceneTypes.Load;
			renderer.Load("Textures/Back/Black");
			renderer.Load("Textures/NumberBlack30");
			renderer.Load("Textures/NumberWhite30");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
			renderer.Unload();
			sound.Unload();
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			//ESCÇ≈èIóπ
			if(Keyboard.GetState().IsKeyDown(Keys.Escape) || IsExit)
			{
				this.Exit();
			}
			// TODO: Add your update logic here
			sceneManager.Update(gameTime);
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			sceneManager.Draw(gameTime, renderer);
			base.Draw(gameTime);
		}
	}
}
