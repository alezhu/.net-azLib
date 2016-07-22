using System;
using System.Runtime.InteropServices;
namespace AZLib.Win32 {
	public class HWND {
		private HandleRef _hr ;
		public static implicit operator HandleRef (HWND value){
			return value._hr;
		}
		public static implicit operator HWND (HandleRef value){
			return new HWND(value);
		}
		public HWND(HandleRef value) {
			_hr = value;
		}

		public static implicit operator int (HWND value){
			return (int)value._hr.Handle;
		}
		public static implicit operator HWND (int value){
			return new HWND(value);
		}
		public HWND(int value) {
			_hr = new HandleRef(null,new IntPtr(value));
		}

		public static implicit operator IntPtr (HWND value){
			return value._hr.Handle;
		}
		public static implicit operator HWND (IntPtr value){
			return new HWND(value);
		}
		public HWND(IntPtr value) {
			_hr = new HandleRef(null,value);
		}

		public static bool operator == (HWND value1, int value2) {
			if  (value1 == null) {
				if (value2 == 0) {
					return true;
				} else {
					return false;
				}
			}
			return (int)value1 == value2;
		}
		public static bool operator != (HWND value1, int value2) {
			return !(value1 == value2);
		}

		public override System.Int32 GetHashCode () {
			return ((int)this).GetHashCode();
		}

		public override System.Boolean Equals (System.Object obj) {
			if ((obj is HWND) || (obj is HandleRef) || (obj is IntPtr) || (obj is int)) {
				return this == (HWND)obj;
			}
			return false;
		}

		public override System.String ToString () {
			return ((int)this).ToString();
		}
	}

	public class HWNDMarshaler : ICustomMarshaler {
		public virtual void CleanUpManagedData (object ManagedObj) {
			//((HWND)ManagedObject)._hr.Handle.
			//Console.WriteLine("CleanUpManagedData");
		}
		public virtual void CleanUpNativeData (IntPtr pNativeData) {
			//Console.WriteLine("CleanUpNativeData");
		}
		public virtual Int32 GetNativeDataSize () {
			//Console.WriteLine("GetNativeDataSize");
			return 4;//sizeof(int);
		}
		public virtual IntPtr MarshalManagedToNative (object ManagedObj){
			//Console.WriteLine("MarshalManagedToNative");
			return (IntPtr)((HWND)ManagedObj);
		}
		public virtual object MarshalNativeToManaged (IntPtr pNativeData) {
			//Console.WriteLine("MarshalNativeToManaged");
			return new HWND(pNativeData);
		}
		public static ICustomMarshaler GetInstance (string pstrCookie) {
			//Console.WriteLine("GetInstance");
			return (new HWNDMarshaler()) as ICustomMarshaler;
		}
	};
}