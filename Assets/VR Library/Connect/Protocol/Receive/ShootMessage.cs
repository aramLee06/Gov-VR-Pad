using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class ShootMessage : ReceiveMessage
	{
		private float _positionX;
		public float positionX {
			get{
				return _positionX;
			}
			set{
				_positionX = value;
			}
		}
		private float _positionY;
		public float positionY {
			get{
				return _positionY;
			}
			set{
				_positionY = value;
			}
		}
		private float _positionZ;
		public float positionZ {
			get{
				return _positionZ;
			}
			set{
				_positionZ = value;
			}
		}
		private float _velocityX;
		public float velocityX {
			get{
				return _velocityX;
			}
			set{
				_velocityX = value;
			}
		}
		private float _velocityY;
		public float velocityY {
			get{
				return _velocityY;
			}
			set{
				_velocityY = value;
			}
		}
		private float _velocityZ;
		public float velocityZ {
			get{
				return _velocityZ;
			}
			set{
				_velocityZ = value;
			}
		}

		public ShootMessage (List<byte> data)
		{	
			byte[] position_x_arr = { data [1], data [2], data [3], data [4] };
			byte[] position_y_arr = { data [5], data [6], data [7], data [8] };
			byte[] position_z_arr = { data [9], data [10], data [11], data [12] };
			byte[] velocity_x_arr = { data [13], data [14], data [15], data [16] };
			byte[] velocity_y_arr = { data [17], data [18], data [19], data [20] };
			byte[] velocity_z_arr = { data [21], data [22], data [23], data [24] };

			_positionX = BitConverter.ToSingle (position_x_arr, 0);
			_positionY = BitConverter.ToSingle (position_y_arr, 0);
			_positionZ = BitConverter.ToSingle (position_z_arr, 0);
			_velocityX = BitConverter.ToSingle (velocity_x_arr, 0);
			_velocityY = BitConverter.ToSingle (velocity_y_arr, 0);
			_velocityZ = BitConverter.ToSingle (velocity_z_arr, 0);

			data.RemoveRange (0, 25);
		}
	}
}

