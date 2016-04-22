using UnityEngine;
using System.Collections;

namespace VR.Game {
	public enum UnitType {
		RedDrone = 2, RedTank = 3, BlueDrone = 1, BlueTank = 0
	}

	public enum PlayerStatus {
		Wait, Live, Death
	}

	class VRPlayer {
		public VRPlayer(int uid, int HP=11) {
			this._uid = uid;
			this.hp = HP;

			this.Position = new Vector3 ();
			this.Move = new Vector3 ();
			this.Rotation = new Vector3 ();
		}

		public VRPlayer(int uid, int HP, Vector3 Pos, Vector3 Move, Vector3 Rot){
			this._uid = uid;
			this.HP = HP;
			this.Position = Pos;
			this.Move = Move;
			this.Rotation = Rot;
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