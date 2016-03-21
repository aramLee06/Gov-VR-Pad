using UnityEngine;
using System.Collections;

using VR.Connect;


public class TEST : MonoBehaviour {
	PadConnect vr;
	// Use this for initialization
	void Start () {
		vr = new PadConnect ();
		vr.Connect ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void btn_join(){
		vr.Join (12345);
	}
}
