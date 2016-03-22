using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using DG.Tweening;

using VR.Connect;

public class RoomNumberWindow : MonoBehaviour {
	

	public TouchScreenKeyboardType keyboardType;
	public static bool qrCodeIsNull = true;
	public GameObject qrScannerButton;
	public GameObject numberPad;
	public GameObject eventSystem;
	public GameObject serverConnect;

	public static string qrString;

	//UXClientController clientController
	PadConnect m_PadConnect;

	public static int latest_errCode = -1;

	public Text noti;

	private Queue<int> CommandQueue;

	void Start () {
		//CommonUtil.ScreenSettingsPortrait();

		CommandQueue = new Queue<int> ();
		//numberPad.SetActive(false);

		//clientController = UXClientController.Instance;
		m_PadConnect = PadConnect.Instance;
		//clientController.OnConnected += OnConnected;
		//clientController.OnConnectFailed += OnConnectFailed;
		//clientController.OnJoinSucceeded += OnJoinSucceeded;
		//clientController.OnJoinFailed += OnJoinFailed;
		//clientController.OnDisconnected += OnDisconnected;
		m_PadConnect.OnConnectSucceed += OnConnected;
		m_PadConnect.OnConnectFailed += OnConnectFailed;
		m_PadConnect.OnBindSuccess += OnBindSuccess;
		m_PadConnect.OnBindFailed += OnBindFailed;

		m_PadConnect.Connect();
	}
	
	void Update () {
		if (CommandQueue.Count > 0) {
			int cmd = CommandQueue.Dequeue ();

			if (cmd == 0) {
				serverConnect.SetActive (false);
			} else if (cmd == 1) {
				OKPopUp.popUpType = OKPopUp.APPLICATION_QUIT;
				CommonUtil.InstantiateOKPopUp("Failed connect to Server");	
			}
		}
		//clientController.Run();
		m_PadConnect.Run();
	}

	void OnDestroy() {
//		if(clientController != null){
//			clientController.OnConnected -= OnConnected;
//			clientController.OnConnectFailed -= OnConnectFailed;
//			
//			clientController.OnJoinSucceeded -= OnJoinSucceeded;
//			clientController.OnJoinFailed -= OnJoinFailed;
//			clientController.OnDisconnected -= OnDisconnected;
//		}

		if (m_PadConnect != null) {
			m_PadConnect.OnConnectSucceed -= OnConnected;
			m_PadConnect.OnConnectFailed -= OnConnectFailed;
			m_PadConnect.OnBindSuccess -= OnBindSuccess;
			m_PadConnect.OnBindFailed -= OnBindFailed;
		}
	}


	public void OnConnectButtonUp(){
		eventSystem.SetActive(false);
		numberPad.SetActive(true);
	}

	void OnConnected(){
		CommandQueue.Enqueue (0);
	}
	
	void OnConnectFailed(){
		CommandQueue.Enqueue(1);
	}

	void OnDisconnected(){
		OKPopUp.popUpType = OKPopUp.APPLICATION_QUIT;
		//CommonUtil.InstantiateOKPopUp(commonLang.langList[5]);
	}


	void OnBindSuccess(){
		SceneManager.LoadScene ("Pad_Control");
	}

	void OnBindFailed(int code){
		if (code == 0) {
			CommonUtil.InstantiateOKPopUp("Wrong VR Uid");	
		}
	}

	
	void OnJoinFailed(int err){
		Debug.Log ("JOIN ERROR : " + err);
		if(err == 10001 || err == 20003){
			OKPopUp.popUpType = OKPopUp.POPUP_DESTROY;
			//CommonUtil.InstantiateOKPopUp(commonLang.langList[8]);
			return;
			
		}else if(err == 10002){
			OKPopUp.popUpType = OKPopUp.POPUP_DESTROY;
			//CommonUtil.InstantiateOKPopUp(commonLang.langList[14]);
			return;
			
		}else if (err == 10003 || err == 20001){
			Debug.Log("Max User");
			OKPopUp.popUpType = OKPopUp.POPUP_DESTROY;
			//CommonUtil.InstantiateOKPopUp(commonLang.langList[12] );
			return;
		}else if(err == 10004 ||  err == 20002){
			Debug.Log("Already Start");
			OKPopUp.popUpType = OKPopUp.POPUP_DESTROY;;
			//CommonUtil.InstantiateOKPopUp(commonLang.langList[13] );
			return;
		}
		else {
			OKPopUp.popUpType = OKPopUp.POPUP_DESTROY;
			//CommonUtil.InstantiateOKPopUp(commonLang.langList[6] );
		}
	}
}
