using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefs;
    [SerializeField] private GameObject bornPlayer;
    [SerializeField] private ScreenUI screenUi;
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
        Respawn = false;
        _firstPos = new Vector3(0, 1, 0);
        Invoke("Spawn", 3);

    }

    // Update is called once per frame
    void Update()
    {


        if (!Respawn)
        {
            return;
        }

        Invoke("Spawn", 3);
        Respawn = false;

    }


    public void Spawn()
    {

        bornPlayer = PhotonNetwork.Instantiate(playerPrefs.name, _firstPos, Quaternion.identity, 0);
        screenUi._shootBulCheck = true;

        childOfPlayer = bornPlayer.transform.Find("FirstPersonCharacter").gameObject;
        camera = childOfPlayer.GetComponent<Camera>();
        camera.enabled = true;
    }
}