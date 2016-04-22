using System;
using VR;
using VR.Connect;
using VR.Connect.NET;

using Send = VR.Connect.Protocol.Send;
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
			Send.VRJoinMessage msg = new Send.VRJoinMessage (this.uid);
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

		#region SendMessage
		public void SendShoot(float postionX, float postionY, float postionZ, float velocityX, float velocityY, float velocityZ){
			Send.ShootMessage msg = new Send.ShootMessage (postionX, postionY, postionZ, velocityX, velocityY, velocityZ);
			this.Send (msg);
		}

		public void SendHit(int uid){
			Send.HitMessage msg = new Send.HitMessage (uid);
			this.Send (msg);
		}

		public void SendDeath(){
			Send.DeathMessage msg = new Send.DeathMessage ();
			this.Send (msg);
		}

		public void SendReady(int unit){
			Send.ReadyMessage msg = new Send.ReadyMessage (unit);
			this.Send (msg);
		}

		public void SendExit(){
			Send.ExitMessage msg = new Send.ExitMessage ();
			this.Send (msg);
		}

		public void SendCurrentPosition(float moveX, float moveY, float moveZ,float rotateX, float rotateY, float rotateZ,float postionX, float postionY, float postionZ){
			Send.CurrentPositionMessage msg = new Send.CurrentPositionMessage (moveX, moveY, moveZ,rotateX, rotateY, rotateZ,postionX, postionY, postionZ);
			this.Send (msg);
		}

		#endregion
	}
}

