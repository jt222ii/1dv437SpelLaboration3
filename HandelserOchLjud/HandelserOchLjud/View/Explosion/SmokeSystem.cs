using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.View.Explosion
{
    class SmokeSystem
    {
        private List<SmokeParticle> particles = new List<SmokeParticle>();
        private int maxParticles = 200;
        private int particlesLifeTime = 3;
        private static Random rand = new Random();
        private Texture2D smoke;
        private float scale;
        private Vector2 _startLocation;

        public SmokeSystem(Texture2D smokeTexture, float Scale, Vector2 startLocation)
        {
            _startLocation = startLocation;
            scale = Scale;
            smoke = smokeTexture;
            while (particles.Count<(maxParticles))
            {
                addSmoke();
            }
        }
        private void addSmoke()
        {
            if (particles.Count < maxParticles)
            {
                particles.Add(new SmokeParticle(smoke, rand, particlesLifeTime, scale, _startLocation));
            }
        }
        public void Update(float elapsedTime)
        {
            // I want it to continously add smokeparticles. If the lifetime of a particle is 5 seconds and only 10 are to be rendered they should be added spread out over those 10 seconds
            foreach (SmokeParticle particle in particles)
            {
                particle.move(elapsedTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            foreach (SmokeParticle particle in particles)
            {
                particle.Draw(spriteBatch, camera);
            }
        }
    }
}
