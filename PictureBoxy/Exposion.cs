using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureBoxy
{
    public class Explosion : IFigure
    {
        private Bitmap[] frames;
        private int currentFrame = 0;
        public int X { get; set; }
        public int Y { get; set; }

        // Nowy konstruktor przyjmujący ścieżkę do pliku
        public Explosion(string filepath, int x, int y, int rows = 2, int cols = 4)
        {
            this.X = x;
            this.Y = y;
            this.frames = PrepareAnimationFrames(filepath, rows, cols);
        }

        // Metoda przeniesiona do wnętrza klasy
        private Bitmap[] PrepareAnimationFrames(string filepath, int rows, int cols)
        {
            try
            {
                if (!System.IO.File.Exists(filepath)) return new Bitmap[0];

                Bitmap[] animationFrames = new Bitmap[rows * cols];

                using (Bitmap spriteSheet = new Bitmap(filepath))
                {
                    int frameWidth = spriteSheet.Width / cols;
                    int frameHeight = spriteSheet.Height / rows;
                    int index = 0;

                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            Rectangle cropArea = new Rectangle(col * frameWidth, row * frameHeight, frameWidth, frameHeight);
                            // Używamy formatu 32bppPArgb dla zachowania przezroczystości (PNG) i wydajności
                            animationFrames[index] = spriteSheet.Clone(cropArea, PixelFormat.Format32bppPArgb);
                            index++;
                        }
                    }
                }
                return animationFrames;
            }
            catch
            {
                return new Bitmap[0];
            }
        }

        public void Draw(Graphics g)
        {
            if (frames == null || frames.Length == 0) return;

            // Rysujemy klatkę wyśrodkowaną na punkcie X, Y
            Bitmap img = frames[currentFrame];
            g.DrawImage(img, X - img.Width / 2, Y - img.Height / 2);
        }

        public void Update(int width, int height)
        {
            if (frames == null || frames.Length == 0) return;

            // Zmiana klatki na następną
            currentFrame++;
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0; // Zapętlenie (można tu też dodać flagę do usuwania obiektu)
            }
        }
    }
}