using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class ParticleStore : IResettable {

        private static ParticleStore instance;
        public static ParticleStore Instance {
            get {
                if(instance == null) {
                    instance = new ParticleStore();
                }
                return instance;
            }
        }

        private ParticleStore() {

        }

        /// <summary>
        /// Stores instance of Particle to Asset store
        /// </summary>
        /// <param name="name"></param>
        /// <param name="particle"></param>
        public static void AddParticle(string name, Particle particle) {
            particles.Add(name, particle);
        }

        /// <summary>
        /// Retrieves Particle instance from Asset store
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Particle GetParticle(string name) {
            return particles[name];
        }

        public static void RemoveParticle(string name){
            particles.Remove(name);
        }

        private static Dictionary<string, Particle> particles = new Dictionary<string, Particle>();

        public Particle Emit(Particle particle, Vector2 position) {
            return default(Particle);   
        }

        public void Reset() {
            particles.Clear();
        }
    }
}
