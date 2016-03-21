using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;

namespace VR.Connect.NET
{
	public class VRSocket
	{
		const int RBUFFER_SIZE = (4096); //ReceiveBuffer Size

		public static int SOCK_ERROR_SEND = 1;
		public static int SOCK_ERROR_SEND_FILE = 2;
		public static int SOCK_ERROR_READ = 3;

		string hostIP;
		int hostPort;
		private Socket socket;
		private Socket cbSock; //async callback socket

		private byte[] recieveBuffer = new byte[RBUFFER_SIZE];

		public delegate void OnConnectHandler();
		public delegate void OnConnectFailedHandler();
		public delegate void OnDisConnectHandler();
		public delegate void OnDataReceivedHandler(byte[] arr);
		public delegate void OnErrorHandler(int err, string msg);
		public delegate void OnSendEndedHandler();
		public delegate void OnFileSendEndedHandler();

		public event OnConnectHandler OnConnect;
		public event OnConnectFailedHandler OnConnectFailed;
		public event OnDisConnectHandler OnDisconnect;
		public event OnDataReceivedHandler OnDataReceived;
		public event OnErrorHandler OnError;
		public event OnSendEndedHandler OnSendEnded;
		public event OnFileSendEndedHandler OnFileSendEnded;


		public bool Open(string host, int port)
		{
			hostIP = host;
			hostPort = port;

			try
			{
				IPAddress serverIp = IPAddress.Parse(hostIP);
				IPEndPoint ipep = new IPEndPoint(serverIp, port);

				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				socket.BeginConnect(ipep, new AsyncCallback(OnConnected), socket);

			}
			catch (Exception e)
			{
				if (OnConnectFailed != null)
				{
					OnConnectFailed();
				}

				return false;
			}

			return true;
		}


		private void OnConnected(IAsyncResult ar)
		{
			try
			{
				if (socket.Connected == false)
				{
					if (OnConnectFailed != null)
					{
						OnConnectFailed();
					}
					return;
				}

				Socket tmpSocket = (Socket) ar.AsyncState;

				tmpSocket.EndConnect(ar);
				cbSock = tmpSocket; 
				cbSock.BeginReceive(this.recieveBuffer, 0, recieveBuffer.Length, SocketFlags.None, new AsyncCallback(OnMessaged), cbSock);
				if (OnConnect != null)
				{
					OnConnect();
				}
			}
			catch (SocketException se)
			{
				if (se.SocketErrorCode == SocketError.NotConnected)
				{
					Console.WriteLine(se.Message);

					this.Open(this.hostIP, this.hostPort);
				}
			}

		}//OnConnected


		private void OnMessaged(IAsyncResult ar)
		{
			try
			{
				Socket tempSocket = (Socket) ar.AsyncState;
				int nSize = tempSocket.EndReceive(ar); 
				if (nSize != 0) //ÀÐÀº°Ô ÀÖÀ»¶§
				{
					string msg = new UTF8Encoding().GetString(recieveBuffer, 0, nSize); 
					byte[] arr = recieveBuffer.Skip(0).Take(nSize).ToArray();
					if (OnDataReceived != null)
					{
						OnDataReceived(arr);
					}
					this.Receive();
				}
			}
			catch (SocketException se)
			{
				if (se.SocketErrorCode == SocketError.ConnectionReset)
				{
					this.Open(this.hostIP, this.hostPort);
				}
			}
		}


		public void Receive()
		{
			cbSock.BeginReceive(this.recieveBuffer, 0, recieveBuffer.Length, SocketFlags.None, new AsyncCallback(OnMessaged), cbSock);
		}
			

		public void Write(byte[] msg, bool isAsync = false)
		{
			try
			{
				if (IsConnected() == false)
				{
					if (OnError != null)
						OnError(SOCK_ERROR_SEND, "Socket was closed");

					return;
				}
					
				socket.BeginSend(msg, 0, msg.Length, SocketFlags.None,new AsyncCallback(OnSended), msg);
				if (OnSendEnded != null)
					OnSendEnded();

			}
			catch (SocketException se)
			{
				if (OnError != null)
					OnError(SOCK_ERROR_SEND, "Send error: " + se.Message);

				return;
			}
		}

		private void OnSended(IAsyncResult ar)
		{
			string msg = (string) ar.AsyncState;
			if (OnSendEnded != null)
				OnSendEnded();
		}


		public void Disconnect()
		{
			if (socket != null)
			{
				if (socket.Connected == true)
				{
					socket.Close();
				}
			}

			socket = null;

			if (OnDisconnect != null)
			{
				OnDisconnect();
			}
		}


		public bool IsConnected()
		{
			if (socket == null)
			{
				return false;
			}

			return socket.Connected;
		}

	}//class
}//namespace
