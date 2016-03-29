using System;

/// <summary>
/// Gun fire message.
/// pad fire버튼 터치시 서버로 보내는 메세지
/// </summary>
namespace VR.Connect.Protocol.Send
{
	class GunFireMessage : SendMessage
	{
		public GunFireMessage ()
		{
		}

		public override byte[] Generate(){
			byteList.Clear ();
			AddByte8 (5); //cmd
			return byteList.ToArray ();
		}
	}
}

