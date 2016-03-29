using System;
/// <summary>
/// Exit message.
/// 로비에서 보내는 게임 종료 메세지
/// </summary>
namespace VR.Connect.Protocol.Send
{
	class ExitMessage : SendMessage
	{
		public ExitMessage ()
		{
		}
		public override byte[] Generate(){
			byteList.Clear ();
			AddByte8 (9); //cmd
			return byteList.ToArray ();
		}
	}
}

