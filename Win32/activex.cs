using System;
using System.Runtime.InteropServices;
namespace AZLib.Win32 {

	public class  ActiveX {
		public const string Ole32Dll   = "ole32.dll";
		public const string OleAut32Dll   = "oleaut32.dll";
		public const string OlePro32Dll   = "olepro32.dll";

		//function FindWindowW(lpClassName, lpWindowName: PWideChar): HWND; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="FindWindowW")]
		[return:MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]
		private static extern  HWND _FindWindow([MarshalAs(UnmanagedType.LPWStr)]string ClassName, [MarshalAs(UnmanagedType.LPWStr)]string WindowName);
		public static HWND FindWindow(string ClassName, string WindowName) {
			if (ClassName == String.Empty){
				ClassName = null;
			}
			if (WindowName == String.Empty){
				WindowName = null;
			}
			return _FindWindow(ClassName,WindowName);
		}


		//function FindWindowExW(Parent, Child: HWND; ClassName, WindowName: PWideChar): HWND; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="FindWindowExW")]
		[return:MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]
		private static extern HWND _FindWindowEx([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Parent,
			[MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Child, 
			[MarshalAs(UnmanagedType.LPWStr)]string ClassName, [MarshalAs(UnmanagedType.LPWStr)]string WindowName);
		public static HWND FindWindowEx(HWND Parent, HWND Child, string ClassName, string WindowName) {
			if (ClassName == String.Empty){
				ClassName = null;
			}
			if (WindowName == String.Empty){
				WindowName = null;
			}
			return _FindWindowEx(Parent, Child, ClassName,WindowName);
		}

		
		//function GetWindow(hWnd: HWND; uCmd: UINT): HWND; stdcall;

		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="GetWindow")]
		[return:MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]
		private static extern HWND _GetWindow([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd,
			[MarshalAs(UnmanagedType.U4)] GetWindowType uCmd);
		public static HWND GetWindow(HWND Wnd,GetWindowType uCmd) {
			return _GetWindow(Wnd,uCmd);
		}


		//function GetWindowTextW(hWnd: HWND; lpString: PWideChar; nMaxCount: Integer): Integer; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="GetWindowTextW")]
		private static extern IntPtr _GetWindowText([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd,
			[MarshalAs(UnmanagedType.LPWStr),Out]string lpString,int nMaxCount);
		public static string GetWindowText(HWND Wnd) {
			int length = GetWindowTextLength(Wnd);
			char[] chars = new char[length];
			string res = new String(chars);
			_GetWindowText(Wnd,res,length+1);
			return res;
		}

		//function GetWindowTextLengthW(hWnd: HWND): Integer; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="GetWindowTextLengthW")]
		private static extern IntPtr _GetWindowTextLength([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd);
		public static int GetWindowTextLength(HWND Wnd) {
			IntPtr len = _GetWindowTextLength(Wnd);
			return (int) len;
		}

		//function GetWindowRect(hWnd: HWND; var lpRect: TRect): BOOL; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="GetWindowRect")]
		private static extern IntPtr _GetWindowRect([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd,
		[Out] RECT Rect);
		public static bool GetWindowRect(HWND Wnd, RECT Rect) {
			IntPtr p = _GetWindowRect(Wnd, Rect);
			return (p != IntPtr.Zero);
		}

		//function ShowWindow(hWnd: HWND; nCmdShow: Integer): BOOL; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="ShowWindow")]
		private static extern IntPtr _ShowWindow([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd,
			[MarshalAs(UnmanagedType.I4)] ShowWindowCommands nCmdShow);
		public static bool ShowWindow(HWND Wnd, ShowWindowCommands nCmdShow) {
			IntPtr p = _ShowWindow(Wnd, nCmdShow);
			return (p != IntPtr.Zero);
		}



		//function SendMessageW(hWnd: HWND; Msg: UINT; wParam: WPARAM; lParam: LPARAM): LRESULT; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="SendMessageW")]
		public static extern IntPtr SendMessage([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd, 
			UInt32 Msg, IntPtr WParam, IntPtr lParam);

		public static int SendMessage(HWND Wnd,	UInt32 Msg, int WParam, int lParam) {
			return (int) SendMessage(Wnd,Msg,new IntPtr(WParam), new IntPtr(lParam));
		}


		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="SendMessageW")]
		public static extern IntPtr SendMessage([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd, 
			UInt32 Msg, [MarshalAs(UnmanagedType.I4)] MouseMessagesKeyState Keys, IntPtr lParam);
	};
}