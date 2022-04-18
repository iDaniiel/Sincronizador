using System;
using System.IO;

namespace gdtPublisher.Utils
{
    public static class Bitacora
    {
        public static void Write(string message)
        {
            string date = DateTime.Now.ToString("yyyyMMdd");
            string logTran = string.Format(Properties.Settings.Default.pathLogsBitacora + "gdtPublisher_{0}_{1}.log", Environment.MachineName, date);
            string text = string.Format("{0} | {1}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);

            File.AppendAllText(logTran, text);
        }
    }
}
