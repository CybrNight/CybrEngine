using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public interface ICDraw : IComponent {
        Texture2D sprite { get; }

        public void Draw(SpriteBatch batch);
        public void SetSprite(string path);
    }
}
