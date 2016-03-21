using System;
using VR;
using VR.Connect;
using VR.Connect.NET;
using VR.Connect.Protocol.Send;


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
			PadJoinMessage msg = new PadJoinMessage (this.uid, vr_uid);
			this.Send (msg);
		}
	}
}

