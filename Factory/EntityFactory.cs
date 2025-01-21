using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public partial class Entity {
        internal static Entity Construct<T>() {
            var entity = (Entity)Activator.CreateInstance(typeof(T));

            //Set member variables of gameObject
            entity.Name = entity.GetType().ToString();
            entity.Name = entity.GetType().Name;

            entity.ID = GLOBAL_ID++;
            entity.Transform = new Transform();
            entity.ObjectAllocator = Autoload.EntityAllocator;
            entity.ComponentAllocator = Autoload.ComponentAllocator;
            entity.ParticleHandler = Autoload.ParticleHandler;

            Debug.WriteLine(entity.ID);

            return entity;
        }
    }

}
