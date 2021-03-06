﻿using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using HandelserOchLjud;
using HandelserOchLjud.Model;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using HandelserOchLjud.View.Explosion;

namespace HandelserOchLjud.View
{
    class GameView
    {
        private float crosshairSize = 0.1f;

        private Camera _camera;
        private BallSimulation _ballSimulation;
        private Rectangle _rect;
        private Vector2 _ballCenter;

        private Texture2D ballTexture,
        deadBallTexture,
        background,
        crosshair;

        
        //List<ExplosionView> explosions = new List<ExplosionView>();
        //List<SmokeSystem> smokes = new List<SmokeSystem>();
        public GameView(GraphicsDeviceManager graphics, BallSimulation BallSimulation, ContentManager Content, Camera camera)
        {
            LoadSprites(Content, graphics);
            _camera = camera;
            _ballSimulation = BallSimulation;

            int screenSize = _camera.getSizeOfField();
            _rect = new Rectangle(_camera.bordersize, _camera.bordersize, screenSize, screenSize);
        }

        public void Draw(SpriteBatch spriteBatch, float timeElapsed, List<SmokeSystem> smokes)
        {
            DrawCrosshair(spriteBatch);
            foreach (Ball ball in _ballSimulation.getBalls())
            {
                spriteBatch.Draw(background, _rect, Color.White);
                Vector2 ballLogicalLocation = ball.position;
                float scale = _camera.Scale(ball.radius * 2, ballTexture.Width);
                var ballVisualLocation = _camera.convertToVisualCoords(ballLogicalLocation, scale);
                if(ball.isBallDead)
                {
                    spriteBatch.Draw(deadBallTexture, ballVisualLocation, null, Color.White, 0, _ballCenter, scale, SpriteEffects.None, 0.9f);
                }
                else
                    spriteBatch.Draw(ballTexture, ballVisualLocation, null, Color.White, 0, _ballCenter, scale, SpriteEffects.None, 0.9f);
            }

            foreach (SmokeSystem smokeSystems in smokes)
            {
                smokeSystems.Update(timeElapsed);
                smokeSystems.Draw(spriteBatch, _camera);
            }
            
        }
        public void DrawCrosshair(SpriteBatch spriteBatch)
        {
            float scale = _camera.Scale(crosshairSize, crosshair.Width);
            spriteBatch.Draw(crosshair, _camera.centerTextureAtMouse(crosshair, scale), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 1);
        }
        public void LoadSprites(ContentManager Content, GraphicsDeviceManager graphics)
        {
            ballTexture = Content.Load<Texture2D>("aqua-ball");
            deadBallTexture = Content.Load<Texture2D>("aqua-ball-dead");
            crosshair = Content.Load<Texture2D>("Crosshair");
            _ballCenter = new Vector2(ballTexture.Width / 2, ballTexture.Height / 2);


            background = new Texture2D(graphics.GraphicsDevice, 1, 1);
            background.SetData(new Color[] { Color.Black });
        }
        public float CrosshairSize
        {
            get { return crosshairSize; }
        }


    }
}
