using UnityEngine;
using System.Collections;

public class PadController : MonoBehaviour {

	public EasyJoystick MoveJoyStick;
	public EasyJoystick RotateJoyStick;

	private Vector2 beforeAxis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		beforeAxis = RotateJoyStick.JoystickAxis;

	}

	public void OnFire() {
		if (beforeAxis.x == 0 && beforeAxis.y == 0) {
			Debug.Log ("click");
		}
	}
}
