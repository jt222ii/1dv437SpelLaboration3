using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandelserOchLjud
{
    class Camera
    {
        private int sizeOfField;
        private int windowSizeX;
        private int windowSizeY;
        public void setSizeOfField(Viewport port)
        {
            windowSizeX = port.Width;
            windowSizeY = port.Height;
            if (windowSizeX < windowSizeY)
            {
                sizeOfField = windowSizeX;
            }
            else
            {
                sizeOfField = windowSizeY;
            }
        }
        public Vector2 convertToVisualCoords(Vector2 coords, float width, float height, float scale)
        {
            float visualX = coords.X * sizeOfField - (width / 2) * scale;
            float visualY = coords.Y * sizeOfField - (height / 2) * scale;
            return new Vector2(visualX, visualY);
        }

        public float Scale(float size, float width)
        {
            return sizeOfField * size / width;
        }

        public int getSizeOfField()
        {
            return sizeOfField;
        }
    }
}
