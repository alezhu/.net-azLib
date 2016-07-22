using System;
using System.IO;
using System.Text;
namespace AZLib {
	public class EncodingUtils
	{

		static public String UnicodeToWindowsCodePage(String value) {
			Byte[] bytes = Encoding.Unicode.GetBytes(value);
			Encoding WinEncoding = Encoding.GetEncoding(Encoding.Default.WindowsCodePage);
			Byte[] bytess = Encoding.Convert(Encoding.Unicode,WinEncoding,bytes);
			return WinEncoding.GetString(bytess);
		}
	}
}