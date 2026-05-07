using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PictureBoxy
{
    public partial class Form1 : Form
    {
        List<Circle> balls = new List<Circle>();
        List<Square> boxes = new List<Square>();
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 50; i++)
            {
                balls.Add(new Circle(rnd.Next(0, 300), rnd.Next(0, 300), rnd.Next(20, 50), rnd.Next(1, 5), rnd.Next(1, 5)));
                boxes.Add(new Square(rnd.Next(0, 300), rnd.Next(0, 300), rnd.Next(20, 50), rnd.Next(1, 5), rnd.Next(1, 5)));
            }
            timer1.Start();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Circle c in balls) c.Draw(e.Graphics);
            foreach (Square s in boxes) s.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Circle c in balls) c.Update(pictureBox1.Width, pictureBox1.Height);
            foreach (Square s in boxes) s.Update(pictureBox1.Width, pictureBox1.Height);

            pictureBox1.Refresh();
        }
    }
}