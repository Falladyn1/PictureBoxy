using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PictureBoxy
{
    public partial class Form1 : Form
    {
        //List<Circle> balls = new List<Circle>();
        //List<Square> boxes = new List<Square>();

        //IFigure[] figures = new IFigure[101];
        List<IFigure> figures = new List<IFigure>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();

            Bitmap[] explosionFrames = PrepareAnimationFrames("wybuch.jpg");

            for (int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    figures.Add(new Circle(rnd.Next(0, 300), rnd.Next(0, 300), rnd.Next(20, 50), rnd.Next(1, 5), rnd.Next(1, 5)));
                }
                else
                {
                    figures.Add(new Square(rnd.Next(0, 300), rnd.Next(0, 300), rnd.Next(20, 50), rnd.Next(1, 5), rnd.Next(1, 5)));
                }
                //balls.Add(new Circle(rnd.Next(0, 300), rnd.Next(0, 300), rnd.Next(20, 50), rnd.Next(1, 5), rnd.Next(1, 5)));
                //boxes.Add(new Square(rnd.Next(0, 300), rnd.Next(0, 300), rnd.Next(20, 50), rnd.Next(1, 5), rnd.Next(1, 5)));
            }


            figures.Add(new Explosion(explosionFrames, 150, 150));

            timer1.Start();
        }

        private Bitmap[] PrepareAnimationFrames(string filepath)
        {
            Bitmap[] frames = new Bitmap[8];
            Bitmap spriteSheet = new Bitmap(Image.FromFile(filepath));

            int frameWidth = spriteSheet.Width / 4;
            int frameHeight = spriteSheet.Height / 2;
            int frameIndex = 0;

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Rectangle cropArea = new Rectangle(col * frameWidth, row * frameHeight, frameWidth, frameHeight);
                    frames[frameIndex] = spriteSheet.Clone(cropArea, spriteSheet.PixelFormat);
                    frameIndex++;
                }
            }

            spriteSheet.Dispose();
            return frames;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Image img = Image.FromFile("wybuch.jpg");

            //Rectangle cropArea = new Rectangle(50, 30, 200, 100);

            //Bitmap bmpImage = new Bitmap(img);
            //Bitmap cropped = bmpImage.Clone(cropArea, bmpImage.PixelFormat);

            foreach (IFigure f in figures)
            {
                f?.Draw(g);
            }

            //foreach (Circle c in balls) c.Draw(e.Graphics);
            //foreach (Square s in boxes) s.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (IFigure f in figures)
            {
                f?.Update(pictureBox1.Width, pictureBox1.Height);
            }

            //foreach (Circle c in balls) c.Update(pictureBox1.Width, pictureBox1.Height);
            //foreach (Square s in boxes) s.Update(pictureBox1.Width, pictureBox1.Height);

            pictureBox1.Refresh();
        }
    }
}