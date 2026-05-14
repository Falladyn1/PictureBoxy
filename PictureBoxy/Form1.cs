using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

namespace PictureBoxy
{
    public partial class Form1 : Form
    {
        List<IFigure> figures = new List<IFigure>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

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
            }

            this.pictureBox1.MouseClick += PictureBox1_MouseClick;

            timer1.Start();

            var options = new JsonSerializerOptions
            {
                IncludeFields = true,
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(figures, options);
            Console.WriteLine(json);
            List<IFigure> figures2 = JsonSerializer.Deserialize<List<IFigure>>(json);
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            figures.Add(new Explosion("wybuch.png", e.X, e.Y, 2, 4));
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (IFigure f in figures)
            {
                f?.Draw(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (IFigure f in figures)
            {
                f?.Update(pictureBox1.Width, pictureBox1.Height);
            }

            figures.RemoveAll(f => f.IsFinished);

            pictureBox1.Refresh();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

        }
    }
}