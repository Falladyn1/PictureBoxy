using System;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PictureBoxy
{
    internal class Circle : IFigure
    {
        [JsonInclude]
        private float x;
        [JsonInclude]
        private float y;
        [JsonInclude]
        private float size;
        [JsonInclude]
        private float vx;
        [JsonInclude]
        private float vy;

        private Color color;
        private static Random rnd = new Random();

        public bool IsFinished => false;

        [JsonConstructor]
        public Circle()
        {
            color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        public Circle(float startX, float startY, float startSize, float startVX, float startVY)
        {
            x = startX;
            y = startY;
            size = startSize;
            vx = startVX;
            vy = startVY;
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