using System;

namespace VR.Connect.Protocol.Send
{
	class PadJoinMessage : SendMessage
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
		private int _VR_uid;
		public int VR_uid {
			get{
				return _VR_uid;
			}
			set{
				_VR_uid = value;
			}
		}

		public PadJoinMessage ()
		{
			
		}
		public PadJoinMessage (int uid, int VR_uid)
		{
			_uid = uid;
			_VR_uid = VR_uid;
		}


		public override byte[] Generate()
		{
			byteList.Clear ();
			AddByte8 (1); //cmd
			AddByte16 (uid);
			AddByte16 (VR_uid);
			return byteList.ToArray ();
		}
	}
}

