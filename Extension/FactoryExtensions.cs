using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public static class FactoryExtensions{
        private static T Instance<T>(this T instance) where T : Object{
            var clone = Object.Factory<T>.Instance(instance); 
            if (clone is Entity){
                Autoload.objAllocator.AddInstance(clone as Entity);
            }
            return clone;
        }

        public static Object Instance(this Object obj){
            return obj.Instance<Object>();
        }

        public static Entity Instance(this Entity obj){
            return obj.Instance<Entity>();
        }

        public static Particle Instance(this Particle particle){
            var temp = particle.Instance<Particle>();
            return temp;
        }
    }
}
