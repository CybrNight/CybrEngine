using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybrEngine{
    public class ParticleEmitter : Component, IDrawable{

        public Particle Particle {  get; private set; }

        private List<Particle> particles = new List<Particle>();

        public void SetParticle(string name){
            Particle = Assets.GetParticle(name);
        }

        public void SetParticle(string name, Color color){
            Particle = Assets.GetParticle(name); 
            Particle.Color = color;
        }

        public override void Draw(SpriteBatch spriteBatch) {
            for(int i = 0; i < particles.Count; i++) {
                var particle = particles[i];
                if(!particle.IsDestroyed) {
                    particle.Draw(spriteBatch);
                }
            }
        }

        public override void Update() {
            for(int i = 0; i < particles.Count; i++) {
                var particle = particles[i];
                if(!particle.IsDestroyed) {
                    particle.Update();
                } else {
                    particles.Remove(particle);
                }
            }
        }

        public void Emit(){
            var p = Particle.Instance();
            p.Bounds = Entity.Bounds;
            p.Transform.Position = Entity.Position;
            p.Bounds = Entity.Bounds;
            particles.Add(p);
        }

        public Particle Emit(Particle particle, Vector2 position) {
            var p = particle.Instance();
            p.Bounds = Entity.Bounds;
            p.Transform.Position = position;
            p.Bounds = Entity.Bounds;
            particles.Add(p);
            return p;
        }

        public void Reset() {
            particles.Clear();
        }
    }
}
