using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public partial class Entity{
        internal static class EntityFactory<T> where T : Entity {
            internal static T Construct(ObjectAllocator objHandler) {
                var entity = Builder.Entity<T>();

                //Set member variables of gameObject
                entity.Name = entity.GetType().ToString();
                entity.Name = entity.GetType().Name;
                entity.ObjectAllocator = Autoload.ObjectAllocator;
                entity.ComponentAllocator = Autoload.ComponentAllocator;    
                entity.ParticleHandler = Autoload.ParticleHandler; 
                return entity;
            }
        }

    }
}
