using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive 
{
	class DeathMessage : ReceiveMessage
	{
		private int _uid;
		public int uid {
			get{
				return _uid;
			}
			set{
				_uid = value;
			}
		}
		public DeathMessage (List<byte> data)
		{
			byte[] uid_arr = { data [1], data [2] };
			_uid = BitConverter.ToInt16 (uid_arr, 0);

			data.RemoveRange (0, 3);
		}
	}
}

