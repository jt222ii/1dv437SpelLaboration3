using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.View.Explosion
{
    class SplitterParticle
    {
        private float particleSize = 0.01f;
        private Vector2 randomDirection;
        private float maxSpeed = 1f;
        private Texture2D _spark;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 acceleration = new Vector2(0.0f, 0.7f);
        private Vector2 startPos;
        private float particleLifeTime;
        private float fade = 1;
        private SpriteBatch _spriteBatch;
        private Camera _camera;
        public SplitterParticle(Texture2D spark, Random rand, SpriteBatch spriteBatch, Camera camera, float scale, Vector2 StartLocation, float particlesLifeTime)
        {
            particleLifeTime = particlesLifeTime;
            startPos = StartLocation;
            position = StartLocation;
            acceleration = acceleration * scale;
            particleSize = particleSize * scale;
            _spark = spark;
            _camera = camera;
            _spriteBatch = spriteBatch;
            randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.7f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between 0 to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed);
            velocity = randomDirection*scale;
        }
        public void move(float elapsedTime)
        {
            fade -= elapsedTime / particleLifeTime;
            velocity = elapsedTime * acceleration + velocity;
            position = elapsedTime * velocity + position;
            collide();
        }
        public void Draw()
        {
            float scale = _camera.Scale(particleSize, _spark.Width);
            Color color = new Color(fade, fade, fade, fade);
            _spriteBatch.Draw(_spark, _camera.convertToVisualCoords(position, scale), null, color, 0, randomDirection, scale, SpriteEffects.None, 0.9f);
        }
       
        //for funzies will need to remove on the next lab
        public void collide()
        {
            if(position.Y > 1)
            {
                velocity.Y = -velocity.Y * 0.65f;
                velocity.X = velocity.X * 0.85f;
            }
            if (position.X >= 1 || position.X <= 0)
            {
                velocity.X = -velocity.X;
            }
        }
    }
}
