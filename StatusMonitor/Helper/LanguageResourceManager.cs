using System;
using System.Collections.Generic;
using System.Text;

namespace StatusMonitor.Helper
{
    class LanguageResourceManager
    {
        private System.Resources.ResourceManager m_res;

        public LanguageResourceManager(string language)
        {

            if (System.Configuration.ConfigurationManager.AppSettings.HasKeys())
            {
                language = System.Configuration.ConfigurationManager.AppSettings["Language"].ToString();
            }
            else
            {
                language = "JP";
            }
            string lan = "LAN_SM_" + language;
            m_res = System.Resources.ResourceManager.CreateFileBasedResourceManager(lan, ".", null);

        }
        public string GetString(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(m_res.GetString(name)))
                    return "";
                else
                    return (m_res.GetString(name));
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
