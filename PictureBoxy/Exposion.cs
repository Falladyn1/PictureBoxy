using System.Drawing;

namespace PictureBoxy
{
    public class Explosion : IFigure
    {
        private Bitmap[] frames;
        private int currentFrame = 0;
        private int x, y;

        public Explosion(Bitmap[] animationFrames, int x, int y)
        {
            this.frames = animationFrames;
            this.x = x;
            this.y = y;
        }

        public void Draw(Graphics g)
        {
            if (frames != null && frames.Length > 0 && currentFrame < frames.Length)
            {
                g.DrawImage(frames[currentFrame], x, y);
            }
        }

        public void Update(int width, int height)
        {
            if (frames == null || frames.Length == 0) return;

            currentFrame++;
            
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }
        }
    }
}