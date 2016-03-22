using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class GameCountMessage : ReceiveMessage
	{
		private int _value;
		public int value {
			get {
				return _value;
			}
		}
		public GameCountMessage (List<byte> data)
		{
			_value = data [1];
			data.RemoveRange (0, 2);
		}
	}
}

