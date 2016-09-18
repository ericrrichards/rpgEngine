namespace RpgEngine {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IRenderCommand {
        void Render(SpriteBatch spriteBatch);
    }

    public class TextCommand : IRenderCommand {
        private readonly Vector2 _position;
        private readonly string _text;
        private readonly SpriteFont _font;
        private readonly Color _color;
        private Renderer _renderer;

        public TextCommand(Renderer renderer, Vector2 position, string text, SpriteFont font, Color color) {
            _renderer = renderer;
            _position = position;
            _text = text;
            _font = font;
            _color = color;
        }

        public void Render(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(_font, _text, _position, _color);
        }
    }

    public class SpriteCommand : IRenderCommand {
        private readonly Vector2 _position;
        private readonly Texture2D _texture;



        public SpriteCommand(Texture2D texture, Vector2 position) {
            _texture = texture;
            _position = position;
        }

        public void Render(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}