using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class GameEndMessage : ReceiveMessage
	{
		public GameEndMessage (List<byte> data)
		{	
			data.RemoveRange (0, 1);
		}
	}
}

