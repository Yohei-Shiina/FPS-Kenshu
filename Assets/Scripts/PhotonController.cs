using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonController : Photon.MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("ルーム入室に失敗しました");
        PhotonNetwork.CreateRoom(null);
    }

    void OnJoinedRoom()
    {
        Debug.Log("ルームに入室しました");     

    }
	   

    // Update is called once per frame
    void Update()
    {
		
    }
}
