using System;
using System.Collections.Generic;


namespace VR.Connect.Protocol.Receive 
{
	abstract class ReceiveMessage 
	{
		/// <summary>
		/// Parse the specified data.
		/// command부분을 읽어서 해당 객체를 생성
		/// </summary>
		/// <param name="data">Data.</param>
		public static ReceiveMessage Parse(List<byte> data){
			ReceiveMessage msg = null;

			int cmd = data [0];

			switch (cmd) {
				case 0 : // BindSuccess
					msg = new BindSuccessMessage(data);
					break;

				case 1 : // Bind Failed
					msg = new BindFailedMessage(data);
					break;

				case 2:	//Move and Rotate
					msg = new MoveAndRotateMessage (data);
					break;

				case 3 : // Fire
					msg = new FireMessage(data);
					break;

				case 4 : // game_count
					msg = new GameCountMessage(data);
					break;

				case 5 : // game_start
					msg = new GameStartMessage(data);
					break;

				case 6 : // game_end
					msg = new GameEndMessage(data);
					break;

				case 7 : // hit
					msg = new HitMessage(data);
					break;

				case 8 : // map
					msg = new MapMessage(data);
					break;

			case 9: // player_position
				msg = new PlayerPosition (data);
				break;
			
			}


			return msg;
		}
	}
}