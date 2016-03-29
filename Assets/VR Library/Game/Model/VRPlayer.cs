using UnityEngine;
using System.Collections;

namespace VR.Game {
	public enum UnitType {
		RedDrone = 2, RedTank = 0, BlueDrone = 3, BlueTank = 1
	}

	public enum PlayerStatus {
		Wait, Live, Death
	}

	class VRPlayer {
		public VRPlayer(int uid, int HP=11) {
			this._uid = uid;
			this.hp = HP;
		}

		public UnitType Unit{ get; set;}

		public PlayerStatus Status { get; set;}

		private int _uid;
		public int uid {
			get {
				return _uid;
			}
		}

		private int hp;
		public int HP { 
			get {
				return hp;
			}
			set {
				hp = value;
				if (hp <= 0) {
					hp = 0;
					this.Status = PlayerStatus.Death;
				}
			}
		}

		public bool isCurrent {get; set;}

		public Vector3 Position {get; set;}

		public Vector3 Move {get; set;}

		public Vector3 Rotation {get; set;}
	}
}