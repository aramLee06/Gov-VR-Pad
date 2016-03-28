using System;

namespace VR.Connect.Protocol.Send
{
	class FireMessage : SendMessage
	{
		public FireMessage ()
		{
		}

		public override byte[] Generate()
		{ 
			byteList.Clear ();
			AddByte8 (3); //cmd
			return byteList.ToArray ();
		}
	}
}

