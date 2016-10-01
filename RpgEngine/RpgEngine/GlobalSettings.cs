namespace RpgEngine {
    public class GlobalSettings {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Manifest { get; set; }
        public string MainScript { get; set; }
        public string OnUpdate { get; set; }
        public bool Webserver { get; set; }
        public bool Debug { get; set; }

        public GlobalSettings() {
            Instance = this;
        }

        internal static GlobalSettings Instance { get; private set; }
    }
}