namespace RpgEngine {
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Xna.Framework.Graphics;

    public class TextureStore {
        private readonly Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private Manifest _manifest;
        private GraphicsDevice _device;

        public TextureStore(Manifest manifest, GraphicsDevice device) {
            _manifest = manifest;
            _device = device;

            foreach (var texture in manifest.Textures) {
                using (var fileStream = new FileStream(texture.Path, FileMode.Open)) {
                    _textures[texture.Name] = Texture2D.FromStream(_device, fileStream);
                }
            }
        }
        

        public Texture2D Find(string name) {
            if (_textures.ContainsKey(name)) {
                return _textures[name];
            }
            Console.WriteLine("Couldn't find texture: " + name);
            throw new FileNotFoundException();
        } 
    }
}