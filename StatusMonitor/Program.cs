using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Comdesign;
using StatusMonitor.Helper;
namespace StatusMonitor
{
    static class Program
    {
		/// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			
			// ２重起動の防止
            //add,xzg,2009/10/07,S--------
            //if (ToolsPrevApp.PopupPrevApp(MyTools.MyTool.GetModuleName())) return;
            //add,xzg,2009/10/07,E--------

            System.Threading.Mutex appMutex = new System.Threading.Mutex(false, Application.ProductName);
            if (!appMutex.WaitOne(0, false))
            {
                //LanguageResourceManager res = new LanguageResourceManager("JP");
                //MessageBox.Show(res.GetString("SM9990003"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                appMutex.Close();

                ToolsPrevApp.PopupPrevApp(Application.ProductName);

                return;
            }
            try { 
			
			// Run
			Application.Run(new MainForm());

			// Release appMutex
			appMutex.ReleaseMutex();
			appMutex.Close();
                }
            catch(Exception ex)
            {
                LogManager.WriteLog("Main system error:"+ ex.Message);
            }
		}
       
    }
}
