using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public interface IObject {
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
    }
}
