using System;
using System.IO;

namespace dechifr_client
{
    class LogFile
    {
        public static void writeToFile(string logMessage)
        {
            using (StreamWriter w = File.AppendText("logFileDAD.txt"))
            {
                Log(logMessage, w);
            }

            using (StreamReader r = File.OpenText("logFileDAD.txt"))
            {
                DumpLog(r);
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    
    }
}
