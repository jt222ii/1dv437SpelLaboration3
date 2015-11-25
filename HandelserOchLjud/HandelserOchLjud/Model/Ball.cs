using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.Model
{
    class Ball
    {
        float _radius = 0.05f;
        float maxSpeed = 1f;
        float minSpeed = 0.2f;
        Vector2 velocity;
        Vector2 _position;
        Vector2 randomDirection;
        
        public Ball(Random rand)
        {
            _position = new Vector2(0.3f, 0.2f);
            randomDirection = new Vector2((float)rand.NextDouble() - 0.5f, (float)rand.NextDouble() - 0.5f);
            //normalize to get it spherical vector with length 1.0
            randomDirection.Normalize();
            //add random length between 0 to maxSpeed
            randomDirection = randomDirection * ((float)rand.NextDouble() * maxSpeed + minSpeed);
            velocity = randomDirection;
        }
        public float radius
        {
            get
            {
                return _radius;
            }
        }
        public Vector2 speed
        {
            get
            {
                return velocity;
            }
        }
        public Vector2 position
        {
            get
            {
                return _position;
            }
        }
        public void setNewPos(float time)
        {
            _position += velocity * time;
        }

        public void setNewSpeedX()
        {
            velocity.X = -speed.X;
        }
        public void setNewSpeedY()
        {
            velocity.Y = -speed.Y;
        }
    }
}
