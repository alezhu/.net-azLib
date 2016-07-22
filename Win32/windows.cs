using System;
using System.Runtime.InteropServices;
//using HWND = System.Runtime.InteropServices.HandleRef;
namespace AZLib.Win32 {
	public enum GetWindowType {First=0,Last=1,Next=2,Prev=3,Owner=4,Child=5};

	[Flags]
	public enum MouseMessagesKeyState {MK_NONE = 0, MK_LBUTTON = 1, MK_RBUTTON = 2, MK_SHIFT = 4,  MK_CONTROL = 8,  MK_MBUTTON = 0x10};

	public enum GetWindowLongIndex {
		GWL_WNDPROC = -4,
		GWL_HINSTANCE = -6,
		GWL_HWNDPARENT = -8,
		GWL_STYLE = -16,
		GWL_EXSTYLE = -20,
		GWL_USERDATA = -21,
		GWL_ID = -12
	}

	public enum WindowMessages {
		WM_NULL = 0,
		WM_CREATE = 1,
		WM_CLOSE = 16,
		WM_SHOWWINDOW       = 0x0018,
		WM_LBUTTONDOWN  = 0x0201,
		WM_LBUTTONUP        = 0x0202,
		WM_LBUTTONDBLCLK    = 0x0203
	};

	public enum ShowWindowCommands {
		SW_HIDE = 0,
		SW_SHOWNORMAL = 1,
		SW_NORMAL = 1,
		SW_SHOWMINIMIZED = 2,
		SW_SHOWMAXIMIZED = 3,
		SW_MAXIMIZE = 3,
		SW_SHOWNOACTIVATE = 4,
		SW_SHOW = 5,
		SW_MINIMIZE = 6,
		SW_SHOWMINNOACTIVE = 7,
		SW_SHOWNA = 8,
		SW_RESTORE = 9,
		SW_SHOWDEFAULT = 10
	}
	
	[Flags]
	public enum WindowsStyles: uint {
		WS_OVERLAPPED = 0,
		WS_POPUP = 0x80000000,
		WS_CHILD = 0x40000000,
		WS_MINIMIZE = 0x20000000,
		WS_VISIBLE = 0x10000000,
		WS_DISABLED = 0x8000000,
		WS_CLIPSIBLINGS = 0x4000000,
		WS_CLIPCHILDREN = 0x2000000,
		WS_MAXIMIZE = 0x1000000,
		WS_CAPTION =  WS_BORDER | WS_DLGFRAME ,
		WS_BORDER = 0x800000,
		WS_DLGFRAME = 0x400000,
		WS_VSCROLL = 0x200000,
		WS_HSCROLL = 0x100000,
		WS_SYSMENU = 0x80000,
		WS_THICKFRAME = 0x40000,
		WS_GROUP = 0x20000,
		WS_TABSTOP = 0x10000,

		WS_MINIMIZEBOX = 0x20000,
		WS_MAXIMIZEBOX = 0x10000,

		WS_TILED = WS_OVERLAPPED,
		WS_ICONIC = WS_MINIMIZE,
		WS_SIZEBOX = WS_THICKFRAME,

		// Common Window Styles 
		WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
		WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
		WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
		WS_CHILDWINDOW = WS_CHILD
	}

	[Flags]
	public enum ExtendedWindowStyles:uint {
		WS_EX_DLGMODALFRAME = 1,
		WS_EX_NOPARENTNOTIFY = 4,
		WS_EX_TOPMOST = 8,
		WS_EX_ACCEPTFILES = 0x10,
		WS_EX_TRANSPARENT = 0x20,
		WS_EX_MDICHILD = 0x40,
		WS_EX_TOOLWINDOW = 0x80,
		WS_EX_WINDOWEDGE = 0x100,
		WS_EX_CLIENTEDGE = 0x200,
		WS_EX_CONTEXTHELP = 0x400,

		WS_EX_RIGHT = 0x1000,
		WS_EX_LEFT = 0,
		WS_EX_RTLREADING = 0x2000,
		WS_EX_LTRREADING = 0,
		WS_EX_LEFTSCROLLBAR = 0x4000,
		WS_EX_RIGHTSCROLLBAR = 0,

		WS_EX_CONTROLPARENT = 0x10000,
		WS_EX_STATICEDGE = 0x20000,
		WS_EX_APPWINDOW = 0x40000,
		WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
		WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST
	}	

	public class  Windows{
		public const string AdvApi32Dll  = "advapi32.dll";
		public const string Kernel32Dll  = "kernel32.dll";
		public const string MPRDll       = "mpr.dll";
		public const string VersionDll   = "version.dll";
		public const string Comctl32Dll  = "comctl32.dll";
		public const string GDI32Dll     = "gdi32.dll";
		public const string OpenGL32Dll  = "opengl32.dll";
		public const string User32Dll    = "user32.dll";
		public const string WinTrustDll  = "wintrust.dll";
		public const string Msimg32Dll   = "msimg32.dll";

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

		[DllImport(Windows.Kernel32Dll,CharSet = CharSet.Unicode,EntryPoint="Beep")]
		private static extern IntPtr _Beep(UInt32 dwFreq, UInt32 dwDuration);
		public static bool Beep(UInt32 Freq, UInt32 Duration) {
			IntPtr p = _Beep(Freq, Duration);
			return (p != IntPtr.Zero);
		}

		//function SetWindowLongA(hWnd: HWND; nIndex: Integer; dwNewLong: Longint): Longint; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="SetWindowLongW")]
		public static extern IntPtr SetWindowLong([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd, 
			[MarshalAs(UnmanagedType.I4)]GetWindowLongIndex nIndex, UInt32 NewLong);

		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="SetWindowLongW")]
		public static extern IntPtr SetWindowLong(IntPtr Wnd, [MarshalAs(UnmanagedType.I4)]GetWindowLongIndex nIndex, UInt32 NewLong);

		//function GetWindowLongA(hWnd: HWND; nIndex: Integer): Longint; stdcall;
		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="GetWindowLongW")]
		private static extern IntPtr _GetWindowLong([MarshalAs(UnmanagedType.CustomMarshaler,MarshalType="AZLib.Win32.HWNDMarshaler")]HWND Wnd, 
			[MarshalAs(UnmanagedType.I4)]GetWindowLongIndex nIndex);
		public static UInt32 GetWindowLong(HWND Wnd, GetWindowLongIndex nIndex) {
			IntPtr p = _GetWindowLong(Wnd,nIndex);
			return (UInt32)p;
		}

		[DllImport(Windows.User32Dll,CharSet = CharSet.Unicode,EntryPoint="GetWindowLongW")]
		private static extern IntPtr _GetWindowLong(IntPtr Wnd, [MarshalAs(UnmanagedType.I4)]GetWindowLongIndex nIndex);
		public static UInt32 GetWindowLong(IntPtr Wnd, GetWindowLongIndex nIndex) {
			IntPtr p = _GetWindowLong(Wnd,nIndex);
			return (UInt32)p;
		}

	};
}