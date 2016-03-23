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
		StartCoroutine("SendMoveRotate"); //멀티 스레드처럼 동시처리 가능
	} 
	
	// Update is called once per frame
	void Update () {
		beforeAxis = RotateJoyStick.JoystickAxis;
	}
	//IEumerator : 제네릭이 아닌 컬렉션을 단순하게 반복할 수 있도록 지원합니다.
	private IEnumerator SendMoveRotate()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.4f); // wait half a second
			Vector2 move = MoveJoyStick.JoystickAxis;
			Vector2 rotate = RotateJoyStick.JoystickAxis;
			m_PadConnect.SendControlData (move.x, move.y, rotate.x, rotate.y); //얘를 계속 보내준다.
			// do things
		}
	}

	/// <summary>
	/// Raises the fire event.
	/// When press fire btn on pad
	/// </summary>
	public void OnFire() {
		if (beforeAxis.x == 0 && beforeAxis.y == 0) { //터치 확인 //드래그는 X
			m_PadConnect.SendFire (); 
			Handheld.Vibrate(); //핸드폰 진동
		}
	}
}

