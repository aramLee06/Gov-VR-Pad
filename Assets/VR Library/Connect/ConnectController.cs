using System;
using VR;
using VR.Connect;
using VR.Connect.NET;
using UnityEngine;
using VR.Connect.Protocol.Send;

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

		VRSocket m_VRSocket;

		private int _uid;
		public int uid {
			get {
				return _uid;
			}
		}

		public ConnectController() {
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

		protected void Send (SendMessage msg){
			m_VRSocket.Write (msg.Generate ());
		}


		#region EVENT

		void OnConnect()
		{
			_uid = int.Parse (VRRest.Request("/uid"));
		}

		void OnConnectFailed()
		{
			Debug.Log ("OnConnectFailed ");
		}

		void OnDataReceived (byte[] arr)
		{
			
		}

		#endregion
	}
}

