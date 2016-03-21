using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Connect.Protocol.Send
{
	abstract class SendMessage
	{
		protected List<byte> byteList;

		public SendMessage ()
		{
			byteList = new List<byte> ();
		}

		protected void AddByteFloat(float val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte v in bytes)
			{
				byteList.Add(v);
			}
		}

		protected void AddByte8(byte value)
		{
			byteList.Add(value);
		}

		protected void AddByte16(int val)
		{
			Int16 value = (Int16)val;
			byte[] bytes = BitConverter.GetBytes(value);
			foreach (byte v in bytes)
			{
				byteList.Add(v);
			}
		}

		protected void AddByte32(int val)
		{
			byte[] bytes = BitConverter.GetBytes(val);
			foreach (byte v in bytes)
			{
				byteList.Add(v);
			}
		}

		protected void AddByteString(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			for (int i = 0; i < bytes.Length; i++) 
			{
				byte v = bytes [i];
				byteList.Add(v);
			}
		}

		public abstract byte[] Generate();
	}
}

