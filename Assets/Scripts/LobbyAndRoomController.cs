using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LobbyAndRoomController : MonoBehaviour {
	[SerializeField] GameObject lobbyPanel;
	[SerializeField] GameObject roomPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void OpenRoomPanel(){

		lobbyPanel.SetActive (false);
		roomPanel.SetActive (true);

	}

	public void GameStart(){ 	//Roomの開始するボタン
		SceneManager.LoadScene ("Main");

	}
}
