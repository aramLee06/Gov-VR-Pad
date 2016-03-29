using UnityEngine;
using System.Collections;

namespace VR.Game {
	public enum UnitType {
		RedDrone, RedTank, BlueDrone, BlueTank
	}

	public enum PlayerStatus {
		
	}

	class VRPlayer {
		public VRPlayer(int uid) {
			this._uid = uid;
		}

		public UnitType Unit{ get; set;}

		private int _uid;
		public int uid {
			get {
				return uid;
			}
		}

		public Vector3 Position {get; set;}

		public Vector3 Move {get; set;}

		public Vector3 Rotation {get; set;}
	}
}