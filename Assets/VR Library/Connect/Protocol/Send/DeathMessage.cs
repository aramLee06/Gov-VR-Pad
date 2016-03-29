using System;
/// <summary>
/// Death message.
/// 게임중 나 죽음
/// </summary>
namespace VR.Connect.Protocol.Send
{
	class DeathMessage : SendMessage
	{
		public DeathMessage ()
		{
		}
		public override byte[] Generate(){
			byteList.Clear ();
			AddByte8 (7); //cmd
			return byteList.ToArray ();
		}
	}
}

