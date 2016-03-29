using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VR.Game {
	class PlayerManager {
		private static PlayerManager instance = null;
		public static PlayerManager Instance {
			get {
				if(instance == null){
					instance = new PlayerManager();
				}

				return instance;
			}
		}

		private VRPlayer currentPlayer = null;
		public VRPlayer CurrentPlayer{
			get {
				return currentPlayer;
			}
			set {
				this.currentPlayer = value;
			}
		}

		private Dictionary<int, VRPlayer> m_PlayerList;

		public PlayerManager () {
			this.m_PlayerList = new Dictionary<int, VRPlayer> ();
		}

		/// <summary>
		/// Adds the player.
		/// </summary>
		/// <param name="player">Player.</param>
		public void AddPlayer(VRPlayer player){
			if (player.uid > 0) {
				m_PlayerList.Add (player.uid, player);
			}
		}

		/// <summary>
		/// Gets the player.
		/// </summary>
		/// <returns>The player.</returns>
		/// <param name="uid">Uid.</param>
		public VRPlayer GetPlayer(int uid) {
			VRPlayer player = m_PlayerList [uid];
			return player;
		}

		/// <summary>
		/// Gets the type of the player from unit.
		/// </summary>
		/// <returns>The player from unit type.</returns>
		/// <param name="type">Type.</param>
		public VRPlayer GetPlayerFromUnitType(UnitType type){
			VRPlayer player = null;
			foreach (VRPlayer p in m_PlayerList.Values) {
				if (p.Unit == type) {
					player = p;
					break;
				}
			}

			return player;
		}

		/// <summary>
		/// Gets the player list.
		/// </summary>
		/// <returns>The player list.</returns>
		public List<VRPlayer> GetPlayerList(){
			List<VRPlayer> list = new List<VRPlayer> ();
			foreach (VRPlayer p in m_PlayerList.Values) {
				list.Add (p);
			}

			return list;
		}
	}
}
