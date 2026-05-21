using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PictureBoxy
{
    internal class Circle : IFigure
    {
        [JsonInclude]
        [Description("Pozycja x")]
        private float x;

        [JsonInclude]
        [Description("Pozycja y")]
        private float y;
        
        [JsonInclude]
        [Description("Promień")]
        private float size;
        
        [JsonInclude]
        [Description("Prędkość x")]
        private float vx;
        
        [JsonInclude]
        [Description("Prędkość y")]
        private float vy;
        
        [JsonInclude]
        [Description("Kolor czerwony (R)")]
        private int red;
        
        [JsonInclude]
        [Description("Kolor zieolny (G)")]
        private int green;
        
        [JsonInclude]
        [Description("Kolor niebieski (B)")]
        private int blue;

        private static Random rnd = new Random();

        public bool IsFinished => false;

        [JsonConstructor]
        public Circle(){}

        public Circle(float startX, float startY, float startSize, float startVX, float startVY, int r, int g, int b)
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
                g.DrawEllipse(pn, x, y, size, size);
            }
        }
    }
}