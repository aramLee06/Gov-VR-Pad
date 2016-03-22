using System;
using System.Collections.Generic;

namespace VR.Connect.Protocol.Receive
{
	class MoveAndRotateMessage : ReceiveMessage
	{
		private float _moveX;
		public float moveX {
			get{
				return _moveX;
			}
			set{
				_moveX = value;
			}
		}
		private float _moveY;
		public float moveY {
			get{
				return _moveY;
			}
			set{
				_moveY = value;
			}
		}
		private float _rotateX;
		public float rotateX {
			get{
				return _rotateX;
			}
			set{
				_rotateX = value;
			}
		}
		private float _rotateY;
		public float rotateY {
			get{
				return _rotateY;
			}
			set{
				_rotateY = value;
			}
		}

		public MoveAndRotateMessage (List<byte> data)
		{
			byte[] move_x_arr = { data [1], data [2], data [3], data [4] };
			byte[] move_y_arr = { data [5], data [6], data [7], data [8] };
			byte[] rotate_x_arr = { data [9], data [10], data [11], data [12] };
			byte[] rotate_y_arr = { data [13], data [14], data [15], data [16] };

			_moveX = BitConverter.ToSingle (move_x_arr, 0);
			_moveY = BitConverter.ToSingle (move_y_arr, 0);
			_rotateX = BitConverter.ToSingle (rotate_x_arr, 0);
			_rotateY = BitConverter.ToSingle (rotate_y_arr, 0);

			data.RemoveRange (0, 17);
		}
	}
}

