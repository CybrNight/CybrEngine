using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public interface IComponent {

        public virtual void Update(){ }

        public bool Unique { get; }
        public Entity Owner { get; set; }
        public virtual string Name { get { return Name; } }

        public Type ComponentGroup { get; }
    }
}
