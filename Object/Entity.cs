using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine{
    public static class EntityExtensions {
        public static Entity Instance(this Entity obj) {
            return GameObject.Factory<Entity>.Instance(obj);
        }
    }

    /// <summary>
    /// Defines abstract class for Entity
    /// An Entity is a GameObject that is assumed to be physical
    /// </summary>
    public class Entity : GameObject {

        public Sprite Sprite { get; protected set; }
        public string Path { get; set; }

        private void _Awake(){
            Sprite = GetComponent<Sprite>();
            Sprite.SetTexture(Assets.GetTexture(Path));
        }

        public void SetSprite(string path){
            
        }

        protected Entity(): base() { }
    }
}
