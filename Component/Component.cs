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

        public virtual void Update() { }

        public string Name { get; set; }
        public Entity Owner { get; set; }
        public bool Unique { get; set; }
        public virtual Type ComponentGroup { get { return typeof(IComponent);} }
        public virtual void Init() { }

        public override bool Equals(object obj) {
            return obj is Component component &&
                   Name == component.Name &&
                   EqualityComparer<Entity>.Default.Equals(Owner, component.Owner);
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name, Owner);
        }

        // Overload bool to allow null checks
        public static implicit operator bool(Component component) {
            return (component != null);
        }

        public static bool operator ==(Component left, Component right) {
            return EqualityComparer<Component>.Default.Equals(left, right);
        }

        public static bool operator !=(Component left, Component right) {
            return !(left == right);
        }
    }
}
