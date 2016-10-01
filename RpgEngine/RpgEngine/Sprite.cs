namespace RpgEngine {
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Sprite {
        public static Sprite Create() {
            return new Sprite();
        }

        public Texture2D Texture { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Point Position { get { return new Point(X, Y);} }
        public Rectangle? UVs { get; set; }


        public void SetPosition(int x, int y) {
            X = x;
            Y = y;
        }

        public void SetUVs(int left, int top, int width, int height) {
            UVs = new Rectangle(left, top, width, height);
        }

        public void ClearUVs() {
            UVs = null;
        }
    }
}