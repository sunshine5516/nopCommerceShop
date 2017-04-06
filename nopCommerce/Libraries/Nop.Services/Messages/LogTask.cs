using Nop.Services.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Messages
{
    public class LogTask : ITask
    {
        private static string filePath = @"D:\Test\log.txt";
        public void Execute()
        {
            string content = " WriteLog：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";

            FileInfo fs = new FileInfo(filePath);
            StreamWriter sw = fs.AppendText();
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
