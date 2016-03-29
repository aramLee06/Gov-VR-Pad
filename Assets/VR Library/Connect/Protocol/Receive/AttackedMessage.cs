using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive 
{
	class AttackedMessage : ReceiveMessage
	{
		private float _uid;
		public float uid {
			get{
				return _uid;
			}
			set{
				_uid = value;
			}
		}
		public AttackedMessage (List<byte> data)
		{
			byte[] uid_arr = { data [1], data [2] };
			_uid = BitConverter.ToSingle (uid_arr, 0);
			data.RemoveRange (0, 3);
		}
	}
}

