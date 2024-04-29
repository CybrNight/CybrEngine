﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Object {

        public string Name { get; protected set; }

        private bool Active { get; set; } = false;

        public bool IsCreated { get; private set; } = false;
        protected bool Destroyed { get; set; }
        protected bool BeingDestroyed { get; set; }
        public int ID { get; private set; }
        private static int GLOBAL_ID { get; set; } = 0;

        public bool IsActive {
            get { return Active || IsDestroyed; }
        }

        public bool IsDestroyed {
            get {
                return BeingDestroyed || Destroyed;
            }
        }

        public void Awake() {
            if(IsCreated) return;

            _Awake();
            Active = true;
        }

        public void Start() {
            if(IsCreated) return;
            _Start();
            IsCreated = true;
        }

        public virtual void _Awake() { }
        public virtual void _Start() { }
        public virtual void _Update() { }

        public void SetActive(bool value = true) { Active = value; }
        public void Destroy() { BeingDestroyed = true; _Cleanup(); Destroyed = true; }

        public void Cleanup() {
            if(BeingDestroyed && Destroyed) return;
            _Cleanup();
        }

        protected virtual void _Cleanup() { }

        public override bool Equals(object obj) {
            return obj is Object @object &&
                   ID == @object.ID;
        }

        public override int GetHashCode() {
            return HashCode.Combine(ID);
        }

        protected Object() {


            ID = GLOBAL_ID++;
        }

        public static bool operator ==(Object left, Object right) {
            return EqualityComparer<Object>.Default.Equals(left, right);
        }

        public static bool operator !=(Object left, Object right) {
            return !(left == right);
        }
    }
}
