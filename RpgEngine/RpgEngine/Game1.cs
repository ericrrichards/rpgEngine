using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace RpgEngine {
    public class GlobalSettings {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Manifest { get; set; }
        public string MainScript { get; set; }
        public string OnUpdate { get; set; }
        public bool Webserver { get; set; }
    }

    public class Manifest {
        public List<string> Scripts { get; set; }
        public List<string> Textures { get; set; }
    }

    public static class CSharpScriptEngine {
        private static ScriptState<object> _scriptState;
        public static object Execute(string code) {
            _scriptState = _scriptState == null 
                ? CSharpScript.RunAsync(code).Result 
                : _scriptState.ContinueWithAsync(code).Result;
            if (_scriptState.ReturnValue != null && !string.IsNullOrEmpty(_scriptState.ReturnValue.ToString())) {
                return _scriptState.ReturnValue;
            }
            return null;
        }

        public static void LoadScript(string scriptFile) {
            Execute(File.ReadAllText(scriptFile));
        }

        public static void Reset() {
            _scriptState = null;
        }
    }


    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GlobalSettings _settings;
        private Manifest _manifest;


        public Game1() {

            _settings = JsonConvert.DeserializeObject<GlobalSettings>(File.ReadAllText("settings.json"));
            _manifest = JsonConvert.DeserializeObject<Manifest>(File.ReadAllText(_settings.Manifest));

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = _settings.Height;
            graphics.PreferredBackBufferWidth = _settings.Width;
            Window.Title = _settings.Name;

            Content.RootDirectory = "Content";

            CSharpScriptEngine.LoadScript(_settings.MainScript);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //var panel = new Panel(Content.Load<Texture2D>("simple_panel.png"), 3);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Exit();
            }

            // TODO: Add your update logic here
            CSharpScriptEngine.Execute(_settings.OnUpdate);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }

    internal class Panel {
        private Texture2D texture2D;
        private int tileSize;

        public Panel(Texture2D texture2D, int size) {
            this.texture2D = texture2D;
            tileSize = size;
        }
    }
}
