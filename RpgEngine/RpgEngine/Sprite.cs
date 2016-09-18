namespace RpgEngine {
    using Microsoft.Xna.Framework.Graphics;

    public class Sprite {
        public static Sprite Create() {
            return new Sprite();
        }

        public Texture2D Texture { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        public void SetPosition(int x, int y) {
            X = x;
            Y = y;
        }
    }
}