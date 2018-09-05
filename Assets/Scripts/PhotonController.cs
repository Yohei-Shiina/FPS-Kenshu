using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PhotonController : Photon.MonoBehaviour
{

	private string _roomName;
	private RoomOptions roomOptions; 
	private RoomInfo[] roomInfo;
	private GameObject roomtextObj;
	private string _roomText;
	private string _roomNameforJoin;
	private bool _isConnected;
	private Text stateText;
	[SerializeField]private GameObject stateTextObj;
	[SerializeField]private GameObject roomPanel;
	[SerializeField]private InputField lobbyInputField;
	[SerializeField]private Text[] roomPanelText;
	[SerializeField]private GameObject[] roomPrefab;


    // Use this for initialization
    void Start()
    {
		PhotonNetwork.ConnectUsingSettings("0.1");
		stateText = stateTextObj.GetComponent<Text>();
		stateText.text = "Lobby";
		lobbyInputField = lobbyInputField.GetComponent<InputField> ();
		print(lobbyInputField);
		roomOptions = new RoomOptions ();
		roomOptions.MaxPlayers = 4;
		roomOptions.IsOpen = true;
		roomOptions.IsVisible = true;
		_isConnected = false;

    }

    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました");

	}

	public void MakeRoom(){
		_roomName = lobbyInputField.text;
		print (_roomName);
		PhotonNetwork.JoinOrCreateRoom (_roomName, roomOptions, null);

	}



    void OnPhotonCreateRoomFailed()
    {
        Debug.Log("ルーム入室に失敗しました");
        PhotonNetwork.CreateRoom(null);
    }


	void OnReceivedRoomListUpdate(){
		Text roomText;
		Text playerText;

		roomInfo = PhotonNetwork.GetRoomList ();

		for (int i = 0; i < roomInfo.Length; i++) {

			roomPrefab [i].SetActive (true);

			roomText = roomPrefab [i].transform.Find ("RoomText").GetComponent<Text> ();
			playerText = roomPrefab [i].transform.Find ("PlayerText").GetComponent<Text> ();
		
			roomText.text = roomInfo [i].Name;
			playerText.text = "Player : " + roomInfo [i].PlayerCount;

	
		}
	}

    void OnJoinedRoom()
    {
        
		Debug.Log("ルームに入室しました");

		roomPanel.SetActive (true);
		_isConnected = true;
		print (roomInfo.Length);
		//RoomPanelの部屋名の表示
		roomPanelText [0].text = "Room Name : " + PhotonNetwork.room.Name;
    }

    // Update is called once per frame
    void Update()
	{
		bool judgeRoom;
		bool judgeSearch;

		if (_isConnected) {
			stateText.text = "Room";
		} else {
			stateText.text = "Lobby";
		}

		if (PhotonNetwork.playerList.Length < roomOptions.MaxPlayers) {
			judgeRoom = true;
			judgeSearch = true;
		} else {
			judgeRoom = false;
			judgeSearch = false;
		}
		roomPanelText [1].text = "Player : " + PhotonNetwork.playerList.Length;
		roomPanelText [2].text = "IsOpen : " + judgeRoom;
		roomPanelText [3].text = "IsVisible : " + judgeSearch;
	}


	public void GameStart()
	{
		SceneManager.LoadScene ("Main");
	}


}