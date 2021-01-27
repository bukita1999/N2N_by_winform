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
        private string[] server_ip = { "aliyun.yanduduandchuangdudu.club:10086","yanyan.yanduduandchuangdudu.club:10086" };
        
        private const string basic_cmd_str1 = "edge.exe -c caonetwork -k mysecretpass -a 192.168.187.";
        private const string basic_cmd_str2 = " -l ";
        private const string basic_cmd_str3 = " -r";
        string exefile = @".\edge_v2.exe";
        Process process;
        public void Connect_to_server(int specialNum,int server)
        {
            string full_cmd = basic_cmd_str1 + specialNum.ToString() + basic_cmd_str2 + server_ip[server].ToString() + basic_cmd_str3;
            process = new Process();
            if (File.Exists(exefile))
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
            
        }
        public void close_connection()
        {
            process.Kill();
        }
    }
}
