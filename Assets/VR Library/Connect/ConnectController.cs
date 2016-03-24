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
	public enum MoveType {
		Straight, Rearguard, Left, Right, Stop
	};

	public enum RotateType {
		Up, Down, Left, Right, Stop
	};
	
	abstract class ConnectController
	{
		public static string SERVER_IP = "192.168.5.104";
		public const int SERVER_PORT = 3500; 
		public static string REST_URL {
			get {
				return "http://" + SERVER_IP + ":3510";
			}
		}

		private int _uid = -1;
		public int uid {
			get {
				if (_uid == -1)
					_uid = int.Parse (VRRest.Request("/uid")); //rest로 uid를 받아온다.
				return _uid;
			}
		}

		private VRSocket m_VRSocket;

		public Queue<Receive.ReceiveMessage> MessageQueue;

		#region Delegate & Event
		public delegate void BaseEventHandler ();
		public delegate void BindFailedHandler (int code);
		public delegate void ReceiveUidEventHandler (int uid);
		public delegate void GameCountHandler (int value);
		public delegate void MoveAndRotateHandler (Vector2 move, Vector2 rotate);

		/// <summary>
		/// Move and rotate enum handler.
		/// </summary>
		public delegate void MoveAndRotateEnumHandler (MoveType move, RotateType rotate);

		/// <summary>
		/// Occurs when on connect succeed.
		/// VR or Pad - Server (socket open) 
		/// </summary>
		public event BaseEventHandler OnConnectSucceed;

		/// <summary>
		/// Occurs when on connect failed.
		/// PVR or PAd -/- Server (socket open) 
		/// </summary>
		public event BaseEventHandler OnConnectFailed;

		/// <summary>
		/// Occurs when on bind success. (Between VR to Pad)
		/// </summary>
		public event BaseEventHandler OnBindSuccess;

		/// <summary>
		/// Occurs when on fire. (Press fire btn on pad)
		/// </summary>
		public event BaseEventHandler OnFire;

		/// <summary>
		/// Occurs when on game start.
		/// </summary>
		public event BaseEventHandler OnGameStart;

		/// <summary>
		/// Occurs when on game end.
		/// </summary>
		public event BaseEventHandler OnGameEnd;

		/// <summary>
		/// 미정
		/// </summary>
		public event BaseEventHandler OnMap;

		public event GameCountHandler OnGameCount;

		/// <summary>
		/// Occurs when on hit. (미사일 맞았을 때)
		/// </summary>
		public event ReceiveUidEventHandler OnHit;

		/// <summary>
		/// Occurs when on bind failed. (Between VR to Pad)
		/// </summary>
		public event BindFailedHandler OnBindFailed;

		/// <summary>
		/// 패드에서의 동작 :: move, rotate(aim움직임)
 		/// </summary>
		public event MoveAndRotateHandler OnControl;

		/// <summary>
		/// Occurs when on control type.
		/// </summary>
		public event MoveAndRotateEnumHandler OnControlType;
		#endregion

		public ConnectController() {
			MessageQueue = new Queue<Receive.ReceiveMessage> ();

			m_VRSocket = new VRSocket ();
			m_VRSocket.OnConnect += OnConnect;
			m_VRSocket.OnConnectFailed += OnConnectFailed_Socket;
			m_VRSocket.OnDataReceived += OnDataReceived;
		}

		/// <summary>
		/// Connect Server (Socket open)
		/// </summary>
		public void Connect()
		{
			if (!m_VRSocket.IsConnected()) {
				m_VRSocket.Open(SERVER_IP, SERVER_PORT);
			}

		}

		/// <summary>
		/// Send the specified msg to Server. (Socket Write)
		/// </summary>
		/// <param name="msg">Message.</param>
		protected void Send (Send.SendMessage msg){
			m_VRSocket.Write (msg.Generate ());
		}

		/// <summary>
		/// Processes the message.
		/// Server to Client Message.
		/// 서버로부터 온 메세지를 큐에서 꺼내 읽고 해당 이벤트 발생.
		/// </summary>
		/// <param name="msg">Message.</param>
		protected void ProcessMessage(Receive.ReceiveMessage msg)
		{
			if (msg is Receive.BindSuccessMessage) {
				if (OnBindSuccess != null)
					OnBindSuccess ();
			} else if (msg is Receive.BindFailedMessage) {
				if (OnBindFailed != null)
					OnBindFailed (((Receive.BindFailedMessage)msg).Code);
			} else if (msg is Receive.MoveAndRotateMessage) {
				Receive.MoveAndRotateMessage m = (Receive.MoveAndRotateMessage)msg;

				Vector2 move = new Vector2 (m.moveX, m.moveY);
				Vector2 rotate = new Vector2 (m.rotateX, m.rotateY);

				if (OnControl != null)
					OnControl (move, rotate);

				if (OnControlType != null)
					OnControlType (getMoveType (move), getRotateType (rotate));
			} else if (msg is Receive.FireMessage) { 
				if (OnFire != null)
					OnFire ();
			} else if (msg is Receive.GameStartMessage) {
				if (OnGameStart != null)
					OnGameStart ();
			} else if (msg is Receive.GameCountMessage) {
				if (OnGameCount != null)
					OnGameCount (((Receive.GameCountMessage)msg).value);
			} else if (msg is Receive.GameEndMessage) {
				if (OnGameEnd != null)
					OnGameEnd ();
			} else if (msg is Receive.HitMessage) {
				if (OnHit != null)
					OnHit (((Receive.HitMessage)msg).uid);
			} else if (msg is Receive.MapMessage) {
				if (OnMap != null)
					OnMap ();
			}
		}

		#region EVENT

		/// <summary>
		/// Raises the connect event.
		/// Connect Succeed (Socket Open)ß
		/// </summary>
		void OnConnect()
		{
			if (OnConnectSucceed != null)
				OnConnectSucceed ();
		}

		/// <summary>
		/// Raises the connect failed. (socket open failed)
		/// </summary>
		void OnConnectFailed_Socket()
		{
			if (OnConnectFailed != null)
				OnConnectFailed ();
		}

		/// <summary>
		/// Raises the data received event.
		/// byte[] array --> List<byte>
		/// 받은 data를 parse해서 List에 넣어준다.
		/// </summary>
		/// <param name="arr">Arr.</param>
		void OnDataReceived (byte[] arr)
		{
			List<byte> data = new List<byte> (); 
			data.AddRange (arr);
			lock (MessageQueue) {
				while (data.Count > 0) 
				{
					MessageQueue.Enqueue (Receive.ReceiveMessage.Parse(data)); //Parse 하고 queue에 넣는다!
				}
			}
		}
		#endregion

		private MoveType getMoveType(Vector2 move){
			if (Math.Abs (move.x) > Math.Abs (move.y)) {
				if (move.x > 0) {
					return MoveType.Right;	
				} else {
					return MoveType.Left;
				}
			} else {
				if (move.y > 0) {
					return MoveType.Straight;
				} else if (move.y < 0) {
					return MoveType.Rearguard;
				} else {
					return MoveType.Stop;
				}
			}
		}

		private RotateType getRotateType(Vector2 rotate){
			if (Math.Abs (rotate.x) > Math.Abs (rotate.y)) {
				if (rotate.x > 0) {
					return RotateType.Right;	
				} else {
					return RotateType.Left;
				}
			} else {
				if (rotate.y > 0) {
					return RotateType.Up;
				} else if (rotate.y < 0) {
					return RotateType.Down;
				} else {
					return RotateType.Stop;
				}
			}
		}
	}
}

