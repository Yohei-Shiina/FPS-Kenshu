using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour {

	[SerializeField] private GameObject playerPrefs;
	[SerializeField] private GameObject bornPlayer;
	[SerializeField] private ScreenUI screenUi;
	private Vector3 _firstPos;
	private GameObject childOfPlayer;
	private Camera camera;
	// Use this for initialization
	void Start () {
		
		_firstPos = new Vector3 (0, 1, 0);
		Invoke("Spawn",3);

	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Spawn(){
		
		bornPlayer = PhotonNetwork.Instantiate (playerPrefs.name, _firstPos, Quaternion.identity, 0);
		screenUi._shootBulCheck = true;

		childOfPlayer = bornPlayer.transform.Find("FirstPersonCharacter").gameObject;
		camera = childOfPlayer.GetComponent<Camera>();
		camera.enabled = true;

	}
	public void Die(){
		bornPlayer.SetActive (false);

		Invoke ("Respawn", 3);
	}

	public void Respawn(){
		bornPlayer.SetActive (true);

		ShootBullet shootBullet = bornPlayer.GetComponent<ShootBullet> ();
		//銃弾再補充
		shootBullet.BulletBox = 150;
		shootBullet.ChargedBullet = 30;
		//初期位置にリスポン
		bornPlayer.transform.position = _firstPos;
	}
}