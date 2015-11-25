using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.Model
{
    class BallSimulation
    {
        private List<Ball> balls = new List<Ball>();
        int maxBalls = 10;
        public BallSimulation()
        {
            for (int i = 0; i < maxBalls; i++)
            {
                balls.Add(new Ball());
            }
        }

        public void Update(float time)
        {
            foreach (Ball ball in balls)
            {
                ballCollision(ball);
                ball.setNewPos(time);
            }
        }

        public void ballCollision(Ball ball)
        {
            if (ball.position.X <= 0 + ball.radius || ball.position.X >= 1 - ball.radius)
            {
                ball.setNewSpeedX();
            }
            if (ball.position.Y <= 0 + ball.radius || ball.position.Y >= 1 - ball.radius)
            {
                ball.setNewSpeedY();
            }
        }
    }
}
