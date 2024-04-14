using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary
/// Defines generic Component base class
/// </summary>
namespace CybrEngine {
    public abstract class Component : Object, IComponent {

        protected readonly string _name;
        protected readonly bool _unique = false;
        protected readonly Type _cgroup;

        public Entity Owner { get; set; }
        public bool Unique { get { return _unique; } }
        public Type ComponentGroup { get { return typeof(IComponent);} }

        // Overload bool to allow null checks
        public static implicit operator bool(Component component) {
            return (component != null);
        }
    }
}
