﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace box_line_crossover
{
    public partial class ChildForm : Form
    {
        float[] points;
        int type;
        double alfa;
        //Graphics g;

        // Border color
        Pen red = new Pen(Color.Red);
        Pen green = new Pen(Color.Green);
        Pen black = new Pen(Color.Black);
        Pen blue = new Pen(Color.Blue);


        // Fill
        System.Drawing.SolidBrush fillRed = new System.Drawing.SolidBrush(Color.Red);
        System.Drawing.SolidBrush fillGreen = new System.Drawing.SolidBrush(Color.Green);
        int isDrawn = 0;
       

        public ChildForm(float[] parents, int crossoverType, double alfaValue)
        {
            InitializeComponent();
            points = parents;
            type = crossoverType;
            alfa = alfaValue;
        }

        public void GeneralCrossoverForm(Graphics g)
        {
            Random random = new Random();
            double uMaxLimit = 2 * alfa + 1;
            CrossoverType t = GetCrossoverType(type);
            float childX = 0, childY = 0;

            if (t == CrossoverType.Line || t == CrossoverType.ExtendedLine)
            {

                g.DrawLine(black, points[0], points[1], points[2], points[3]);
                //g.DrawLine(black, points[0], 100 - points[1], points[2], 100 - points[3]);

                double u = random.NextDouble() * uMaxLimit;
                childX = (float)((points[0] - alfa) + u * (points[2] - points[0]));
                childY = (float)((points[1] - alfa) + u * (points[3] - points[1]));

                xLabel.Text = String.Format("{0:0.00}", childX);
                yLabel.Text = String.Format("{0:0.00}", childY);

                g.DrawRectangle(red, points[0], points[1], 2, 2);
                g.DrawRectangle(blue, points[2], points[3], 2, 2);
                g.DrawRectangle(green, childX, childY, 2, 2);

            }

            if (t == CrossoverType.Box || t == CrossoverType.ExtendedBox)
            {

                g.DrawRectangle(black, points[0], points[1], Math.Abs( points[2] - points[0]), Math.Abs(points[3] -( points[1])));

                double u1 = random.NextDouble() * uMaxLimit;
                double u2 = random.NextDouble() * uMaxLimit;
                while (u1 == u2)
                {
                    u2 = random.NextDouble() * uMaxLimit;
                }

                childX = (float)((points[0] - alfa) + u1 * (points[2] - points[0]));
                childY = (float)((points[1] - alfa) + u2 * (points[3] - points[1]));

                xLabel.Text = String.Format("{0:0.00}", childX);
                yLabel.Text = String.Format("{0:0.00}", childY);

                g.DrawRectangle(red, points[0], points[1], 2, 2);
                g.DrawRectangle(blue, points[2], points[3], 2, 2);
                g.DrawRectangle(green, childX, childY, 2, 2);
            }

        }

        public CrossoverType GetCrossoverType(int t) { 
            switch (t)
            {
                case (1):
                    return CrossoverType.Line;
                case (2):
                    return CrossoverType.ExtendedLine;
                case (3):
                    return CrossoverType.Box;
                case (4):
                    return CrossoverType.ExtendedBox;
            }

            return 0;
        }


        public enum CrossoverType
        {
            Line,
            ExtendedLine,
            Box,
            ExtendedBox
        }

        private void ChildForm_Paint(object sender, PaintEventArgs e)
        {
            if (isDrawn == 1)
            {
                GeneralCrossoverForm(e.Graphics);
            }
            isDrawn = 1;
            
        }
    }
}
