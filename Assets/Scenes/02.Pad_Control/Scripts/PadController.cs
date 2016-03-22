using UnityEngine;
using System.Collections;

using VR.Connect;

public class PadController : MonoBehaviour {

	public EasyJoystick MoveJoyStick;
	public EasyJoystick RotateJoyStick;

	private Vector2 beforeAxis;

	private PadConnect m_PadConnect;

	// Use this for initialization
	void Start () {
		//Screen.orientation = ScreenOrientation.LandscapeLeft;
		m_PadConnect = PadConnect.Instance;
		StartCoroutine("SendMoveRotate");
	}
	
	// Update is called once per frame
	void Update () {
		beforeAxis = RotateJoyStick.JoystickAxis;
	}

	private IEnumerator SendMoveRotate()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.4f); // wait half a second
			Vector2 move = MoveJoyStick.JoystickAxis;
			Vector2 rotate = RotateJoyStick.JoystickAxis;
			m_PadConnect.SendControlData (move.x, move.y, rotate.x, rotate.y);
			// do things
		}
	}

	public void OnFire() {
		if (beforeAxis.x == 0 && beforeAxis.y == 0) {
			m_PadConnect.SendFire ();
			Handheld.Vibrate();
		}
	}
}

