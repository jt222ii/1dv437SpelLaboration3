﻿using Microsoft.Xna.Framework;
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
        public int bordersize = 32;
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
            sizeOfField -= bordersize * 2;
        }
        public Vector2 convertToVisualCoords(Vector2 coords, float scale, float? width = 0, float? height = 0)
        {
            float visualX = coords.X * sizeOfField + bordersize - ((float)width / 2) * scale;
            float visualY = coords.Y * sizeOfField + bordersize - ((float)height / 2) * scale;
            return new Vector2(visualX, visualY);
        }
        public Vector2 convertMousePosToLogicalCoords(Vector2 visualCoords)
        {
            float logicalX = (visualCoords.X-bordersize)/sizeOfField;
            float logicalY = (visualCoords.Y-bordersize)/sizeOfField;
            return new Vector2(logicalX, logicalY);
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
