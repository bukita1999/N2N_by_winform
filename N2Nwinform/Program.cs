using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


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
        private DataStruct dataStruct;
        private bool isConnected = false;
        string exefile = @".\edge_v2.exe";
        Process process;
        public N2Nclient(string server_ip, string server_port,
            string network_name, string network_secret, string private_ip)
        {
            dataStruct = new DataStruct(server_ip, server_port, network_name, network_secret, private_ip);
        }
        public bool Connect_to_server()
        {
            isConnected = true;
            string full_cmd = String.Format(" -c {0} -k {1} -a {2} -l {3}:{4} -r"
                , dataStruct.network_name, dataStruct.network_secret, dataStruct.private_ip, dataStruct.server_ip, dataStruct.server_port);
            Console.WriteLine("full_cmd is " + full_cmd);
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
                process = new Process();
                if (!File.Exists(exefile))
                {
                    throw new FileNotFoundException(exefile);
                }
                ProcessStartInfo startInfo = new ProcessStartInfo(exefile, full_cmd);
                //startInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
                startInfo.Verb = "runas";
                process.StartInfo = startInfo;
                
                
                process.Start();
                return true;
            }
            catch (FileNotFoundException file)
            {
                System.Console.WriteLine("No File Exists!");
                return false;
            }
        }
        public void close_connection()
        {
            try
            {
                process.Kill();
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Already Exit!");
            }
        }
    }
    public class LocalDataSync
    {
        private string jsonFile_name = "config.json";
        private string jsonFile_addr;
        private string json_string;
        private DataStruct datastruct;
        public LocalDataSync()
        {
            jsonFile_addr = Application.StartupPath + "//" + jsonFile_name;
            if (!System.IO.File.Exists(jsonFile_addr))
            {
                datastruct = new DataStruct("aliyun.yan2sleep.me", "10086", "bugxia", "123456", "192.168.55.101");
                using (System.IO.StreamWriter outputFile = new StreamWriter(jsonFile_addr))
                {
                    json_string = JsonConvert.SerializeObject(datastruct, Formatting.Indented);
                    System.Console.WriteLine("Write json_string:" + json_string);
                    outputFile.WriteLine(json_string);
                    outputFile.Close();
                }
            }
            else
            {
                using (System.IO.StreamReader inputFile = new StreamReader(jsonFile_addr))
                {
                    json_string = inputFile.ReadToEnd();
                    System.Console.WriteLine("Read json_string:" + json_string);
                    datastruct = JsonConvert.DeserializeObject<DataStruct>(json_string);
                    inputFile.Close();

                }
            }

        }
        public DataStruct get_datastruct()
        {
            return this.datastruct;
        }
        public int sync(DataStruct dataStruct)
        {
            try
            {
                using (System.IO.StreamWriter outputFile = new StreamWriter(jsonFile_addr))
                {
                    json_string = JsonConvert.SerializeObject(dataStruct, Formatting.Indented);
                    System.Console.WriteLine("Write json_string:" + json_string);
                    outputFile.WriteLine(json_string);
                    outputFile.Close();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
            
        }
    }
    [Serializable]
    public class DataStruct
    {
        //sudo edge -c mynetwork -k mysecretpass -a 192.168.100.1 -f -l supernode.ntop.org:7777 -r
        public string server_ip { get; set; }
        public string server_port { get; set; }
        public string network_name { get; set; } //网络名，相当于mynetwork
        public string network_secret { get; set; } //网络密码，相当于mysecretpass
        public string private_ip { get; set; } //网络ip，在同一个网络通过此ip来通讯，一般是192.168.55.x
        public DataStruct(string server_ip, string server_port, 
            string network_name, string network_secret, string private_ip)
        {
            this.server_ip = server_ip;
            this.server_port = server_port;
            this.network_name = network_name;
            this.network_secret = network_secret; 
            this.private_ip = private_ip;
        }
    }
}
