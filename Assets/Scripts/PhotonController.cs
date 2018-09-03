using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotonController : Photon.MonoBehaviour
{
	private string _roomName;
	private LobbyAndRoomController lobbyAndRoomController;
	[SerializeField]private GameObject lRController;
	[SerializeField]private InputField lobbyInputField;

    // Use this for initialization
    void Start()
    {
		lobbyInputField = lobbyInputField.GetComponent<InputField> ();
		lobbyAndRoomController = lRController.GetComponent<LobbyAndRoomController>();
        PhotonNetwork.ConnectUsingSettings("0.1");	//自動ロビーログイン

    }

    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました");

    
	}

	public void MakeRoom(){ //ルーム作成
		print("呼ばれた");
		//変数にROおｍ名を代入
		_roomName = lobbyInputField.text;
		//Roomを作成
		//_roomNameがnullならreturn
		if (_roomName == "") {
			return;
		}
		Debug.Log (_roomName);
		PhotonNetwork.CreateRoom (_roomName);
		　//入室画面表示
		lobbyAndRoomController.OpenRoomPanel();
	}

    void OnPhotonCreateRoomFailed()//作成失敗
    {
        Debug.Log("ルーム入室に失敗しました");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom() //ルーム入室成功時よびだし
    {
        Debug.Log("ルームに入室しました");

    }


    // Update is called once per frame
    void Update()
    {
		
    }
}