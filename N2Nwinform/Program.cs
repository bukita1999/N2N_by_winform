using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace N2Nwinform
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public class N2Nclient
    {
        //private string SPnum;
        //sudo edge -c mynetwork -k mysecretpass -a 192.168.100.1 -f -l supernode.ntop.org:7777 -r
        private string server_ip; 
        private string server_port;
        private string network_name; //网络名，相当于mynetwork
        private string network_secret; //网络密码，相当于mysecretpass
        private string private_ip; //网络ip，在同一个网络通过此ip来通讯，一般是192.168.55.x
        string exefile = @".\edge_v2.exe";
        Process process;
        public void Connect_to_server(int specialNum,int server)
        {
            string full_cmd = String.Format(".\edge_v2.exe -c {0} -k {1} -a {2} -f -l {3}:{4} -r"
                ,network_name,network_secret,private_ip,server_ip,server_port);
            process = new Process();
            /*if (File.Exists(exefile))
            {
                // params 为 string 类型的参数，多个参数以空格分隔，如果某个参数为空，可以传入””
                Console.WriteLine("存在！");
                ProcessStartInfo startInfo = new ProcessStartInfo(exefile, full_cmd);
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
            }
            else
                Application.Exit();
            */
            try
            {
                if(!File.Exists(exefile))
                {
                    throw FileNotFoundException(exefile);
                }
                ProcessStartInfo startInfo = new ProcessStartInfo(exefile,full_cmd);
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                process.Start();
            }
            catch (FileNotFoundException file)
            {

            }
        }
        public void close_connection()
        {
            process.Kill();
        }
    }
}
