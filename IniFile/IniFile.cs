using System.IO;
namespace AZLib.IniFile {
	class IniFile {
		public IniFile(string fileName) {
			Load(fileName);
		}

		public IniFile(Stream stream) {
			Load(stream);
		}

		public IniFile(StreamReader reader) {
			Load(reader);
		}

		public void Load(string fileName) {
			using (StreamReader reader = new StreamReader(fileName)) {
				Load(reader);
				reader.Close();
			}
		}

		public void Load(Stream stream) {
			using (StreamReader reader = new StreamReader(stream)) {
				Load(reader);
				reader.Close();
			}
		}

		public void Load(StreamReader reader) {
			string line = null;
			 while ((line = reader.ReadLine()) != null) {
				line = line.Trim();
				
			}
		}
	};
}