﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

/// <summary
/// Defines generic Component base class
/// </summary>
namespace CybrEngine {
    public abstract class Component : Object {

        protected readonly Type _cgroup;

        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

        public bool Unique { get; set; }
        public virtual Type ComponentType { get { return typeof(Component); } }

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
