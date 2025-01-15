using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if(Particle) {
                Particle.Color = color;
            }
        }

        public void SetTexture(string name){
            if(Particle) {
                Particle.SetTexture(name);
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            for(int i = 0; i < particles.Count; i++) {
                var particle = particles[i];
                if(!particle.IsDestroyed) {
                    spriteBatch.Draw(particle.Texture, particle.Bounds, null, particle.Color, 0f, Entity.Transform.Origin, SpriteEffects.None, 1.0f);
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

            if(p) {
                p.Bounds = Entity.Bounds;
                p.Transform.Position = Entity.Position;
                p.Bounds = Entity.Bounds;
                particles.Add(p);
            }
        }

        public void Reset() {
            particles.Clear();
        }
    }
}
