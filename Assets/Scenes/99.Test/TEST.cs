using UnityEngine;
using System.Collections;

using VR.Connect;


public class TEST : MonoBehaviour {
	PadConnect vr;
	// Use this for initialization
	void Start () {
		vr = new PadConnect ();
		vr.OnBindSuccess += OnBindSuccess;
		vr.Connect ();
	}
	
	// Update is called once per frame
	void Update () {
		vr.Run ();
	}

	public void btn_join(){
		vr.Join (47971);
	}

	public void OnBindSuccess(){
		Debug.Log ("Bind Success!!!!!!!!");
	}
}
