using System;

namespace VR.Connect.Protocol.Send
{
	class FireMessage : SendMessage
	{
		public FireMessage ()
		{
		}

		public override byte[] Generate()
		{ //cmd
			byteList.Clear ();
			AddByte8 (5); //cmd
			return byteList.ToArray ();
		}
	}
}

