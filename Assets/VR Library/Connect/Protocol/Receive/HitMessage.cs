using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class HitMessage : ReceiveMessage
	{
		private int _uid = -1;
		public int uid {
			get {
				return _uid;
			}
		}
		public HitMessage (List<byte> data)
		{
			byte[] uidArr = {data[1], data[2]};
			_uid = BitConverter.ToInt16 (uidArr, 0);
			data.RemoveRange (0, 3);
		}
	}
}

