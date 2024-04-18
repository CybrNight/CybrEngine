using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary
/// Defines generic Component base class
/// </summary>
namespace CybrEngine {
    public abstract class Component : IComponent {

        protected readonly Type _cgroup;

        //public abstract void Update();

        public string Name { get; set; }
        public Entity Owner { get; set; }
        public bool Unique { get; set; }
        public virtual Type ComponentGroup { get { return typeof(IComponent);} }
        public virtual void Init() { }

        // Overload bool to allow null checks
        public static implicit operator bool(Component component) {
            return (component != null);
        }
    }
}
