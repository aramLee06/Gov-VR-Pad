using System;

namespace VR.Connect.Protocol.Send
{
	class MoveAndRotateMessage : SendMessage
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


		public MoveAndRotateMessage ()
		{
		}

		public MoveAndRotateMessage(float move_x, float move_y, float rotate_x, float roate_y){
			this._moveX = move_x;
			this._moveY = move_y;
			this._rotateX = rotate_x;
			this._rotateY = roate_y;
		}

		public override byte[] Generate()
		{ //cmd, x, y
			byteList.Clear ();
			AddByte8 (2); //cmd
			AddByteFloat(moveX);
			AddByteFloat (moveY);
			AddByteFloat(rotateX);
			AddByteFloat (rotateY);
			return byteList.ToArray ();
		}
	}
}

