using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int a, b, result = 0;
        bool c = false;
        string d;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "1";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "2";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "5";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
            }
            textBox1.Text += "3";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "4";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "6";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "7";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "8";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "9";
        }

        private void button2_Click(object sender, EventArgs e)
        {          
                b = int.Parse(textBox1.Text);
                d = "+";
            textBox1.Text = " ";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (c == true)
            {
                c = false;
            }
            textBox1.Text += "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            b = int.Parse(textBox1.Text);
            d = "-";
            textBox1.Text = " ";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            b = int.Parse(textBox1.Text);
            d = "*";
            textBox1.Text = " ";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            a = int.Parse(textBox1.Text);
            switch (d)
            {
             case "+": result = a + b;   break;     
　　　　　   case "-": result = a - b;   break;    
　　　　     case "*": result = a * b;   break;        
　　　　     case "/": result = a / b;   break;
        }
            textBox1.Text = result.ToString();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = " ";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            b = int.Parse(textBox1.Text);
            d = "/";
            textBox1.Text = " ";
        }
    }
}
