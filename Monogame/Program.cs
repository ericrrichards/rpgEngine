using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame {
	public class MainClass : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;


		public MainClass() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize() {
			base.Initialize();
		}

		protected override void LoadContent() {
			spriteBatch = new SpriteBatch(GraphicsDevice);

			base.LoadContent();
		}

		protected override void Update(GameTime gameTime) {

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
				Exit();
			}
			
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);

			base.Draw(gameTime);
		}

		public static void Main(string[] args) {
			Console.WriteLine("Hello World!");

			var game = new MainClass();
			game.Run();
		}
	}
}
