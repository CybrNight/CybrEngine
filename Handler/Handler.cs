using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public abstract class Handler {


        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Called every game tick
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called every physics tick
        /// </summary>
        public virtual void FixedUpdate() { }

        /// <summary>
        /// Called every draw tick
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch) { }

        protected Handler(){ }
    }

}
