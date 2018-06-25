using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace CheckProxy
{
    class Program
    {
        public static string pathProxy = "proxy.txt";
        private static string path = "in.txt";
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "CheckProxy By MisterNT : /*/_______/*/ -----> ->>>>>>>>>>>>>>>>> V.1.0.0.1";
            OpenFileDialog b = new OpenFileDialog();
            b.Title = "OPEN INPUT FILE Proxy ";
            b.Filter = "INPUT FILE Proxy (.txt)|*.txt";
            if (b.ShowDialog() == DialogResult.OK)
            {

                path = b.FileName;
                var list = new List<string>();


                var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("ADD : Proxy {0}", line);
                        chkProxy(line.ToString().Trim());
                    }
                }
            }
            else {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File NOT FOUD");
            }
            Console.ReadKey();
        }
        private static void chkProxy(string proxy) {
            try {
                using (var wb = new WebClient())
                {
                    WebProxy temp = new WebProxy(proxy);
                    wb.Proxy = temp;
                    byte[] byteArray2 = wb.DownloadData("https://www.facebook.com/");

                    Console.ForegroundColor = ConsoleColor.Green;

                    using (StreamWriter sw = File.AppendText(pathProxy))
                    {
                        sw.WriteLine(proxy);
                        sw.Close();
                    }
                    Console.WriteLine("ProxySuccs : " + (proxy));

                }
            } catch {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ProxyError : " + (proxy));
            }
        }
    }
}
