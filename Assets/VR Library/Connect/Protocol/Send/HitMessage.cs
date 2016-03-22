using System;

namespace VR.Connect.Protocol.Send
{
	class HitMessage : SendMessage
	{
		public HitMessage ()
		{
		}

		public override byte[] Generate()
		{ //cmd
			byteList.Clear ();
			AddByte8 (4); //cmd
			return byteList.ToArray ();
		}
	}
}

