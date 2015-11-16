/////////////////////////////////////////////////////////////////////
// ToolsPrevApp.cs

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Diagnostics;

// SecurityPermission
using System.Security.Permissions;
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Flags = SecurityPermissionFlag.UnmanagedCode)]

namespace Comdesign
{
	//////////////////////////////////////////////////////////////////////
	// ToolsPrevApp

	internal static class ToolsPrevApp
	{
		//////////////////////////////////////////////////////////////////////
		// NativeMethod

		internal static class NativeMethod
		{
			//////////////////////////////////////////////////////////////////////
			// Constants

			// ShowWindow() Commands
			public const int SW_SHOWNORMAL	= 1;
			// GetWindow() Constants
			public const uint GW_HWNDNEXT	= 2;
			public const uint GW_CHILD		= 5;

			//////////////////////////////////////////////////////////////////////
			// Native method declaration

			// BOOL ShowWindow(HWND hWnd, int nCmdShow);
			[DllImport("user32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

			// HWND GetLastActivePopup(HWND hWnd);
			[DllImport("user32.dll", SetLastError = true)]
			internal static extern IntPtr GetLastActivePopup(IntPtr hWnd);

			// BOOL SetForegroundWindow(HWND hWnd);
			[DllImport("user32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool SetForegroundWindow(IntPtr hWnd);

			// HWND GetDesktopWindow(VOID);
			[DllImport("user32.dll", SetLastError = true)]
			internal static extern IntPtr GetDesktopWindow();

			// HWND GetWindow(HWND hWnd, UINT uCmd);
			[DllImport("user32.dll", SetLastError = true)]
			internal static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

			// BOOL SetProp(HWND hWnd, LPCTSTR lpString, HANDLE hData);
			[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool SetProp(IntPtr hWnd, string lpString, IntPtr hData);

			// HANDLE GetProp(HWND hWnd, LPCTSTR lpString);
			[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern IntPtr GetProp(IntPtr hWnd, string lpString);

			// HANDLE RemoveProp(HWND hWnd, LPCTSTR lpString);
			[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern IntPtr RemoveProp(IntPtr hWnd, string lpString);

			//////////////////////////////////////////////////////////////////////
			// End Class
		}

		//////////////////////////////////////////////////////////////////////
		// ToolsPrevApp

		public static void SetWindowPropRun(IntPtr hWnd, string appName)
		{
			// ウィンドウのプロパティにインスタンスタグを追加する
			string property = "PropRun," + appName;
			if(!NativeMethod.SetProp(hWnd, property, new IntPtr(1)))
				throw new Win32Exception();
		}

		public static IntPtr SearchWindowPropRun(string appName)
		{
			string property = "PropRun," + appName;            
			// アプリケーションのメインウィンドウを列挙する
			IntPtr hWnd = NativeMethod.GetWindow(NativeMethod.GetDesktopWindow(), NativeMethod.GW_CHILD);
			while(hWnd != IntPtr.Zero)                
			{ 
                // このウィンドウにインスタンスタグがあるか
                IntPtr hData = NativeMethod.GetProp(hWnd, property);
                if (hData != IntPtr.Zero) return hWnd;
                // 次のウィンドウに移る
                hWnd = NativeMethod.GetWindow(hWnd, NativeMethod.GW_HWNDNEXT);
			}
			return IntPtr.Zero;
		}

		public static bool PopupPrevApp(string appName)
		{
			// 前のアプリケーションのメインウィンドウを探す
			IntPtr hWnd = SearchWindowPropRun(appName);
			if(hWnd == IntPtr.Zero) return false;
			
			// ウィンドウを復元し、フォーカスを設定する
            //modified by Zhu 2014/04/09
            //NativeMethod.ShowWindow(hWnd, NativeMethod.SW_SHOWNORMAL);
            NativeMethod.ShowWindow(hWnd, 3);
            //end modified
			IntPtr hPopupWnd = NativeMethod.GetLastActivePopup(hWnd);
			if(hPopupWnd != IntPtr.Zero) hWnd = hPopupWnd;
			NativeMethod.SetForegroundWindow(hWnd);
			return true;
		}

		//////////////////////////////////////////////////////////////////////
		// End Class
	}
}

