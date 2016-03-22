using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class MoveAndRotateMessage : ReceiveMessage
	{
		public MoveAndRotateMessage (List<byte> data)
		{
			data.RemoveRange (0, 17);
		}
	}
}

