using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using VR.Connect;


public class TEST : MonoBehaviour {
	VRConnect vr;
	public Text text;

	public Text moveText;
	public Text rotateText;
	public Text fireText;

	// Use this for initialization
	void Start () {
		vr = new VRConnect ();
		vr.OnBindSuccess += OnBindSuccess;
		vr.OnControl += OnControl;
		vr.OnShoot += OnShoot;
		vr.OnControlType += OnControlType;
		vr.Connect ();

		text.text = vr.uid.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		vr.Run ();
	}

	public void btn_join(){
		vr.Join ();
	}

	public void OnBindSuccess(){
		Debug.Log ("Bind Success!!!!!!!!");
	}

	public void OnControl(Vector2 move, Vector2 rotate){
		moveText.text = "Move, x : " + move.x + ", y : " + move.y;
		rotateText.text = "Rotate, x : " + rotate.x + ", y : " + rotate.y;

	}

	public void OnControlType(MoveType move, RotateType rotate){
		Debug.Log (move.ToString () + ", " + rotate.ToString());
	}

	int fireCount = 0;
	public void OnShoot(Vector3 position, Vector3 velocity){
		fireText.text = "FireCount : " + (++fireCount);
	}
}
