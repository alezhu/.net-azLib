using System;
using System.IO;
namespace AZLib.IniFile {
	class Sections {
		public Sections(string fileName) {
			Load(fileName);
		}

		public Sections(Stream stream) {
			Load(stream);
		}

		public Sections(StreamReader reader) {
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