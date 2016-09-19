namespace RpgEngine {
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    

    public static class Texture2DExtensions {
        public static List<Rectangle> GenerateUVs(this Texture2D texture, int tileSize) {
            var uvs = new List<Rectangle>();
            var cols = texture.Width / tileSize;
            var rows = texture.Height / tileSize;

            var u0 = 0;
            var v0 = 0;
            for (int j = 0; j < cols; j++) {
                for (int i = 0; i < rows; i++) {
                    uvs.Add(new Rectangle(u0, v0, tileSize,tileSize));
                    u0 += tileSize;
                }
                u0 = 0;
                v0 += tileSize;
            }

            return uvs;
        }
    }
}