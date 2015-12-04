using HandelserOchLjud.Model;
using HandelserOchLjud.View.Explosion;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud
{
    class ExplosionSystem
    {
        Camera camera;
        Texture2D splitterTexture,
        splitterSecondTexture,
        smokeTexture,
        shockwaveTexture,
        explosionTexture;
        SoundEffect firesound;
        public ExplosionSystem(Camera _camera, ContentManager Content)
        {
            camera = _camera;
            LoadTextures(Content);
        }

        public ExplosionView newExplosion(SpriteBatch spriteBatch, Vector2 startLocation, float explosionSize)
        {
            firesound.Play(0.1f, 0, 0);
            return new ExplosionView(camera, spriteBatch, startLocation, explosionSize, splitterTexture, splitterSecondTexture, smokeTexture, explosionTexture, shockwaveTexture);
        }

        public SmokeSystem newDeadBallSmoke(Ball ball)
        {
            return new SmokeSystem(smokeTexture, 0.5f, ball.position, true);
        }

        public void LoadTextures(ContentManager Content)
        {
            splitterTexture = Content.Load<Texture2D>("Spark3");
            splitterSecondTexture = Content.Load<Texture2D>("Spark2");
            smokeTexture = Content.Load<Texture2D>("Smoketest");
            explosionTexture = Content.Load<Texture2D>("Fixforshittyschoolcomputer");
            shockwaveTexture = Content.Load<Texture2D>("Shockwave2");
            firesound = Content.Load<SoundEffect>("fire");
        }
    }
}
