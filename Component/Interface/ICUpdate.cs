using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public interface ICUpdate : IComponent {
        Texture2D sprite { get; }

        public void Update();
    }
}
