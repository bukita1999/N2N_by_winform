using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N2Nwinform
{
    public partial class Form1 : Form
    {
        private int server = 0;
        public N2Nclient n2Nclient;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "代替有毒的小黄鸭";
            button1.Text = "连接";
            button2.Text = "断开";
            button2.Enabled = false;
            radioButton1.Text = "上海服务器";
            radioButton1.Checked = true;
            radioButton2.Text = "北京服务器";
            label1.Text = "输入一个0~255的数字,与他人不同";
            n2Nclient = new N2Nclient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "连接")
            {
                
                if (radioButton1.Checked is true)
                {
                    server = 0;
                }
                else
                {
                    server = 1;
                }
                
                n2Nclient.Connect_to_server(int.Parse(textBox1.Text), server);
                button2.Enabled = true;
                button1.Text = "重启";
            }
            else if(button1.Text is "重启")
            {
                n2Nclient.close_connection();
                n2Nclient.Connect_to_server(int.Parse(textBox1.Text), server);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n2Nclient.close_connection();
            button1.Text = "连接";
            button2.Enabled = false;
        }
    }
}
