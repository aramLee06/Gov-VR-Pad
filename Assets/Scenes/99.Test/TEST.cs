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
		vr.OnFire += OnFire;

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

	int fireCount = 0;
	public void OnFire(){
		fireText.text = "FireCount : " + (++fireCount);
	}
}
