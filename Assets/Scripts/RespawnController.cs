using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefs;
    [SerializeField] private ScreenUI screenUi;

    private GameObject bornPlayer;
    private Vector3 _firstPos;
    private GameObject childOfPlayer;
    private Camera camera;
    private bool _respawn;

    public bool Respawn
    {
        set
        {
            this._respawn = value;
        }
        get
        {
            return this._respawn;
        }

    }


    // Use this for initialization
    void Start()
    {
		_respawn = false;
        _firstPos = new Vector3(0, 1, 0);
        Invoke("Spawn", 3);

    }

    // Update is called once per frame
    void Update()
    {


		if (!_respawn)
        {
            return;
        }

        Invoke("Spawn", 3);
		_respawn = false;

    }


    void Spawn()
    {

        bornPlayer = PhotonNetwork.Instantiate(playerPrefs.name, _firstPos, Quaternion.identity, 0);
        screenUi.ShootBulCheck = true;

        childOfPlayer = bornPlayer.transform.Find("FirstPersonCharacter").gameObject;
        camera = childOfPlayer.GetComponent<Camera>();
        camera.enabled = true;
    }
}