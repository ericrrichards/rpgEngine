using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace RpgEngine {
    using System;
    using System.Linq;
    using System.Reflection;

    using IronPython.Hosting;
    using IronPython.Runtime.Types;

    using Microsoft.Scripting.Hosting;

    public class GlobalSettings {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Manifest { get; set; }
        public string MainScript { get; set; }
        public string OnUpdate { get; set; }
        public bool Webserver { get; set; }
        public bool Debug { get; set; }
    }

    public class Manifest {
        public List<Asset> Scripts { get; set; }
        public List<Asset> Textures { get; set; }

        public bool AssetExists(string name) {
            return Scripts.Any(a => a.Name == name) || Textures.Any(a => a.Name == name);
        }
    }

    public class Asset {
        public string Name { get; set; }
        public string Path { get; set; }
    }

    public class Globals {
        internal GameTime DeltaTime { get; set; }
        public Renderer Renderer { get; set; }
        public KeyboardState Keyboard { get; set; }

        public double GetDeltaTime() {
            if (DeltaTime == null) {
                return 0;
            }
            return DeltaTime.ElapsedGameTime.TotalSeconds;
        }

        public bool IsKeyDown(Keys key) {
            return Keyboard != null && Keyboard.IsKeyDown(key);
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
        private TextureStore _textures;

        private bool _loadedOnce;
        private DateTime? _lastReload;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);

            LoadGlobalSettings();
            _loadedOnce = true;
            Content.RootDirectory = "Content";

            _engine = Python.CreateEngine();
            _engine.Runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            _engine.Runtime.LoadAssembly(typeof(Keys).Assembly);
        }

        private void LoadGlobalSettings() {
            _settings = JsonConvert.DeserializeObject<GlobalSettings>(File.ReadAllText("settings.json"));
            _manifest = JsonConvert.DeserializeObject<Manifest>(File.ReadAllText(_settings.Manifest));
            if (!_manifest.AssetExists(_settings.MainScript)) {
                Console.WriteLine("Error: Main script {0}, defined in settings.json, does not exist in the asset store.", _settings.MainScript);
                Console.WriteLine("- Has it been defined in the manifest file {0}?", _settings.Manifest);
                Environment.Exit(-1);
            }
            
            graphics.PreferredBackBufferHeight = _settings.Height;
            graphics.PreferredBackBufferWidth = _settings.Width;
            if (_loadedOnce) {
                graphics.ApplyChanges();
            }
            Window.Title = _settings.Name;
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
            _textures = new TextureStore(_manifest, GraphicsDevice);
            try {
                _scope = _engine.CreateScope();
                
                _engine.Execute("from Microsoft.Xna.Framework.Input import Keys", _scope);
                _engine.Execute("import RpgEngine", _scope);

                _scope.SetVariable("Renderer", _globals.Renderer);
                _scope.SetVariable("GetDeltaTime", new Func<double>(_globals.GetDeltaTime));
                _scope.SetVariable("Sprite", DynamicHelpers.GetPythonTypeFromType(typeof(Sprite)));
                _scope.SetVariable("Texture", _textures);
                _scope.SetVariable("LoadMap", new Func<string, TileMap>(s => {
                    if (_manifest.AssetExists(s)) {
                        return JsonConvert.DeserializeObject<TileMap>(File.ReadAllText(_manifest.Scripts.First(a => a.Name == s).Path));
                    }
                    throw new FileNotFoundException(s);
                }));
                _scope.SetVariable("IsKeyDown", new Func<Keys, bool>(keys => _globals.IsKeyDown(keys)));
                _scope.SetVariable("LoadScript", new Action<string>(scriptFile => {
                    if (_manifest.AssetExists(scriptFile)) {
                        _engine.ExecuteFile(_manifest.Scripts.First(a => a.Name == scriptFile).Path, _scope);
                        Console.WriteLine("Loaded script {0}", scriptFile);
                    } else {
                        throw new FileNotFoundException(scriptFile);
                    }
                }));

                _engine.ExecuteFile(_settings.MainScript, _scope);
                _onUpdate = _scope.GetVariable(_settings.OnUpdate);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                Console.WriteLine(_engine.GetService<ExceptionOperations>().FormatException(ex));
            }
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

            if (_settings.Debug && Keyboard.GetState().IsKeyDown(Keys.F2)) {
                if (_lastReload != null && (DateTime.Now - _lastReload.Value).TotalSeconds < 1) {
                    return;
                }
                // reload resources
                LoadGlobalSettings();
                LoadContent();
                _lastReload = DateTime.Now;
            }


            _globals.DeltaTime = gameTime;
            _globals.Keyboard = Keyboard.GetState();
            
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
