using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Scene : Object {
        public abstract void LoadObjects();

        protected T Instantiate<T>(Vector2 position = new Vector2()) where T : Entity{
            return ObjectAllocator.Instance.Instantiate<T>(position);
        }
    }
}
