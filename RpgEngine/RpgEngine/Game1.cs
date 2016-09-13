using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace RpgEngine {
    using System;
    using System.Reflection;

    using IronPython.Hosting;

    using Microsoft.Scripting.Hosting;

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

    public class Globals {
        internal GameTime DeltaTime { get; set; }
        public Renderer Renderer { get; set; }

        public double GetDeltaTime() {
            if (DeltaTime == null) {
                return 0;
            }
            return DeltaTime.ElapsedGameTime.TotalSeconds;
        }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        private GlobalSettings _settings;
        private Manifest _manifest;
        private Globals _globals;
        private Renderer _renderer;
        private ScriptEngine _engine;
        private ScriptScope _scope;
        private dynamic _onUpdate;

        public Game1() {

            _settings = JsonConvert.DeserializeObject<GlobalSettings>(File.ReadAllText("settings.json"));
            _manifest = JsonConvert.DeserializeObject<Manifest>(File.ReadAllText(_settings.Manifest));

            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferHeight = _settings.Height;
            graphics.PreferredBackBufferWidth = _settings.Width;
            Window.Title = _settings.Name;

            Content.RootDirectory = "Content";

            _engine = Python.CreateEngine();
            _engine.Runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            
            
            _scope = _engine.CreateScope();
            


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
            _renderer = new Renderer(GraphicsDevice, Content);
            _globals = new Globals();
            _globals.Renderer = _renderer;

            _scope.SetVariable("Renderer", _globals.Renderer);
            _scope.SetVariable("GetDeltaTime", new Func<double>(_globals.GetDeltaTime));
            _engine.ExecuteFile(_settings.MainScript, _scope);
            _onUpdate = _scope.GetVariable(_settings.OnUpdate);

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
            _globals.DeltaTime = gameTime;
            
            try {
                _onUpdate();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            
            _renderer.Render();

            base.Draw(gameTime);
        }
    }
}
