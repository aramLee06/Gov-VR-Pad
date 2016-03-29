using System;
//게임)내가 대포 발사한거 쟤 맞음
namespace VR.Connect.Protocol.Send
{
	class HitMessage : SendMessage
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

		public HitMessage ()
		{
		}
		public HitMessage (int uid)
		{
			_uid = uid;
		}

		public override byte[] Generate()
		{ //cmd
			byteList.Clear ();
			AddByte8 (4); //cmd
			AddByte16 (uid);
			return byteList.ToArray ();
		}
	}
}

