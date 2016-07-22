using System;
using System.Runtime.InteropServices;
namespace AZLib.Win32 {
	[StructLayoutAttribute(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
	public class RECT {
		public RECT () {
			Left = Top = Right = Bottom = 0;
		}
		public Int32 Left, Top, Right, Bottom;
		public override string ToString() {
			return String.Format("{0},{1},{2},{3}", Left, Top, Right, Bottom);
		}
	}

}