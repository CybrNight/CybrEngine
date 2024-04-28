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

        public abstract void Destroy();
        public bool Unique { get; protected set;  }
        public string Name { get; protected set; }
    }
}
