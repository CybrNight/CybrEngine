using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public interface IComponent {


        public Entity Entity { get; set; }
        public string Name { get; set; }

        public abstract Type CType { get; }
    }
}
