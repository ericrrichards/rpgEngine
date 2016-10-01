namespace RpgEngine {
    using System.Collections.Generic;
    using System.Linq;

    public class Manifest {
        internal static Manifest Instance { get; private set; }

        public List<Asset> Scripts { get; set; }
        public List<Asset> Textures { get; set; }


        public Manifest() {
            Instance = this;
        }

        public bool AssetExists(string name) {
            return Scripts.Any(a => a.Name == name) || Textures.Any(a => a.Name == name);
        }
    }

    public class Asset {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}