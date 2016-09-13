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

        public TextCommand(int x, int y, string text, SpriteFont font, Color color) {
            _position = new Vector2(x, y);
            _text = text;
            _font = font;
            _color = color;
        }

        public void Render(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(_font, _text, _position, _color);
        }
    }
}