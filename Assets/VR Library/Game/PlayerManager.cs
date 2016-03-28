using UnityEngine;
using System.Collections;

namespace VR.Game {
	class PlayerManager {
		private PlayerManager instance = null;
		public PlayerManager Instance {
			get {
				if(instance == null){
					instance = new PlayerManager();
				}

				return instance;
			}
		}


	}
}
