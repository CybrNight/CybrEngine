using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine {
    public class ParticleHandler : IResettable {

        private static ParticleHandler instance;
        public static ParticleHandler Instance {
            get {
                if (instance == null) {
                    instance = new ParticleHandler();
                }
                return instance;
            }
        }

        private ParticleHandler(){
            
        }

        private List<Particle> particles = new List<Particle>();

        public void Draw(SpriteBatch spriteBatch) {
            for(int i = 0; i < particles.Count; i++) {
                var particle = particles[i];
                if(!particle.IsDestroyed) {
                    particle.Draw(spriteBatch);
                }
            }
        }

        public void Update(){
            for(int i = 0; i < particles.Count; i++) {
                var particle = particles[i];
                if(!particle.IsDestroyed) {
                    particle.Update();
                } else {
                    particles.Remove(particle);
                }
            }
        }

        public Particle Emit(Particle particle, Vector2 position) {
            particle.Transform.Position = position;
            particles.Add(particle);
            return particle;
        }

        public void Reset() {
            particles.Clear();
        }
    }
}
