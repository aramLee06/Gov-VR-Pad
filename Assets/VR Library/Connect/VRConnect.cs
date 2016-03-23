using System;
using VR;
using VR.Connect;
using VR.Connect.NET;
using VR.Connect.Protocol.Send;

using Receive = VR.Connect.Protocol.Receive;

namespace VR.Connect
{
	class VRConnect : ConnectController
	{
		private static VRConnect instance = null;
		public static VRConnect Instance {
			get {
				if (instance == null) {
					instance = new VRConnect ();
				}
				return instance;		
			}
		}
		/// <summary>
		/// VR_Join_Message Send to Server
		/// </summary>
		public void Join()
		{
			VRJoinMessage msg = new VRJoinMessage (this.uid);
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

