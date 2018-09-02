using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpController : Photon.MonoBehaviour {
	
	private RespawnController respawnCon;
	private int _hitPoint;

	// Use this for initialization
	void Start () {
		if (!photonView.isMine) {
			return;
		}
		_hitPoint = 5;
		GameObject RespawnConObj = GameObject.Find ("RespawnController");
		respawnCon = RespawnConObj.GetComponent<RespawnController> (); 
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void MinusHp(){
		if(!photonView.isMine){
			return;
		}
		_hitPoint--;
		Debug.Log (_hitPoint);

		if (_hitPoint <= 0) {
			
		
			_hitPoint = 5;

			respawnCon.Respawn = true;

			PhotonNetwork.Destroy(gameObject);
		}
	}
}
