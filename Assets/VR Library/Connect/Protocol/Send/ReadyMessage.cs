using System;
/// <summary>
/// Ready message.
/// 로비에서 unit 고른 경우 
/// </summary>
namespace VR.Connect.Protocol.Send
{
	class ReadyMessage : SendMessage
	{
		private int _unitnum;
		public int unitnum {
			get{
				return _unitnum;
			}
			set{
				_unitnum = value;
			}
		}

		public ReadyMessage ()
		{
		}
		public ReadyMessage (int unitnum)
		{
			_unitnum = unitnum;
		}
		public override byte[] Generate(){
			byteList.Clear ();
			AddByte8 (8); //cmd
			AddByte8 ((byte)unitnum);
			return byteList.ToArray ();
		}
	}
}

