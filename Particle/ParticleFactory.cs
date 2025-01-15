using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public partial class Particle {
        internal static Particle Construct<T>(Color color, string name, float life = 0.5f) {
            var particle = (Particle)Activator.CreateInstance(typeof(T));

            particle.Transform = new Transform();
            particle.Color = color * (0.75f);
            particle.Life = 1 / life;
            particle.SetTexture(name);

            return particle;

        }
    }
}
