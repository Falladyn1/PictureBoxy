using System;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PictureBoxy
{
    internal class Square : IFigure
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
        [JsonInclude]
        private int red;
        [JsonInclude]
        private int green;
        [JsonInclude]
        private int blue;


        private static Random rnd = new Random();

        public bool IsFinished => false;

        [JsonConstructor]
        public Square() { }

        public Square(float startX, float startY, float startSize, float startVX, float startVY,int r, int g, int b)
        {
            x = startX;
            y = startY;
            size = startSize;
            vx = startVX;
            vy = startVY;
            red = r;
            green = g;
            blue = b;
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
            using (Pen pn = new Pen(Color.FromArgb(red, green, blue), 2))
            {
                g.DrawRectangle(pn, x, y, size, size);
            }
        }
    }
}