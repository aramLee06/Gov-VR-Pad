using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class MapMessage : ReceiveMessage
	{
		public MapMessage (List<byte> data)
		{
			data.RemoveRange (0, 1);///
		}
	}
}

