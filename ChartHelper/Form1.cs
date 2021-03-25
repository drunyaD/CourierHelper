using BusinessLogic.Calculation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartHelper
{
    public partial class Form1 : Form
    {
        private PictureBox pictureBox1 = new PictureBox();
        private Font fnt = new Font("Arial", 10);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(pictureBox1);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            var random = new Random();

            var initialPoints = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                initialPoints.Add(new Point
                {
                    X = random.Next(1000),
                    Y = random.Next(1000)
                });
            }

            var algo = new DuelistAlgorithm(initialPoints);
            var result = algo.Run();
            var points = result.Keys.First();

            for (int i = 0; i < points.Count-1; i++)
            {
                var firstPointName = "Point" + i.ToString();
                g.DrawString(firstPointName, fnt, Brushes.Blue, initialPoints[points[i]]);

                g.DrawLine(Pens.Red, initialPoints[points[i]], initialPoints[points[i+1]]);
            }

            var lastPointName = "Point" + (points.Count - 1).ToString();
            g.DrawString(lastPointName, fnt, Brushes.Blue, initialPoints[points.Last()]);
        }
    }
}
