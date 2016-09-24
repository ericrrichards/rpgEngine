using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgEngine {
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
}
