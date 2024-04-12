using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Component {
        public Entity entity;
        
        protected readonly bool _unqiue;

        public virtual Type CType { get { return this.GetType(); } }
        public virtual bool Unique { get; }
        public virtual string Name { get; protected set; }

        public static implicit operator bool(Component c) {
            return c != null;
        }


    }
}
