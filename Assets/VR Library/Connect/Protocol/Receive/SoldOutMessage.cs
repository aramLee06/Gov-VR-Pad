using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class SoldOutMessage : ReceiveMessage
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
		private int _unitnum;
		public int unitnum {
			get{
				return _unitnum;
			}
			set{
				_unitnum = value;
			}
		}
		public SoldOutMessage (List<byte> data)
		{
			byte[] uid_arr = { data [1], data [2] };
			_uid = BitConverter.ToInt16 (uid_arr, 0);
			_unitnum = data [3];

			data.RemoveRange (0, 4);
		}
	}
}

