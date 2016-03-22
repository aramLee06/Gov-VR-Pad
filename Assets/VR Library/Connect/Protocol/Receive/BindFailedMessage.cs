using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class BindFailedMessage : ReceiveMessage
	{
		private int code = -1;
		public int Code {
			get {
				return code;
			}
		}

		public BindFailedMessage (List<byte> data)
		{
			code = data [1];
			data.RemoveRange (0, 2);
		}
	}
}

