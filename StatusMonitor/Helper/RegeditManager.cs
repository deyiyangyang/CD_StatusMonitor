using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.Helper
{
    public static class RegeditManager
    {
        private static Microsoft.Win32.RegistryKey Reg;
        static RegeditManager()
        {
            Reg = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\NGCPADAPTER");
            if (Reg == null)
            {
                Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\NGCPADAPTER");
            }
        }

        public static string GetValueByKey(string key)
        {
            try
            {
                if (Reg == null) return "";
                return Reg.GetValue("NAME", "").ToString();
            }
            catch (Exception ex)
            {
                LogManager.WriteLog("GetValueByKey SysteError:" + ex.Message + ex.StackTrace);
                return "";
            }
        }
    }
}
