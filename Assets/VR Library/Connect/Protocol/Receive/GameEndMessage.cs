using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class GameEndMessage : ReceiveMessage
	{
		private float _value;
		public float value {
			get {
				return _value;
			}
		}
		public GameEndMessage (List<byte> data)
		{	
			byte[] value_arr = { data [1], data [2] };
			_value = BitConverter.ToSingle (value_arr, 0);
			data.RemoveRange (0, 3);
		}
	}
}

