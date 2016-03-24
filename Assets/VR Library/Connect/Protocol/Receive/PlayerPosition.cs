using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	public class PlayerPosition : ReceiveMessage
	{
		public PlayerPosition (List<byte> data)
		{
			int len = data [1];
			data.RemoveRange (0,len);
		}
	}
}

