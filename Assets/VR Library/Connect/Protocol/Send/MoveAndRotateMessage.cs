using System;

namespace VR.Connect.Protocol.Send
{
	class MoveAndRotateMessage : SendMessage
	{
		private int _moveX;
		public int moveX {
			get{
				return moveX;
			}
			set{
				moveX = value;
			}
		}
		private int _moveY;
		public int moveY {
			get{
				return moveY;
			}
			set{
				moveY = value;
			}
		}
		private int _rotateX;
		public int rotateX {
			get{
				return rotateX;
			}
			set{
				rotateX = value;
			}
		}
		private int _rotateY;
		public int rotateY {
			get{
				return rotateY;
			}
			set{
				rotateY = value;
			}
		}


		public MoveAndRotateMessage ()
		{
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

