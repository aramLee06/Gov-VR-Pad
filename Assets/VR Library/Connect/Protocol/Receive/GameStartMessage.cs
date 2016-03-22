using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class GameStartMessage : ReceiveMessage
	{
		public GameStartMessage (List<byte> data)
		{
			data.RemoveRange (0, 1);
		}
	}
}

