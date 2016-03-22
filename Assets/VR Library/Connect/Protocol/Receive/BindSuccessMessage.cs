using System;
using System.Collections.Generic;


namespace VR.Connect.Protocol.Receive 
{
	class BindSuccessMessage : ReceiveMessage
	{
		public BindSuccessMessage(List<byte> data){
			data.RemoveRange (0, 1);
		}
	}
}