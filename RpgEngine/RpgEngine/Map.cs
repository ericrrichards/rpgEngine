namespace RpgEngine {
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Map {
        public int CamX { get; set; }
        public int CamY { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int WidthPixel { get; set; }
        public int HeightPixel { get; set; }

        public TileMap MapDef { get; set; }
        public TileLayer Layer { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<int> Tiles { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public Texture2D TextureAtlas { get; set; }
        public Sprite TileSprite { get; set; }
        public List<Rectangle> UVs { get; set; }

        public Map(TileMap mapDef) {
            CamX = 0;
            CamY = 0;

            MapDef = mapDef;
            TextureAtlas = TextureStore.Instance.Find(mapDef.TileSets[0].Image);

            TileSprite = new Sprite();
            TileSprite.Texture = TextureAtlas;

            Layer = mapDef.Layers[0];
            Width = mapDef.Layers[0].Width;
            Height = mapDef.Layers[0].Height;

            Tiles = mapDef.Layers[0].Data;
            TileWidth = mapDef.TileSets[0].TileWidth;
            TileHeight = mapDef.TileSets[0].TileHeight;

            X = -GlobalSettings.Instance.Width / 2 + TileWidth / 2;
            Y = GlobalSettings.Instance.Height / 2 - TileHeight / 2;

            WidthPixel = Width * TileWidth;
            HeightPixel = Height * TileHeight;

            UVs = TextureAtlas.GenerateUVs(TileWidth, TileHeight);
        }

        private Point PointToTile(int x, int y) {
            x += TileWidth / 2;
            y -= TileHeight / 2;
            
            x = Math.Min(X + WidthPixel - 1, Math.Max(X, x));
            y = Math.Max(Y - HeightPixel + 1, Math.Min(Y, y));

            var tileX = (int)Math.Floor((double)(x - X) / TileWidth);
            var tileY = (int)Math.Floor((double)(Y - y) / TileHeight);

            return new Point(tileX, tileY);
        }

        private int GetTile(int x, int y) {
            return Tiles[x + y * Width] - 1;
        }

        private void Goto(int x, int y) {
            CamX = x - GlobalSettings.Instance.Width / 2;
            CamY = -y + GlobalSettings.Instance.Height / 2;
        }

        public void GotoTile(int x, int y) {
            Goto(x * TileWidth + TileWidth/2, y * TileHeight + TileHeight/2);
        }

        public void Render(Renderer renderer) {
            var leftBottom = PointToTile(CamX - renderer.ScreenWidth / 2, CamY - renderer.ScreenHeight / 2);
            var rightTop = PointToTile(CamX + renderer.ScreenWidth / 2, CamY + renderer.ScreenHeight / 2);

            for (int j = rightTop.Y; j <= leftBottom.Y; j++) {
                for (int i = leftBottom.X; i <= rightTop.X; i++) {
                    var tile = GetTile(i, j);
                    var uvs = UVs[tile];

                    TileSprite.UVs = uvs;
                    TileSprite.SetPosition(X + i * TileWidth, Y - j * TileHeight);
                    renderer.DrawSprite(TileSprite);
                }
            }
        }
    }
}