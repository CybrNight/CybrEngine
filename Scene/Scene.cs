using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CybrEngine {
    public abstract class Scene : Object {
        public Scene(){
            Root = new Node();
        }

        public class Node {
            public Entity Entity {  get; private set; }
            public Node Parent { get; private set; }
            public List<Node> Children { get; private set; }

            public Node(){
                Entity = null;
                Children = new List<Node>();
                Parent = null;
            }

            public Node(Entity entity, Node parent) {
                Children = new List<Node>();
                Entity = entity;
                Parent = parent;
            }

            public Entity Find<T>() where T : Entity{
                if (Entity && !Entity.IsDestroyed){
                    if (Entity is T){
                        return Entity;
                    }
                }
                Entity node = default;
                for (int i = 0; i < Children.Count; i++){
                    node = Children[i].Find<T>();

                    if (node != default){
                        return node;
                    }
                }
                return node;
            }

            // Called originally from Root Node.
            // Recursively sends _Update Message to all child Entity nodes in tree
            public void Update() {
                if(Entity && !Entity.IsDestroyed) {
                    Entity.SendMessage("_Update");
                    
                }
                for(int i = 0; i < Children.Count; i++) {
                    var node = Children[i];

                    //If Child is Destroyed then just clean it up
                    if(node.Entity.IsDestroyed) {

                        Autoload.ComponentAllocator.RemoveEntity(Entity);
                    }

                    node.Update();
                }
            }

            public void FixedUpdate(){
                if(Entity && Entity.IsActive) {
                    Entity.SendMessage("_FixedUpdate");

                    //Update Position based on Velocity
                    Entity.Transform.Position = new Vector2(Entity.Transform.Position.X + Entity.Velocity.X,
                                             Entity.Transform.Position.Y - Entity.Velocity.Y);
                }
                for(int i = 0; i < Children.Count; i++) {
                    var node = Children[i];

                    //If Child is Destroyed then just clean it up
                    if(node.Entity.IsDestroyed) {

                        Autoload.ComponentAllocator.RemoveEntity(Entity);
                    }

                    node.FixedUpdate();
                }
            }

            /// <summary>
            /// Adds new Node as child of Root
            /// </summary>
            /// <param name="entity"></param>
            public void AddChild(Entity entity){
                var node = new Node(entity, this);
                Children.Add(node);   
            }
        }

        public Node Root { get; private set; }

        public abstract void LoadObjects();

        protected Entity Instantiate<T>(Vector2 position = new Vector2()) where T : Entity {
            return Autoload.SceneManager.Instantiate<T>(position);
        }

        protected Entity Instantiate<T>(float x, float y) where T : Entity {
            return Instantiate<T>(new Vector2(x, y));
        }

        public Entity FindEntityOfType<T>() where T : Entity {
            return Root.Find<T>();
        }

        internal void Update() {
            Root.Update();
        }

        internal void FixedUpdate(){
            Root.FixedUpdate();
        }
    }
}
