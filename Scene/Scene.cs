using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace CybrEngine {
    public abstract class Scene : Object {
        public class Node {
            public Entity data;
            private List<Entity> children;

            public Node(Entity entity) {
                children = new List<Entity>();
                data = entity;
            }

            public void AddChild(Entity node){
                children.Add(node);   
            }

            public List<Entity> GetChildren(){
                return children;
            }
        }

        private Node root;
        private List<Entity> entities;


        public abstract void LoadObjects();

        public void Instantiate(Entity entity) {
            Node node = new Node(entity);
            root.AddChild(entity); 
            entities.Add(entity);
        }

        protected Entity Instantiate<T>(Vector2 position = new Vector2()) where T : Entity {
            return Autoload.EntityAllocator.Instantiate<T>(position);
        }

        protected Entity Instantiate<T>(float x, float y) where T : Entity {
            return Instantiate<T>(new Vector2(x, y));
        }

        public void Update() {
            var nodes = root.GetChildren();
            for (int i = 0; i < nodes.Count; i++){
                var node = nodes[i];
                node.SendMessage("_Update");
            }
        }

        public void Draw(SpriteBatch spriteBatch) {

        }
    }
}
