using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.View.Explosion
{
    class SmokeParticle
    {
        private Vector2 randomDirection;
        private float maxSpeed = 0.2f;
        private Texture2D _smoke;

        private Vector2 startPosition;
        private Vector2 acceleration = new Vector2(0.0f, -0.2f);
        private Vector2 velocity;


        private float particleMinSize = 0;
        private float particleMaxSize = 0.4f;

        private float lifePercent;
        private float timeLived;
        private float randomRotation;
        private float maxTimeToLive;
        private float scale;

        private float fade;
        private float particleSize;
        private float rotation;

        private Vector2 position;


        Random rand;

        public SmokeParticle(Texture2D smoke, Random random, float timeToLive, float Scale, Vector2 startLocation)
        {
            startPosition = startLocation;
            scale = Scale;
            _smoke = smoke;
            rand = random;
            maxTimeToLive = timeToLive;
            initOrResetParticle();
            randomRotation = 0.025f * ((float)rand.NextDouble() - (float)rand.NextDouble());
        }
        public void move(float elapsedTime)
        {

            rotation += randomRotation;
            fade -= elapsedTime / maxTimeToLive;
            timeLived += elapsedTime;
            lifePercent = timeLived / maxTimeToLive;
            particleSize = (particleMinSize + lifePercent * particleMaxSize)*scale;
            velocity = (elapsedTime * acceleration + velocity);
            position = elapsedTime * velocity*scale + position;
        }

        public bool lifeIsOver()
        {
            return timeLived >= maxTimeToLive;
        }

        public void initOrResetParticle()
        {
            particleSize = particleMinSize;
            fade = 1;
            timeLived = 0;
            position = startPosition;
            rotation = 0;
            randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between 0 to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed);
            velocity = randomDirection;
        }
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            float scale = camera.Scale(particleSize, _smoke.Width);
            Color color = new Color(fade, fade, fade, fade);
            spriteBatch.Draw(_smoke, camera.convertToVisualCoords(position, scale), null, color, rotation, randomDirection, scale, SpriteEffects.None, 1f);
        }
    }
}
