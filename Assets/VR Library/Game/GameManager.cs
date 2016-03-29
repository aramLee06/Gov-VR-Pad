using UnityEngine;
using System.Collections;

using VR.Connect;


namespace VR.Game {
	class GameManager : MonoBehaviour {
		#region Member Field
		public VRConnect vrConnect;

		public PlayerManager playerManager;
		#endregion


		#region Delegate & Event
		public delegate void BaseEventHandler ();
		public delegate void ReceiveUidEventHandler (VRPlayer player);
		public delegate void VlaueHandler (int value);
		public delegate void GameEndHanler (float value);
		public delegate void MoveAndRotateHandler (Vector2 move, Vector2 rotate);
		public delegate void ShootHandler (Vector3 position, Vector3 velocity);
		public delegate void SoldOutHandler (VRPlayer player, UnitType type);

		/// <summary>
		/// Move and rotate enum handler.
		/// </summary>
		public delegate void MoveAndRotateEnumHandler (MoveType move, RotateType rotate);


		/// <summary>
		/// Occurs when on shoot message.
		/// 누군가들에의해 발사된 대포 위치를 broad cast 받은것.
		/// </summary>
		public event ShootHandler OnShoot;

		/// <summary>
		/// Occurs when on game start.
		/// </summary>
		public event BaseEventHandler OnGameStart;

		/// <summary>
		/// Occurs when on game end.
		/// 플레이한 시간 보낸다..
		/// </summary>
		public event GameEndHanler OnGameEnd;

		/// <summary>
		/// 미정
		/// </summary>
		public event BaseEventHandler OnMap;

		public event VlaueHandler OnGameCount;

		/// <summary>
		/// Occurs when on attacked. (대포 맞았을 때)
		/// </summary>
		public event BaseEventHandler OnAttacked;

		/// <summary>
		/// 패드에서의 동작 :: move, rotate(aim움직임)
		/// </summary>
		public event MoveAndRotateHandler OnControl;

		/// <summary>
		/// Occurs when on death.
		/// 게임중 다른 사람의 죽음
		/// </summary>
		public event ReceiveUidEventHandler OnDeath;

		/// <summary>
		/// Occurs when on control type.
		/// </summary>
		public event MoveAndRotateEnumHandler OnControlType;

		/// <summary>
		/// Occurs when on sold out.
		/// 로비에서 다른 사람이 유닛 고른경우 오는 메세지
		/// </summary>
		public event SoldOutHandler OnSoldOut;

		#endregion

		// Use this for initialization
		void Start () {
			vrConnect = VRConnect.Instance;
			playerManager = PlayerManager.Instance;

			vrConnect.OnShoot += OnShoot_proxy;
			vrConnect.OnGameStart += OnGameStart_proxy;
			vrConnect.OnGameEnd += OnGameEnd_proxy;
			vrConnect.OnGameCount += OnGameCount_proxy;
			vrConnect.OnAttacked += OnAttacked_proxy;
			vrConnect.OnControl += OnControl_proxy;
			vrConnect.OnDeath += OnDeath_proxy;
			vrConnect.OnControlType += OnControlType_proxy;
			vrConnect.OnSoldOut += OnSoldOut_proxy;
		}

		// Update is called once per frame
		void Update () {
			vrConnect.Run ();
		}

		void OnDestroy(){
			vrConnect.OnShoot -= OnShoot_proxy;
			vrConnect.OnGameStart -= OnGameStart_proxy;
			vrConnect.OnGameEnd -= OnGameEnd_proxy;
			vrConnect.OnGameCount -= OnGameCount_proxy;
			vrConnect.OnAttacked -= OnAttacked_proxy;
			vrConnect.OnControl -= OnControl_proxy;
			vrConnect.OnDeath -= OnDeath_proxy;
			vrConnect.OnControlType -= OnControlType_proxy;
			vrConnect.OnSoldOut -= OnSoldOut_proxy;
		}

		#region Event Implements
		void OnShoot_proxy(Vector3 position, Vector3 velocity){
			if (OnShoot != null) {
				OnShoot (position, velocity);
			}
		}

		void OnGameStart_proxy() {
			if (OnGameStart != null) {
				OnGameStart ();
			}
		}

		void OnGameEnd_proxy(float time) {
			if (OnGameEnd != null) {
				OnGameEnd (time);
			}
		}

		void OnGameCount_proxy(int value) {
			if (OnGameCount != null) {
				OnGameCount (value);
			}
		}

		void OnAttacked_proxy() {
			if (OnAttacked != null) {
				OnAttacked ();
			}
		}

		void OnControl_proxy(Vector2 move, Vector2 rotate) {
			if (OnControl != null) {
				OnControl (move, rotate);
			}
		}

		void OnDeath_proxy(int uid) {
			if (OnDeath != null) {
				VRPlayer player = playerManager.GetPlayer (uid);
				OnDeath (player);
			}
		}

		void OnControlType_proxy(MoveType move, RotateType rotate) {
			if (OnControlType != null) {
				OnControlType (move, rotate);
			}
		}

		void OnSoldOut_proxy(int uid, int unitnum) {
			if (OnSoldOut != null) {
				VRPlayer player = playerManager.GetPlayer (uid);
				UnitType type = (UnitType)unitnum;

				OnSoldOut (player, type);
			}
		}

		#endregion

		#region SendMessage
		public void SendHit(VRPlayer player) {
			vrConnect.SendHit (player.uid);
		}

		public void SendDeath(){
			vrConnect.SendDeath ();
		}

		public void SendReady(){
			vrConnect.SendReady ((int)playerManager.CurrentPlayer.Unit);
		}
		#endregion
	}

}