using HandelserOchLjud.View.Explosion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud
{
    class ExplosionView
    {
        private Camera _camera;
        private SpriteBatch _spriteBatch;


        private SplitterSystem splitterSystem;
        private SmokeSystem smokeSystem;
        private Explosion2d explosion;
        private shockwave shockwave;
        float time = 0;
        float timeBeforeDelete = 3;
        public ExplosionView(Camera camera, SpriteBatch spriteBatch, Vector2 startLocation, float componentsSize, Texture2D splitterTexture, Texture2D splitterSecondTexture, Texture2D smokeTexture, Texture2D explosionTexture, Texture2D shockwaveTexture)
        {
            _camera = camera;
            _spriteBatch = spriteBatch;

            splitterSystem = new SplitterSystem(splitterTexture, splitterSecondTexture, _spriteBatch, _camera, componentsSize, startLocation);
            smokeSystem = new SmokeSystem(smokeTexture, componentsSize, startLocation);
            explosion = new Explosion2d(_spriteBatch, explosionTexture, _camera, componentsSize, startLocation);
            shockwave = new shockwave(_spriteBatch, shockwaveTexture, _camera, componentsSize, startLocation);
        }
        public void UpdateExplosion(float timeElapsed)
        {
            time += timeElapsed;
            splitterSystem.Update(timeElapsed);
            smokeSystem.Update(timeElapsed);
        }
        public void DrawExplosion(float timeElapsed)
        {
            splitterSystem.Draw(timeElapsed);
            smokeSystem.Draw(_spriteBatch, _camera);
            explosion.Draw(timeElapsed);
            shockwave.Draw(timeElapsed);      
        }
        public bool livedItsTime()
        {
            return time >= timeBeforeDelete;
        }
    }
}
