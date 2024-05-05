using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary
/// Defines generic Component base class
/// </summary>
namespace CybrEngine {
    public abstract class Component : Object {
        public static T Create<T>(GameObject entity) where T : Component{
            var component = Builder.Component<T>();
            component.Entity = entity;
            return component;
        }

        private void _Cleanup() {
            Entity = null;
        }

        public virtual void Update(){ }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public GameObject Entity { get; private set; }
        public bool Unique { get; set; } = false;

        public override bool Equals(object obj) {
            return obj is Component component &&
                   Name == component.Name;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name);
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
