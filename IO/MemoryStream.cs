using System;
using System.IO;
using System.Collections;
using SystemMS = System.IO.MemoryStream;
using int64 = System.Int64;
namespace AZLib.IO {
	public class MemoryStream:System.IO.Stream {
		public static int StartBufferSize = 4096;
		internal SystemMemoryStreamList Streams = new SystemMemoryStreamList();
		protected int CurStream = 0;
		protected int64 CurPosition = 0;
		protected int64 CurLength = 0;

		public override System.Boolean CanRead {
			get {
				return (CurLength>0) && (CurPosition<CurLength);
			}
		}
		public override System.Boolean CanSeek {
			get {
				return true;
			}
		}
		public override System.Boolean CanWrite {
			get {
				return true;
			}
		}
		public override System.Int64 Length {
			get {
				return CurLength;
			}
		}
		public override System.Int64 Position {
			get {
				return CurPosition;
			}
			set {
				Seek(value,SeekOrigin.Begin);
			}
		}


		public override void Flush () {
			foreach (SystemMS ms in Streams){
				ms.Flush();
			}
		}

		public override int Read( System.Byte[] buffer, System.Int32 offset, System.Int32 count) {
			int64 BytesToEnd = CurLength-CurPosition;
			int64 TotalRead = (count > BytesToEnd)?BytesToEnd:count;
			int Readed = 0;
			while (TotalRead > 0){
				SystemMS ms = Streams[CurStream];//GetStreamByPosition(Position);
				BytesToEnd = ms.Length - ms.Position;
				int BytesToRead = (int)((BytesToEnd<TotalRead)?BytesToEnd:TotalRead);
				BytesToRead = ms.Read(buffer,offset,BytesToRead);
				Readed += BytesToRead;
				TotalRead -= BytesToRead;
				Position += BytesToRead;
				offset += BytesToRead;
			}
			return Readed;
		}

		public override System.Int64 Seek (System.Int64 offset, System.IO.SeekOrigin origin) {
			if (origin == SeekOrigin.End){
				return Seek(CurLength-offset,SeekOrigin.Begin);
			}
			if (origin == SeekOrigin.Current){
				return Seek(CurPosition+offset,SeekOrigin.Begin);
			}
			if (origin == SeekOrigin.Begin){
				CurPosition = offset;
				int64 PosInStream =0;
				CurStream = GetStreamIndexByPosition(offset, out PosInStream);
//				Console.WriteLine("Seek: CurStream= {0}, Streams.Count = {1}, PosInStream= {2}",CurStream,Streams.Count,PosInStream);
				Streams[CurStream].Position = PosInStream;
				return offset;
			} 
			throw new ArgumentOutOfRangeException("origin");
		}

		public override void SetLength (System.Int64 value) {
			if (CurLength != value){
				Capacity = value;
				CurLength = value;
				if (CurPosition > value){
					Seek(value,SeekOrigin.Begin);
				}
			}
		}

		public override void Write (System.Byte[] buffer, System.Int32 offset, System.Int32 count) {
			if (Capacity < (Length + count)){
				Capacity = Length + count;
			}
			int64 TotalCount = count;
			while (TotalCount >0){
				SystemMS ms = Streams[CurStream];//GetStreamByPosition(Position);
				int64 FreeBytes = ms.Length-ms.Position;
				int BytesToWrite = (int)((FreeBytes<TotalCount)?FreeBytes:TotalCount);
				ms.Write(buffer,offset,BytesToWrite);
				TotalCount -= BytesToWrite;
				offset += BytesToWrite;
				CurLength += BytesToWrite;
				Position += BytesToWrite;
			}

		}

		protected int64 GetStreamSizeByIndex(int StreamIndex) {
			return StartBufferSize << StreamIndex;
		}

		protected int GetStreamIndexByPosition(int64 Position, out int64 InStreamPosition) {
			int64 Delta = Position;
			for (int i = 0; ;i++ ){
				int64 StreamSize = ((int64)StartBufferSize << i);
				if (Delta <= StreamSize){
					InStreamPosition = Delta;
					return i;
				}
				Delta -= StreamSize;
			}
		}

		protected SystemMS GetStreamByPosition(int64 Position) {
			int64 PosInStream=0;
			int index = GetStreamIndexByPosition(Position,out PosInStream);
			if (index >= Streams.Count){
				throw new ArgumentOutOfRangeException("Position");
			}
			return Streams[index];
		}

		protected int64 Capacity {
			get {
				int64 result = 0;
				for (int i = 0 ;i<Streams.Count ;i++ ){
					result += StartBufferSize << i;
				}
				return result;
			}
			set {
				int64 CurCapacity = Capacity;
				if (value > CurCapacity ){
					int i = Streams.Count;
					while (value >0){
						int64 StreamSize = (int64)StartBufferSize << i++;
						Streams.Add(new SystemMS(new byte[StreamSize],true));
						value -= StreamSize;
					}
				} else if (value < CurCapacity){
					int i = 0;
					while (value > 0){
						int64 StreamSize = (int64)StartBufferSize << i++;
						value -= StreamSize;
					}
					Streams.RemoveRange(i,Streams.Count-i);
				}
			}
		}
	};
	
	class SystemMemoryStreamList: System.Collections.ArrayList {
		public new SystemMS this[int index] {
			get { return (SystemMS) base[index];}
		}
	}
}