using System;

namespace VR.Connect.Protocol.Send
{
	class VRJoinMessage : SendMessage
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

		public VRJoinMessage ()
		{
			
		}

		public VRJoinMessage (int uid)
		{
			_uid = uid;
		}
			
		public override byte[] Generate(){
			byteList.Clear ();
			AddByte8 (0); //cmd
			AddByte16 (uid);
			return byteList.ToArray ();
		}
	}
}

