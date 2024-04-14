using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Object {

        public virtual void Destroy() { Destroyed = true; }

        protected bool Destroyed { get; set; }
        protected bool BeingDestroyed { get; set; }

        public bool IsDestroyed {
            get {
                return Destroyed || BeingDestroyed;
            }
        }
    }
}
