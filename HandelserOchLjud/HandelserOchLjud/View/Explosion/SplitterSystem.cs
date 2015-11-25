using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.View.Explosion
{
    class SplitterSystem
    {
        private float particlesLifeTime = 4f;
        private float timeLived = 0;
        private List<SplitterParticle> particles = new List<SplitterParticle>();
        private int maxParticles = 500;
        private static Random rand = new Random();
        public SplitterSystem(Texture2D spark, Texture2D secondSpark, SpriteBatch spriteBatch, Camera camera, float scale, Vector2 startLocation)
        {
            int i = 0;
            while(particles.Count < maxParticles)
            {
                if (i % 2 == 0)
                {
                    particles.Add(new SplitterParticle(spark, rand, spriteBatch, camera, scale, startLocation, particlesLifeTime));
                }
                else
                {
                    particles.Add(new SplitterParticle(secondSpark, rand, spriteBatch, camera, scale, startLocation, particlesLifeTime));
                }
                i++;
            }
        }

        public void Draw()
        {
            foreach (SplitterParticle particle in particles)
            {
                particle.Draw();
            }
        }

        public void Update(float timeElapsed)
        {           
            foreach (SplitterParticle particle in particles)
            {
                particle.move(timeElapsed);
            }
            timeLived += timeElapsed;
            if(timeLived >= particlesLifeTime)
            {
                particles.Clear();
            }
        }

    }
}
