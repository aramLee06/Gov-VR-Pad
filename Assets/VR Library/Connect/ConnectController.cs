﻿using System;
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

		private int _uid = -1;
		public int uid {
			get {
				if (_uid == -1)
					_uid = int.Parse (VRRest.Request("/uid"));
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


		public event BaseEventHandler OnConnectSucceed;
		public event BaseEventHandler OnConnectFailed;
		public event BaseEventHandler OnBindSuccess;
		public event BaseEventHandler OnFire;
		public event BaseEventHandler OnGameStart;
		public event BaseEventHandler OnGameEnd;
		public event BaseEventHandler OnMap;

		public event GameCountHandler OnGameCount;

		public event ReceiveUidEventHandler OnHit;

		public event BindFailedHandler OnBindFailed;

		public event MoveAndRotateHandler OnControl;
		#endregion

		public ConnectController() {
			MessageQueue = new Queue<Receive.ReceiveMessage> ();

			m_VRSocket = new VRSocket ();
			m_VRSocket.OnConnect += OnConnect;
			m_VRSocket.OnConnectFailed += OnConnectFailed_Socket;
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

		void OnConnect()
		{
			if (OnConnectSucceed != null)
				OnConnectSucceed ();
		}

		void OnConnectFailed_Socket()
		{
			if (OnConnectFailed != null)
				OnConnectFailed ();
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

