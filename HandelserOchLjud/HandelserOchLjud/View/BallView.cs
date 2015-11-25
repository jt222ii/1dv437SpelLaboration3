using System;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using HandelserOchLjud;
using HandelserOchLjud.Model;
using Microsoft.Xna.Framework.Audio;

namespace HandelserOchLjud.View
{
    class BallView
    {
        private Camera _camera;
        private BallSimulation _ballSimulation;
        private Texture2D _ball;
        private Rectangle _rect;
        private Texture2D _background;
        private Vector2 _ballCenter;

        private Texture2D splitterTexture;
        private Texture2D splitterSecondTexture;
        private Texture2D smokeTexture;
        private Texture2D shockwaveTexture;
        private Texture2D explosionTexture;
        private SoundEffect fireSound;

        private ExplosionView explosion;
        public BallView(GraphicsDeviceManager graphics, BallSimulation BallSimulation, Texture2D ball, Camera camera, Texture2D SplitterTexture, Texture2D SplitterSecondTexture, Texture2D SmokeTexture, Texture2D ExplosionTexture, Texture2D ShockwaveTexture, SoundEffect sound)
        {
            fireSound = sound;
            splitterTexture = SplitterTexture;
            splitterSecondTexture = SplitterSecondTexture;
            smokeTexture = SmokeTexture;
            shockwaveTexture = ShockwaveTexture;
            explosionTexture = ExplosionTexture;
            _camera = camera;
            _ballSimulation = BallSimulation;
            _ball = ball;
            _ballCenter = new Vector2(_ball.Width / 2, _ball.Height / 2);
            
            int screenSize = _camera.getSizeOfField();
            _rect = new Rectangle(_camera.bordersize, _camera.bordersize, screenSize, screenSize);
            _background = new Texture2D(graphics.GraphicsDevice, 1, 1);
            _background.SetData(new Color[] { Color.Black });
        }

        public void Draw(SpriteBatch spriteBatch, float timeElapsed)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            if(explosion != null)
            {
                explosion.UpdateExplosion(timeElapsed);
                explosion.DrawExplosion(timeElapsed);
            }
            foreach (Ball ball in _ballSimulation.getBalls())
            {
                spriteBatch.Draw(_background, _rect, Color.White);
                Vector2 ballLogicalLocation = ball.position;
                float scale = _camera.Scale(ball.radius*2,_ball.Width);
                var ballVisualLocation = _camera.convertToVisualCoords(ballLogicalLocation, scale);
                spriteBatch.Draw(_ball, ballVisualLocation, null, Color.White, 0, _ballCenter, scale, SpriteEffects.None, 1);
            }
            spriteBatch.End();
        }
        
        public void NewExplosion(float mCoordX, float mCoordY, SpriteBatch spriteBatch)
        {
            fireSound.Play();
            explosion = new ExplosionView(_camera, spriteBatch, _camera.convertToLogicalCoords(new Vector2(mCoordX, mCoordY), 0.5f), 0.5f, splitterTexture, splitterSecondTexture, smokeTexture, explosionTexture, shockwaveTexture);
        }
    }
}
