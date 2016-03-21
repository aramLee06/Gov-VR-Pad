using System;

namespace VR.Connect.Protocol.Send
{
	class RotateMessage : SendMessage
	{
		private int _axisX;
		public int axisX {
			get{
				return axisX;
			}
			set{
				axisX = value;
			}
		}
		private int _axisY;
		public int axisY {
			get{
				return axisY;
			}
			set{
				axisY = value;
			}
		}
		public RotateMessage ()
		{
		}

		public override byte[] Generate()
		{ //cmd, x, y
			byteList.Clear ();
			AddByte8 (4); //cmd
			AddByteFloat(axisX);
			AddByteFloat (axisY);
			return byteList.ToArray ();
		}
	}
}

