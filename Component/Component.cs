using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CybrEngine {
    /// <summary>
    /// Defines base class for Component
    /// </summary>
    public abstract class Component : Object {
        public static T Create<T>(Entity entity) where T : Component{
            var component = Builder.Component<T>();
            component.Entity = entity;
            return component;
        }

        public virtual void Draw(SpriteBatch spriteBatch) { }

        /// <summary>
        /// Owner of Component
        /// </summary>
        public Entity Entity { get; private set; }

        /// <summary>
        /// Defines if Component should be the only one allowed on Entity
        /// </summary>
        public bool Unique { get; set; } = false;

        public override bool Equals(object obj) {
            return obj is Component component &&
                   Name == component.Name;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Name);
        }

        /// <summary>
        /// Overload of bool operator
        /// </summary>
        /// <param name="component"></param>
        public static implicit operator bool(Component component) {
            return (component != null);
        }

        /// <summary>
        /// Overload == for Component comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Component left, Component right) {
            return EqualityComparer<Component>.Default.Equals(left, right);
        }

        /// <summary>
        /// Overload != for Component comparison
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Component left, Component right) {
            return !(left == right);
        }
    }
}
