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
        
        public N2Nclient n2Nclient;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            n2Nclient = new N2Nclient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "连接")
            {
                button2.Enabled = true;
                button1.Text = "重启";
            }
            else if(button1.Text is "重启")
            {
                n2Nclient.close_connection();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n2Nclient.close_connection();
            button1.Text = "连接";
            button2.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
