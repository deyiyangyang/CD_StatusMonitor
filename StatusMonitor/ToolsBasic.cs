/////////////////////////////////////////////////////////////////////
// ToolsBasic.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MyTools
{
	static public partial class MyTool
	{
		// GetDateTime

		static public string GetDateTime()
		{
			// 00/00/00-00:00:00-000
			DateTime now = DateTime.Now;
			return String.Format("{0:yy/MM/dd-HH:mm:ss}-{1:d3}", now, now.Millisecond);
		}

		static public DateTime ParseDateTime(string time)
		{
			string[] formats = { "HH:mm:ss", "yy/MM/dd HH:mm:ss", "yyyy/MM/dd HH:mm:ss", "yy/MM/dd", "yyyy/MM/dd" };
			DateTime result;
			if(!DateTime.TryParseExact(time, formats, null,
				DateTimeStyles.AllowWhiteSpaces, out result))
			{
				return DateTime.Now;
			}
			return result;
		}
		
		// ModuleFile

		static public string GetModulePath()
		{
            
			return Application.ExecutablePath;          
            
            
		}

		static public string GetModuleName()
		{
			string path = GetModulePath();
			path = path.Substring(path.LastIndexOf('\\') + 1);
			path = path.Substring(0, path.LastIndexOf('.'));
			return path;
		}

		static public string GetModuleFolder()
		{
			string path = GetModulePath();
			path = path.Substring(0, path.LastIndexOf('\\'));
			return path;
		}

		static public string GetModulePathCutExt()
		{
			string path = GetModulePath();
			path = path.Substring(0, path.LastIndexOf('.'));
			return path;
		}

		static public string GetModuleIniPath()
		{
            //update,xzg,2012/04/17,S
			//return GetModulePathCutExt() + ".ini";
            string strPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            strPath = strPath + "\\Comdesign\\StatusMonitor.ini";
            return strPath;
            //update,xzg,2012/04/17,E
		}
	} // MyTool
} // namespace
