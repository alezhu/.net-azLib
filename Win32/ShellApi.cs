using System;
using System.Runtime.InteropServices;
namespace AZLib.Win32 {
	unsafe public class ShellApi {
		public const string ShellDll = "shell32.dll";

		public enum NotifyIconMessage {Add,Modify,Delete};
		[Flags]
		public enum NotifyIconFormat {None=0,Message=1,Icon=2,Tip=4};

		[StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
		public class NotifyIconData {
			public NotifyIconData() {
				cbSize = 152;//(UInt32)sizeof(NotifyIconData);
			}
			public UInt32 cbSize	= 0;//: DWORD;
			public Int32 Wnd		= 0;//: HWND;
			public UInt32 uID		= 0;//: UINT;
			[MarshalAs(UnmanagedType.U4)] 
			public NotifyIconFormat uFlags	= 0;//: UINT;
			public UInt32 uCallbackMessage	= 0;//: UINT;
			public Int32 hIcon		= 0;//: HICON;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=64)]
			public string szTip = null;//: array [0..63] of AnsiChar;
		}


		[DllImport(ShellApi.ShellDll,CharSet = CharSet.Unicode)]
		public static extern bool Shell_NotifyIcon([MarshalAs(UnmanagedType.U4)] NotifyIconMessage dwMessage, [In,Out] NotifyIconData lpData);
	};
}