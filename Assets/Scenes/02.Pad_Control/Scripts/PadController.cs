using UnityEngine;
using System.Collections;

using VR.Connect;

public class PadController : MonoBehaviour {

	public EasyJoystick MoveJoyStick;
	public EasyJoystick RotateJoyStick;

	private Vector2 beforeAxis;
	private Vector2 prevMove;
	private Vector2 prevRotate;

	private PadConnect m_PadConnect;

	private bool fireCheck = true;
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
	/// <summary>
	/// Sends the move, rotate.
	/// 0.4초 마다 move joy stick, rotate joy stick의 움직임을 읽고 서버로 보낸다. 
	/// </summary>
	/// <returns>The move rotate.</returns>
	private IEnumerator SendMoveRotate()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.4f); // wait half a second
			Vector2 move = MoveJoyStick.JoystickAxis;
			Vector2 rotate = RotateJoyStick.JoystickAxis;

			if(prevMove!=move || prevRotate!=rotate){
				m_PadConnect.SendControlData (move.x, move.y, rotate.x, rotate.y); //얘를 계속 보내준다.
				prevMove = move;
				prevRotate = rotate;
			}
			Debug.Log ("SendMoveRotate");
			// do things
		}
	}

	/// <summary>
	/// Raises the fire event.
	/// When press fire btn on pad
	/// </summary>
	public void OnFire() {
		if (fireCheck) {
			if (beforeAxis.x == 0 && beforeAxis.y == 0) { //터치 확인 //드래그는 X
				m_PadConnect.SendFire (); 
				Handheld.Vibrate (); //핸드폰 진동
			}
			StartCoroutine (Fireable ());
		}
	}

	IEnumerator Fireable() {
		fireCheck = false;

		yield return new WaitForSeconds(4.0f); 
		fireCheck = true;
	}

	/// <summary>
	/// Raises the emergency stop event.
	/// when press emergency btn on pad
	/// </summary>
	public void OnEmergencyStop() {
		
		StopCoroutine (SendMoveRotate ());
		MoveJoyStick.gameObject.SetActive (false);
		RotateJoyStick.gameObject.SetActive (false);
		m_PadConnect.Emergency ();
		Debug.Log ("Emergency");
	}

	public void OnDeathStop() {
		StopCoroutine (SendMoveRotate ());
		MoveJoyStick.gameObject.SetActive (false);
		RotateJoyStick.gameObject.SetActive (false);
		StartCoroutine (SendDeathStop ());
	}

	private IEnumerator SendDeathStop() {
		m_PadConnect.DeathStop ();

		yield return new WaitForSeconds(1.0f);
		m_PadConnect.Emergency ();
	}

}

