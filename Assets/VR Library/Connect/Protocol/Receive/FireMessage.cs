using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class FireMessage : ReceiveMessage
	{
		
		public FireMessage (List<byte> data)
		{	
			data.RemoveRange (0, 1);
		}
	}
}

