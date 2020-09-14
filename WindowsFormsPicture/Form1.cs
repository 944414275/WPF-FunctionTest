using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsPicture
{
    public partial class Form1 : Form
    {
        byte[] vs = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public Form1()
        {
            InitializeComponent();
            CreateBmp1();
        }

        void CreateBmp1()
        { 
            for (int i = 0; i < 3; i++)
            {
                Bitmap bmp = new Bitmap(700, 260);
                Graphics g = Graphics.FromImage(bmp);

                //BitmapImage image = new BitmapImage();

                System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.White, 1);
                System.Drawing.Pen dashPen = new System.Drawing.Pen(System.Drawing.Color.Gray, 1);
                dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                for (int j = 0; j < 5; j++)
                {
                    g.DrawLine(dashPen, 15.0f, j * 45 + 5.0f, 451.92f + 15f, j * 45 + 5.0f);
                    g.DrawLine(dashPen, (j + 1) * 90.3846f + 15f, 0 + 5f, (j + 1) * 90.3846f + 15, 225 + 5f);
                }
                this.pictureBox1.Image = bmp;
                Thread.Sleep(2000);
                //image = BitmapToBitmapImage(bmp);
                //bitmapImages.Add(image);
            }
        }

         
    }
}
