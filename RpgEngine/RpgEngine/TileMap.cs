using System.Collections.Generic;

namespace RpgEngine {
    using System.IO;
    using System.Linq;

    using Newtonsoft.Json;

    public class TileMap {
        public int Height { get; set; }
        public int Width { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public List<TileLayer> Layers { get; set; }
        public List<TileSet> TileSets { get; set; }

        public static TileMap LoadMap(string filename) {
            if (Manifest.Instance.AssetExists(filename)) {
                return JsonConvert.DeserializeObject<TileMap>(File.ReadAllText(Manifest.Instance.Scripts.First(a => a.Name == filename).Path));
            }
            throw new FileNotFoundException(filename);
        }   
    }

    public class TileLayer {
        public List<int> Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
    }

    public class TileSet {
        public string Name { get; set; }
        public int FirstGid { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public string Image { get; set; }
    }
}
