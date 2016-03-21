using System;

namespace VR.Connect.Protocol.Send
{
	class MoveMessage : SendMessage
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

		public MoveMessage ()
		{
		}

		public override byte[] Generate()
		{ //cmd, x, y
			byteList.Clear ();
			AddByte8 (3); //cmd
			AddByteFloat(axisX);
			AddByteFloat (axisY);
			return byteList.ToArray ();
		}
	}
}

