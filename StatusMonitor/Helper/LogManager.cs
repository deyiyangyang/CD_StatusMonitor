using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.Helper
{
    public static class LogManager
    {
        public static void WriteLog(string msg)
        {
            try
            {
                Microsoft.VisualBasic.Logging.Log log = new Microsoft.VisualBasic.Logging.Log();
                log.DefaultFileLogWriter.Location = Microsoft.VisualBasic.Logging.LogFileLocation.Custom;
                string strFile = "";
                strFile = log.DefaultFileLogWriter.CustomLocation + "\\" + "StatusMonitor" + DateTime.Now.DayOfWeek + ".log";
                if (System.IO.File.Exists(strFile))
                {

                    if (System.IO.File.GetLastWriteTime(strFile).ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
                    {

                    }
                    else
                    {
                        System.IO.File.Delete(strFile);

                    }
                }

                log.DefaultFileLogWriter.MaxFileSize = 50000000;
                log.DefaultFileLogWriter.BaseFileName = "StatusMonitor" + DateTime.Now.DayOfWeek;

                string strMsg = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                strMsg = strMsg + "    " + msg;
                log.DefaultFileLogWriter.WriteLine(strMsg);
                log.DefaultFileLogWriter.Flush();
                log.DefaultFileLogWriter.Close();

            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
