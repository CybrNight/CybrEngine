using Microsoft.Xna.Framework.Graphics;

namespace CybrEngine {
    public class SpriteSheet {

        public Texture2D Source { get; private set; }

        private SpriteSheet() { }
        public SpriteSheet(string path) {
            SetSheet(path);
        }

        private void SetSheet(string path) {
            Source = Assets.GetTexture(path);
        }

        public void Destroy() {
            Source = null;
        }


    }
}
