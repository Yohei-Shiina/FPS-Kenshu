using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour {

	private string _roomName;
	private GameObject parentObj;
	private GameObject childObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EnterRoom()
	{
		parentObj = transform.parent.gameObject;
		childObj = parentObj.transform.Find ("RoomText").gameObject;
		_roomName = childObj.GetComponent<Text> ().text;
		print (_roomName);

		PhotonNetwork.JoinRoom (_roomName);

	}
}
