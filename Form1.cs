// Steve Ratering
// 2020/5/26
// Snowflake Curve Birthday Cards for Katie and Lily

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowflake
{
    public partial class Form1 : Form
    {
        Pen myPen = new Pen(Color.FromName("Blue"));
        Graphics g;
        static int width, height, level;
        static double length;
        static String shape;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(800,800);
            shape = "Snow";
        }

        private void DrawSnow(double x, double y, int direction, double length, int level, Boolean incr)
        {
            double x1, y1;
            if (level == 0) {
                x1 = x + length * Math.Cos(direction * Math.PI / 6);
                y1 = y - length * Math.Sin(direction * Math.PI / 6);
                g.DrawLine(myPen, (int)x, (int)y, (int)(x1), (int)(y1));
            }
            else
            {
                double newLen = length / Math.Sqrt(3);
                if (incr)
                {
                    DrawSnow(x, y, (direction + 1) % 12, newLen, level - 1, false);
                    x1 = x + newLen * Math.Cos((direction + 1) * Math.PI / 6);
                    y1 = y - newLen * Math.Sin((direction + 1) * Math.PI / 6);
                    DrawSnow(x1, y1, (direction + 11) % 12, newLen, level - 1, false);
                }
                else
                {
                    DrawSnow(x, y, (direction + 11) % 12, newLen, level - 1, true);
                    x1 = x + newLen * Math.Cos((direction + 11) * Math.PI / 6);
                    y1 = y - newLen* Math.Sin((direction + 11) * Math.PI / 6);
                    DrawSnow(x1, y1, (direction + 1) % 12, newLen, level - 1, true);
                }
            }
        }

        private void DrawO(double x, double y, int direction, double length, int level)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Boolean forLily = true;
            Font font;
            float x, y, xBd, yBd;
            float thick = 1;
            width = canvas.Width;
            height = canvas.Height;
            length = Math.Min((width - 7 * thick) / 3, (height - 7 * thick) / 2 / Math.Sqrt(3));
            level = 12;
            myPen = new Pen(Color.FromName("Blue"), thick);
            g = canvas.CreateGraphics();
            double centerX, centerY, x1, y1, x2, y2, x3, y3;
            centerX = canvas.Width / 2;
            centerY = canvas.Height / 2;
            g.Clear(Color.White);
            x1 = centerX - length * 1.5;
            y1 = centerY + length * Math.Sqrt(3) / 2;
            x2 = centerX;
            y2 = centerY - length * Math.Sqrt(3);
            x3 = centerX + length * 1.5;
            DrawSnow(x1, y1, 2, length * 3, level, true);
            DrawSnow(x2, y2, 10, length * 3, level, true);
            DrawSnow(x3, y1, 6, length * 3, level, true);

            x1 = centerX - length;
            y1 = centerY;
            x2 = centerX + length / 2;
            y2 = centerY - length * Math.Sqrt(3) / 2;
            y3 = centerY + length * Math.Sqrt(3) / 2;
            DrawSnow(x1, y1, 1, length * Math.Sqrt(3), level - 1, true);
            DrawSnow(x2, y2, 9, length * Math.Sqrt(3), level - 1, true);
            DrawSnow(x2, y3, 5, length * Math.Sqrt(3), level - 1, true);

            if (forLily)
            {
                x1 = centerX - length / 2;
                y1 = centerY + length * Math.Sqrt(3) / 6;
                x2 = centerX;
                y2 = centerY - length / Math.Sqrt(3);
                x3 = centerX + length / 2;
                DrawSnow(x1, y1, 2, length, level - 2, true);
                DrawSnow(x2, y2, 10, length, level - 2, true);
                DrawSnow(x3, y1, 6, length, level - 2, true);
            }

            if (forLily)
            {
                font = new Font("Times New Roman", 19.0f);
                x = (float)(centerX - length * 0.42);
                y = (float)(centerY - length * 0.25);
            }
            else 
            {
                font = new Font("Times New Roman", 24.0f);
                x = (float)(centerX - length * 0.5);
                y = (float)(centerY - length * 0.1);
            }
            Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Purple);
            String txt = "Jesus loves you!";
            String txtBday = "2020.6.30";
            g.DrawString(txt, font, brush, x, y);

            if (forLily)
            {
                font = new Font("Times New Roman", 14.0f);
                x = (float)(centerX - length * 0.18);
                y = (float)(centerY + length * 0.05);
            }
            else 
            {
                font = new Font("Times New Roman", 16.0f);
                x = (float)(centerX - length * 0.2);
                y = (float)(centerY + length * 0.15);
            }

            brush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            txt = "I do too!";
            g.DrawString(txt, font, brush, x, y);

            font = new Font("Times New Roman", 14.0f);
            brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            Brush brushGr = new System.Drawing.SolidBrush(System.Drawing.Color.Green);
            x = (float)(centerX - length * (forLily ? 0.4 : 0.43));
            y = (float)(centerY - length * 1.4);
            xBd = (float)(centerX - length *  0.2);
            yBd = (float)(centerY - length * 1.05);
            if (forLily){
                txt = "Happy birthday Lily!";
            } else {
                txt = "Happy birthday Katie!";
            }
            g.DrawString(txt, font, brush, x, y);
            if (forLily)
            {
                g.DrawString(txtBday, font, brushGr, xBd, yBd);
            }

            float w = (float)centerX;
            float h = (float)centerY;
            for (int i = 0; i < 5; i++)
            {
                g.TranslateTransform(w, h);
                g.RotateTransform(60);
                g.TranslateTransform(-w, -h);
                g.DrawString(txt, font, brush, x, y);
                if (forLily)
                {
                    g.DrawString(txtBday, font, brushGr, xBd, yBd);
                }
            }
        }

        private void myLevel_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void color_TextChanged(object sender, EventArgs e)
        {
        }

        private void myLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void myLevel_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void myShape_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void myShape_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void myShape_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void myShapeO_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void myNested_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
