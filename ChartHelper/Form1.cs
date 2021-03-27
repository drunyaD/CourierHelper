using BusinessLogic.Calculation;
using BusinessLogic.DataModel;
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

            /*var initialPoints = new List<FlatPoint>();

            for (int i = 0; i < 10; i++)
            {
                initialPoints.Add(new FlatPoint
                {
                    X = random.Next(1000),
                    Y = random.Next(1000)
                });
            }*/

            var initialPoints = new List<FlatPoint>()
            {
                new FlatPoint
                {
                    X = 300,
                    Y = 300
                },
                new FlatPoint
                {
                    X = 100,
                    Y = 100
                },
                new FlatPoint
                {
                    X = 550,
                    Y = 50
                },
                new FlatPoint
                {
                    X = 1000,
                    Y = 900
                },
                new FlatPoint
                {
                    X = 800,
                    Y = 700
                },
                new FlatPoint
                {
                    X = 250,
                    Y = 750
                },
                new FlatPoint
                {
                    X = 1200,
                    Y = 800
                },
                new FlatPoint
                {
                    X = 600,
                    Y = 600
                },
                new FlatPoint
                {
                    X = 444,
                    Y = 900
                }
            };

            var paintPoints = initialPoints.Select(p => new Point(p.X, p.Y)).ToList();

            var config = new AlgorithmConfigProvider().ProvideConfig(initialPoints.Count);
            var algo = new DuelistAlgorithm<FlatPoint>(initialPoints, config);
            var result = algo.Run();
            var points = result.Keys.First();

            for (int i = 0; i < points.Count-1; i++)
            {
                var firstPointName = "Point" + i.ToString();
                g.DrawString(firstPointName, fnt, Brushes.Blue, paintPoints[points[i]]);

                g.DrawLine(Pens.Red, paintPoints[points[i]], paintPoints[points[i+1]]);
            }

            var lastPointName = "Point" + (points.Count - 1).ToString();
            g.DrawString(lastPointName, fnt, Brushes.Blue, paintPoints[points.Last()]);
        }
    }
}
