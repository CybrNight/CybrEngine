using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Component {
        public string Name { get; set; }

        public abstract Type CType { get; }
    
        public static implicit operator bool(Component c) {
            return c != null;
        }
                  
    }
}
