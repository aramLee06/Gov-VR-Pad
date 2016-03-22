using System;
using System.Collections.Generic;

using VR;
using VR.Connect;
using VR.Connect.NET;
using Send = VR.Connect.Protocol.Send;
using Receive = VR.Connect.Protocol.Receive;

using UnityEngine;

namespace VR.Connect
{
	abstract class ConnectController
	{
		public static string SERVER_IP = "192.168.5.100";
		public const int SERVER_PORT = 3500; 
		public static string REST_URL {
			get {
				return "http://" + SERVER_IP + ":3510";
			}
		}

		private int _uid;
		public int uid {
			get {
				return _uid;
			}
		}

		private VRSocket m_VRSocket;

		public Queue<Receive.ReceiveMessage> MessageQueue;

		#region Delegate & Event
		public delegate void BaseEventHandler ();
		public delegate void GetUidHandler (int uid);


		public event BaseEventHandler OnBindSuccess;
		public event GetUidHandler OnGetUid;
		#endregion

		public ConnectController() {
			MessageQueue = new Queue<Receive.ReceiveMessage> ();

			m_VRSocket = new VRSocket ();
			m_VRSocket.OnConnect += OnConnect;
			m_VRSocket.OnConnectFailed += OnConnectFailed;
			m_VRSocket.OnDataReceived += OnDataReceived;
		}

		public void Connect()
		{
			if (!m_VRSocket.IsConnected()) {
				m_VRSocket.Open(SERVER_IP, SERVER_PORT);
			}

		}

		protected void Send (Send.SendMessage msg){
			m_VRSocket.Write (msg.Generate ());
		}

		protected void ProcessMessage(Receive.ReceiveMessage msg)
		{
			if (msg is Receive.BindSuccessMessage) {
				if(OnBindSuccess != null)
					OnBindSuccess ();
			}
		}

		#region EVENT

		void OnConnect()
		{
			_uid = int.Parse (VRRest.Request("/uid"));
			if (OnGetUid != null)
				OnGetUid (uid);
		}

		void OnConnectFailed()
		{
			Debug.Log ("OnConnectFailed ");
		}

		void OnDataReceived (byte[] arr)
		{
			List<byte> data = new List<byte> ();
			data.AddRange (arr);
			lock (MessageQueue) {
				while (data.Count > 0) 
				{
					MessageQueue.Enqueue (Receive.ReceiveMessage.Parse(data));
				}
			}
		}
		#endregion
	}
}

