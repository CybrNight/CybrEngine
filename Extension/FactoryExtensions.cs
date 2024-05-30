using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public static class FactoryExtensions{
        private static T Instance<T>(this T instance) where T : Object{
            var clone = Object.Factory<T>.Instance(instance); 
            if (clone is GameObject){
                Autoload.objHandler.AddInstance(clone as GameObject);
            }
            return clone;
        }

        public static Object Instance(this Object obj){
            return obj.Instance<Object>();
        }

        public static GameObject Instance(this GameObject obj){
            return obj.Instance<GameObject>();
        }

        public static Entity Instance(this Entity obj){
            return obj.Instance<Entity>();
        }

        public static Particle Instance(this Particle particle){
            var temp = particle.Instance<Particle>();
            temp.Bounds = particle.Bounds;
            temp.Color = particle.Color;
            return temp;
        }
    }
}
