using System;

/// <summary>
/// Gun fire message.
/// pad fire버튼 터치시 서버로 보내는 메세지
/// </summary>
namespace VR.Connect.Protocol.Send
{
	class DeathStopMessage : SendMessage
	{

		private float _altitude;
		public float altitude {
			get {
				return _altitude;
			}
			set {
				_altitude = value;
			}
		}

		public DeathStopMessage ()
		{
		}

		public DeathStopMessage(float alil){
			this._altitude = altitude;
		}

		public override byte[] Generate(){
			byteList.Clear ();
			AddByte8 (12); //cmd
			AddByteFloat(altitude);
			return byteList.ToArray ();
		}
	}
}
