using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace CybrEngine {
    public abstract class Scene : Object {
        private List<Entity> entities;

        public abstract void LoadObjects();

        protected Entity Instantiate<T>(Vector2 position = new Vector2()) where T : Entity{
            return Autoload.ObjectAllocator.Instantiate<T>(position);
        }

        protected Entity Instantiate<T>(float x, float y) where T : Entity {
            return Instantiate<T>(new Vector2(x, y));
        }

        public void Update(){

        }

        public void Draw(SpriteBatch spriteBatch){

        }
    }
}
