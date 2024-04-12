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


        public void Test(){ Debug.Write("");}
        public Entity Entity { get; set; }
        public string Name { get; }

        public abstract Type CType { get; }
    }
}
