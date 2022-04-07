using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalayTree
{
	public partial class Form1 : Form
	{
		
		public Form1()
		{
			InitializeComponent();
		}
		private Graphics graphics;
		readonly double th1 = 30 * Math.PI / 180;
		readonly double th2 = 20 * Math.PI / 180;
		double per1 = 0.6;
		double per2 = 0.7;

		void drawCayleyTree(int n,double x0, double y0, double leng, double th)
		{
			if (n == 0) return;

			double x1 = x0 + leng * Math.Cos(th);
			double y1 = y0 + leng * Math.Sin(th);

			drawLine(x0, y0, x1, y1);

			drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
			drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);
		}
		
		void drawLine(double x0, double y0, double x1, double y1)
		{
			
			graphics.DrawLine(Pens.Red, (int)x0, (int)y0, (int)x1, (int)y1);
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			//My_Conbobox();
		}
		

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
			if (graphics==null)graphics= this.CreateGraphics();
			drawCayleyTree(16, 200, 310, 100, -Math.PI / 2);
		}

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
		}
		
		private int n1 = 1;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
			n1=int.Parse(textBox1.Text);
        }
		double leng1 = 0;
        //主干长度
		private void textBox2_TextChanged(object sender, EventArgs e)
        {
			leng1 = double.Parse(textBox2.Text);
        }
		//左分支长度比
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
			per1=double.Parse(textBox3.Text);
        }
		//右分支长度比
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
			per2 = double.Parse(textBox4.Text);
        }

		Pen pen = new Pen(Color.Red);
		private void button2_Click(object sender, EventArgs e)
        {
			
			colorDialog1.ShowDialog();
			Color c = colorDialog1.Color;
			pen.Color = c;
			
        }
    }
}
