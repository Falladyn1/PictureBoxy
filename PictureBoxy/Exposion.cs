using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace PictureBoxy
{
    public class Explosion : IFigure
    {
        private Bitmap[] frames;
        private int currentFrame = 0;
        private int x, y;

        private int frameDelayCounter = 0;
        private readonly int ticksPerFrame = 5; 

        public bool IsFinished { get; private set; } = false;

        public Explosion(string filepath, int x, int y, int rows , int cols)
        {
            this.x = x;
            this.y = y;
            this.frames = LoadFrames(filepath, rows, cols);
        }

        private Bitmap[] LoadFrames(string filepath, int rows, int cols)
        {
            try
            {
                // Wczytujemy oryginał
                using (Bitmap spriteSheet = new Bitmap(filepath))
                {
                    Bitmap[] animationFrames = new Bitmap[rows * cols];
                    int frameWidth = spriteSheet.Width / cols;
                    int frameHeight = spriteSheet.Height / rows;
                    int index = 0;

                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            Rectangle cropArea = new Rectangle(col * frameWidth, row * frameHeight, frameWidth, frameHeight);

                            // Klonujemy fragment
                            Bitmap frame = spriteSheet.Clone(cropArea, PixelFormat.Format32bppPArgb);

                            // WYMUSZENIE PRZEZROCZYSTOŚCI: 
                            // Jeśli tło jest idealnie białe, ta linia je usunie:
                            frame.MakeTransparent(Color.White);

                            animationFrames[index] = frame;
                            index++;
                        }
                    }
                    return animationFrames;
                }
            }
            catch { return new Bitmap[0]; }
        }

        public void Draw(Graphics g)
        {
            if (frames != null && currentFrame < frames.Length)
            {
                Bitmap img = frames[currentFrame];

                float centerX = x - (img.Width / 2f);
                float centerY = y - (img.Height / 2f);

                g.DrawImage(img, centerX, centerY);
            }
        }

        public void Update(int width, int height)
        {
            if (frames == null || IsFinished) return;

            frameDelayCounter++;

            if (frameDelayCounter >= ticksPerFrame)
            {
                frameDelayCounter = 0;
                currentFrame++;

                if (currentFrame >= frames.Length)
                {
                    IsFinished = true;
                }
            }
        }
    }
}