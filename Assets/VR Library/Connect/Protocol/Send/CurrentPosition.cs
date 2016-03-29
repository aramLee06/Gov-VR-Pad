using System;

namespace VR.Connect.Protocol.Send
{
	class CurrentPosition : SendMessage
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
		private float _moveZ;
		public float moveZ {
			get{
				return _moveZ;
			}
			set{
				_moveZ = value;
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
		private float _rotateZ;
		public float rotateZ {
			get{
				return _rotateZ;
			}
			set{
				_rotateZ = value;
			}
		}
		private float _currentX;
		public float currentX {
			get{
				return _currentX;
			}
			set{
				_currentX = value;
			}
		}
		private float _currentY;
		public float currentY {
			get{
				return _currentY;
			}
			set{
				_currentY = value;
			}
		}
		private float _currentZ;
		public float currentZ {
			get{
				return _currentZ;
			}
			set{
				_currentZ = value;
			}
		}


		public CurrentPosition ()
		{
		}
		public CurrentPosition (float move_x, float move_y, float move_z, float rotate_x, float rotate_y, float rotate_z, float current_x, float current_y, float current_z ){
			this._moveX = move_x;
			this._moveY = move_y;
			this._moveZ = move_z;
			this._rotateX = rotate_x;
			this._rotateY = rotate_y;
			this._rotateY = rotate_z;
			this._currentX = current_x;
			this._currentY = current_y;
			this._currentZ = current_z;
		}

		public override byte[] Generate()
		{ 
			byteList.Clear ();
			AddByte8 (6); //cmd
			AddByteFloat(moveX);
			AddByteFloat (moveY);
			AddByteFloat (moveZ);
			AddByteFloat(rotateX);
			AddByteFloat (rotateY);
			AddByteFloat (rotateZ);
			AddByteFloat (currentX);
			AddByteFloat (currentY);
			AddByteFloat (currentZ);
			return byteList.ToArray ();
		}

	}
}

