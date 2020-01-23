using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Helpers
{
    public class Logger
    {

        public static void Log(string Message, bool sameline = false, bool nodate = false)
        {
            try
            {
                if (nodate == false) { Message = DateTime.Now + " --> " + Message; }

                string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\BDD_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
                if (!File.Exists(filepath))
                {
                    // Create a file to write to.   
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        if (!sameline) sw.WriteLine(Message); else sw.Write(Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        if (!sameline) sw.WriteLine(Message); else sw.Write(Message);
                    }
                }

                if (sameline) Console.Write(Message); else Console.WriteLine(Message);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
    }
}
