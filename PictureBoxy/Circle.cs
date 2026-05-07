using System;
using System.Drawing;

namespace PictureBoxy
{
    internal class Circle
    {
        private float x;
        private float y;
        private float size;
        private float vx;
        private float vy;
        private Color color;
        private static Random rnd = new Random();

        public Circle(float startX, float startY, float startSize, float startVX, float startVY)
        {
            x = startX;
            y = startY;
            size = startSize;
            vx = startVX;
            vy = startVY;
            color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        public void Update(int width, int height)
        {
            x += vx;
            y += vy;

            if (x + size >= width || x <= 0) vx = -vx;
            if (y + size >= height || y <= 0) vy = -vy;
        }

        public void Draw(Graphics g)
        {
            using (Pen pn = new Pen(color, 2))
            {
                g.DrawEllipse(pn, x, y, size, size);
            }
        }
    }
}