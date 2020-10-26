using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Okta_Web.Helpers
{
    public static class LogWriter
    {
        private static string ConfigFilePath
        {
            get { return AppDomain.CurrentDomain.DynamicDirectory + "ErrorLogFiles" + Path.DirectorySeparatorChar; }
        }

        public static async Task WriteLogFile(string message, Exception ex)
        {
            StreamWriter log;
            FileInfo logFileInfo;
            string logFilePath = ConfigFilePath + string.Format(CultureInfo.InvariantCulture, "Log-{0}.txt", DateTime.Today.ToString("MM-dd-yyyy"));
            logFileInfo = new FileInfo(logFilePath);
            DirectoryInfo logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            FileStream fileStream;
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            try
            {

                log = new StreamWriter(fileStream);
                await log.WriteLineAsync("\n\r");
                await log.WriteLineAsync(message);
                await log.WriteLineAsync("\n\r");
                if (ex == null)
                {
                    await log.WriteLineAsync($"Log created on:" + DateTime.UtcNow.ToString());
                    await log.WriteLineAsync("\n\r");
                    await log.WriteLineAsync("Not an exception. Its just a log.");
                }
                else
                {
                    await log.WriteLineAsync("Error occured on:" + DateTime.UtcNow.ToString());
                    await log.WriteLineAsync("\n\r");
                    await log.WriteLineAsync(JsonConvert.SerializeObject(ex));
                }
                log.Close();
            }
            catch
            {
                //log = new StreamWriter(fileStream);
                //log.WriteLine("WriteLogFile: " + JsonConvert.SerializeObject(e));
            }
        }
    }
}
