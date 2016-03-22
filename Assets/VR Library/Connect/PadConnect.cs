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

		public void Join(int vr_uid)
		{
			Send.PadJoinMessage msg = new Send.PadJoinMessage (this.uid, vr_uid);
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

