using System;
using System.Collections.Generic;


namespace VR.Connect.Protocol.Receive 
{
	abstract class ReceiveMessage 
	{
		public static ReceiveMessage Parse(List<byte> data){
			ReceiveMessage msg = null;

			int cmd = data [0];

			switch (cmd) {
				case 0 : // BindSuccess
					msg = new BindSuccessMessage(data);
					break;
			}

			return msg;
		}
	}
}