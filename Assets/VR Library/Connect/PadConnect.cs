using System;
using VR;
using VR.Connect;
using VR.Connect.NET;
using Send = VR.Connect.Protocol.Send;
using Receive = VR.Connect.Protocol.Receive;

namespace VR.Connect
{
	class PadConnect : ConnectController
	{
		private static PadConnect instance = null;
		public static PadConnect Instance {
			get {
				if (instance == null) {
					instance = new PadConnect ();
				}
				return instance;					
			}
		}

		public PadConnect(){
			
		}
		/// <summary>
		/// Pad_Join_Message Send to Server
		/// </summary>
		/// <param name="vr_uid">Vr uid.</param>
		public void Join(int vr_uid)
		{
			Send.PadJoinMessage msg = new Send.PadJoinMessage (this.uid, vr_uid);
			this.Send (msg);
		}

		/// <summary>
		/// Sends the control data.
		/// control data is move and rotate. (float)
		/// move :: unit (left)
		/// rotate :: aim (right)
		/// </summary>
		/// <param name="move_x">Move x.</param>
		/// <param name="move_y">Move y.</param>
		/// <param name="rotate_x">Rotate x.</param>
		/// <param name="rotate_y">Rotate y.</param>
		public void SendControlData(float move_x, float move_y, float rotate_x, float rotate_y) {
			Send.MoveAndRotateMessage msg = new Send.MoveAndRotateMessage (move_x, move_y, rotate_x, rotate_y);
			this.Send (msg);
		}

		/// <summary>
		/// Send Fire_Message. 
		/// When press fire btn on pad
		/// </summary>
		public void SendFire(){
			Send.GunFireMessage msg = new Send.GunFireMessage ();
			this.Send (msg);
		}

		public void Run()
		{
			lock (MessageQueue) 
			{
				if (MessageQueue.Count > 0) 
				{
					Receive.ReceiveMessage msg = MessageQueue.Dequeue ();
					ProcessMessage (msg);
				}
			}
		}
	}
}

