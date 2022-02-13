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
            this.MaximizeBox = false;
            LocalDataSync localDataSync = new LocalDataSync();
            /*
                textbox对应：
                server_ip: addr_textbox
                server_port: port_textbox
                network_name: textBox1
                network_secret: textBox2
                private_ip: textBox3
             */
            DataStruct ds = localDataSync.get_datastruct();
            addr_textbox.Text = ds.server_ip;
            port_textbox.Text = ds.server_port;
            textBox1.Text = ds.network_name;
            textBox2.Text = ds.network_secret;
            textBox3.Text = ds.private_ip;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("This is Done1!");
            if (button1.Text == "连接")
            {
                System.Console.WriteLine("This is Done2!");
                n2Nclient = new N2Nclient(addr_textbox.Text, port_textbox.Text
                    , textBox1.Text, textBox2.Text, textBox3.Text);
                
                if(n2Nclient.Connect_to_server())
                {
                    button2.Enabled = true;
                    button1.Text = "重启";
                }
            }
            else if(button1.Text is "重启")
            {
                n2Nclient.close_connection();
                System.Console.WriteLine("This is Done2!");
                n2Nclient = new N2Nclient(addr_textbox.Text, port_textbox.Text
                    , textBox1.Text, textBox2.Text, textBox3.Text);

                if (n2Nclient.Connect_to_server())
                {
                    button2.Enabled = true;
                    button1.Text = "重启";
                }
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

        private void addr_textbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
