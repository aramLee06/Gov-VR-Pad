using System;

namespace VR.Connect.Protocol.Send
{
	class ShootMessage : SendMessage
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

		public ShootMessage ()
		{
		}

		public ShootMessage(float position_x, float position_y, float position_z, float velocity_x, float velocity_y, float velocity_z){
			this._positionX = position_x;
			this._positionY = position_y;
			this._positionZ = position_z;
			this._velocityX = velocity_x;
			this._velocityY = velocity_y;
			this._velocityZ = velocity_z;
		}


		public override byte[] Generate()
		{ 
			byteList.Clear ();
			AddByte8 (3); //cmd
			AddByteFloat(positionX);
			AddByteFloat(positionY);
			AddByteFloat(positionZ);
			AddByteFloat(velocityX);
			AddByteFloat(velocityY);
			AddByteFloat(velocityZ);
			return byteList.ToArray ();
		}
	}
}

