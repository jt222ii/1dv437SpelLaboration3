using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.Model
{
    class BallSimulation
    {
        private static Random rand = new Random();
        private List<Ball> balls = new List<Ball>();
        private List<Ball> recentlyKilledBalls;
        int maxBalls = 10;
        public BallSimulation()
        {
            for (int i = 0; i < maxBalls; i++)
            {
                balls.Add(new Ball(rand));
            }
        }

        public void Update(float time)
        {
            foreach (Ball ball in balls)
            {
                if (!ball.isBallDead)
                {
                    ballCollision(ball);
                    ball.setNewPos(time);
                }
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

        public void setDeadBalls(float coordX, float coordY, float crosshairSize)
        {
            recentlyKilledBalls = new List<Ball>();
            foreach (Ball ball in balls)
            {
                if (!ball.isBallDead)
                {
                    if (ball.position.X + ball.radius > coordX - crosshairSize &&
                        ball.position.X - ball.radius < coordX + crosshairSize &&
                        ball.position.Y + ball.radius > coordY - crosshairSize &&
                        ball.position.Y - ball.radius < coordY + crosshairSize)
                    {
                        recentlyKilledBalls.Add(ball);
                        ball.isBallDead = true;
                    }
                }
            }
        }
        
        public List<Ball> RecentlyKilledBalls
        {
            get { return recentlyKilledBalls; }
        }

        public List<Ball> getBalls()
        {
            return balls;
        }
    }
}
