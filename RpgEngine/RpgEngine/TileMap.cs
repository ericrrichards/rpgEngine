using System.Collections.Generic;

namespace RpgEngine {
    using Microsoft.Xna.Framework.Graphics;

    public class TileMap {
        public int Height { get; set; }
        public int Width { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public List<TileLayer> Layers { get; set; }
        public List<TileSet> TileSets { get; set; }
        
    }

    public class TileLayer {
        public List<int> Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
    }

    public class TileSet {
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public string Image { get; set; }
    }


    //public class Map {
    //    public int X { get; set; }
    //    public int Y { get; set; }
    //    public int CamX { get; set; }
    //    public int CamY { get; set; }

    //    public TileMap MapDef { get; set; }
    //    public Texture2D TextureAtlas { get; set; }
    //    public Sprite TileSprite { get; set; }
    //    public TileLayer Layer { get; set; }
    //    public int Height { get; set; }
    //    public int Width { get; set; }
    //    public List<int> Tiles { get; set; }
         
    //}
}
