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

            // Dodanie DoubleBuffered zapobiega migotaniu PictureBox
            this.DoubleBuffered = true;

            // Upewnij siê, ¿e plik wybuch.jpg jest w folderze z plikiem .exe
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

            // W Form1.cs zamiast starej logiki:
            figures.Add(new Explosion("wybuch.png", 150, 150));

            timer1.Start();
        }

        private Bitmap[] PrepareAnimationFrames(string filepath, int rows = 2, int cols = 4)
        {
            try
            {
                if (!System.IO.File.Exists(filepath))
                {
                    throw new System.IO.FileNotFoundException("Nie znaleziono pliku graficznego.");
                }

                Bitmap[] frames = new Bitmap[rows * cols];

                // Wczytujemy orygina³ i tworzymy kopiê, aby nie blokowaæ pliku na dysku
                using (Bitmap spriteSheet = new Bitmap(filepath))
                {
                    int frameWidth = spriteSheet.Width / cols;
                    int frameHeight = spriteSheet.Height / rows;
                    int frameIndex = 0;

                    for (int row = 0; row < rows; row++)
                    {
                        for (int col = 0; col < cols; col++)
                        {
                            Rectangle cropArea = new Rectangle(col * frameWidth, row * frameHeight, frameWidth, frameHeight);

                            // Klonujemy fragment arkusza. 
                            // Format32bppPArgb jest najszybszy do renderowania w GDI+
                            frames[frameIndex] = spriteSheet.Clone(cropArea, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                            frameIndex++;
                        }
                    }
                }
                return frames;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"B³¹d podczas przygotowywania animacji: {ex.Message}");
                return new Bitmap[0];
            }
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