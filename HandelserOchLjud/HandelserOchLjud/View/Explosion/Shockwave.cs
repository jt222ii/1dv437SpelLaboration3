
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud.View.Explosion
{
    class shockwave
    {
        private Vector2 position;
        private Camera _camera;
        private Texture2D texture;
        private float scale;
        private SpriteBatch _spriteBatch;
        private float fade = 1;
        private float particleMinSize = 0;
        private float particleMaxSize = 0.8f;
        private float maxTimeToLive = 0.8f;
        private float timeLived = 0;
        private float particleSize;
        private float lifePercent;

        public shockwave(SpriteBatch spriteBatch, Texture2D shockwaveTexture, Camera camera, float Scale, Vector2 startLocation)
        {
            position = startLocation;
            scale = Scale;
            _camera = camera;
            texture = shockwaveTexture;
            _spriteBatch = spriteBatch;
        }

        public void Draw(float elapsedTime)
        {
            fade -= elapsedTime / maxTimeToLive;
            timeLived += elapsedTime;
            lifePercent = timeLived / maxTimeToLive;
            particleSize = (particleMinSize + lifePercent * particleMaxSize) * scale;
            Color color = new Color(fade, fade, fade, fade);
            float texturescale = _camera.Scale(particleSize, texture.Width);
            _spriteBatch.Draw(texture, _camera.convertToVisualCoords(position, texturescale), null, color, 0, new Vector2(texture.Width/2, texture.Height/2), texturescale, SpriteEffects.None, 1f);
        }
    }
}
