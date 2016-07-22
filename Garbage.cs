using System;
namespace AZLib {
	class Garbage {
		public static void Collect() {
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
		}
	};
	
}