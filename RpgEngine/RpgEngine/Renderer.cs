namespace RpgEngine {
    using System;
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

        private AlignX AlignX { get; set; }
        private AlignY AlignY { get; set; }


        public int ScreenWidth { get { return _graphicsDevice.Viewport.Width; } }
        public int ScreenHeight { get { return _graphicsDevice.Viewport.Height; } }

        public Renderer(GraphicsDevice graphicsDevice, ContentManager content) {
            _commands = new Queue<IRenderCommand>();
            _content = content;
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _defaultFont = _content.Load<SpriteFont>("default");

        }

        public void DrawSprite(Sprite sprite) {
            var width2 = _graphicsDevice.Viewport.Width / 2;
            var height2 = _graphicsDevice.Viewport.Height / 2;
            var center = sprite.Texture.Bounds.Center;
            if (sprite.UVs != null) {
                center = new Point(sprite.UVs.Value.Width/2, sprite.UVs.Value.Height/2);
            }
            var position = new Vector2(sprite.X + width2 - center.X, -sprite.Y + height2 - center.Y);
            
            _commands.Enqueue(new SpriteCommand(sprite.Texture, position, sprite.UVs));
        }

        public void DrawText2D(int x, int y, string text) {
            var width2 = _graphicsDevice.Viewport.Width / 2;
            var height2 = _graphicsDevice.Viewport.Height / 2;

            var textSize = _defaultFont.MeasureString(text);
            var position = new Vector2(x + width2, -y + height2);
            switch (AlignX) {
                case AlignX.Left:
                    // nothing need be done
                    break;
                case AlignX.Center:
                    position.X -= textSize.X / 2;
                    break;
                case AlignX.Right:
                    position.X -= textSize.X;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            switch (AlignY) {
                case AlignY.Top:
                    // nothing need be done
                    break;
                case AlignY.Center:
                    position.Y -= textSize.Y / 2;
                    break;
                case AlignY.Bottom:
                    position.Y -= textSize.Y;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _commands.Enqueue(new TextCommand(this, position, text, _defaultFont, Color.White));
        }

        public void AlignText(string alignX, string alignY) {
            AlignX tempX;
            if (Enum.TryParse(alignX, true, out tempX)) {
                AlignX = tempX;
            } else {
                Console.WriteLine("Unable to parse alignment: " + alignX);
            }
            AlignY tempY;
            if (Enum.TryParse(alignY, true, out tempY)) {
                AlignY = tempY;
            } else {
                Console.WriteLine("Unable to parse alignment: " + alignY);
            }
        }

        public void AlignTextX(string alignX) {
            AlignX tempX;
            if (Enum.TryParse(alignX, true, out tempX)) {
                AlignX = tempX;
            } else {
                Console.WriteLine("Unable to parse alignment: " + alignX);
            }
        }
        public void AlignTextY(string alignY) {
            AlignY tempY;
            if (Enum.TryParse(alignY, true, out tempY)) {
                AlignY = tempY;
            } else {
                Console.WriteLine("Unable to parse alignment: " + alignY);
            }
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

    

    public enum AlignX {
        Left,
        Center,
        Right
    }

    public enum AlignY {
        Top,
        Center,
        Bottom
    }
}