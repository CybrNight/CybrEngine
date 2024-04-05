using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class Handler {

        private static List<GameObject> gameObjects = new List<GameObject>();

        public static void Instantiate(GameObject gameObject){
            Console.WriteLine("Instantiated!");
            gameObjects.Add(gameObject);
        }

        public void Update(){
            foreach(GameObject obj in gameObjects){
                Console.WriteLine(obj.position.ToString());
                if(obj.IsDestroyed)
                    gameObjects.Remove(obj);    

                obj.Update();
            }
        }
    
        public void Draw(SpriteBatch spriteBatch){
            foreach(GameObject obj in gameObjects){
                obj.Draw(spriteBatch);
            }
        }    
    }
}
