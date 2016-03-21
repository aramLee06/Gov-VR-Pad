using System;
using VR;
using VR.Connect;
using VR.Connect.NET;
using VR.Connect.Protocol.Send;


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

		public void Join()
		{
			VRJoinMessage msg = new VRJoinMessage (this.uid);
			this.Send (msg);
		}
	}
}

