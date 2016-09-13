namespace RpgEngine {
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class Renderer {
        private readonly SpriteBatch _spriteBatch;
        private readonly Queue<IRenderCommand> _commands;
        private readonly SpriteFont _defaultFont;
        private readonly ContentManager _content;
        private readonly GraphicsDevice _graphicsDevice;

        public Renderer(GraphicsDevice graphicsDevice, ContentManager content) {
            _commands = new Queue<IRenderCommand>();
            _content = content;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _defaultFont = _content.Load<SpriteFont>("default");

        }

        public void DrawText2D(int x, int y, string text) {
            var width2 = _graphicsDevice.Viewport.Width / 2;
            var height2 = _graphicsDevice.Viewport.Height / 2;


            _commands.Enqueue(new TextCommand(x + width2, -y + height2, text, _defaultFont, Color.White));
        }

        internal void Render() {
            _graphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            while (_commands.Count != 0) {
                var command = _commands.Dequeue();
                command.Render(_spriteBatch);
            }

            _spriteBatch.End();
        }
    }


}