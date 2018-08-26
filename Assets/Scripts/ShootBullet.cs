using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShootBullet : Photon.MonoBehaviour
{

    [SerializeField] private AudioClip fire;
    [SerializeField] private AudioClip reload;

    private AudioSource audioSource;
    private GameObject sparkle;
    private GameObject generatedSparkle;
    private GameObject hitObj;
    private GameObject canvas;
    private GameObject gunScope;
    private Vector3 bulletHitPoint;
    private Vector3 screenCenter;
    private Image snipe;
    private ScreenUI screenUi;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private int _chargedBullet;
    [SerializeField] private int _maxBullet;
    [SerializeField] private int _bulletBox;
    [SerializeField] private float _interval;
    private bool _isShootable;
    private bool _isRelaoding;
    private bool _isZooming;

    public int BulletBox
    {
        set
        {
            this._bulletBox = value;
        }

        get { return this._bulletBox; }
    }

    public int ChargedBullet
    {
        set
        {
            this._chargedBullet = value;
        }

        get { return this._chargedBullet; }
    }

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas").gameObject;
        screenUi = canvas.GetComponent<ScreenUI>();
        gunScope = canvas.transform.Find("snipe").gameObject;
        snipe = gunScope.GetComponent<Image>();
        screenCenter = new Vector3(Screen.width / 2, Screen.height / 2);

        audioSource = GetComponent<AudioSource>();
        sparkle = (GameObject)Resources.Load("prefabs/Sparkle");


        _chargedBullet = 30;
        _maxBullet = 30;
        _bulletBox = 150;
        _isShootable = true;
        _isRelaoding = false;
        _isZooming = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.isMine)
        {
            return;
        }

        screenUi.DisplayText(BulletBox, ChargedBullet);

        if (Input.GetMouseButtonDown(1))
        {
            UseScope();
        }
        if (Input.GetMouseButton(0))
        {
            FireBullet();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("ReloadBullet");
        }
        _interval += Time.deltaTime;

        if (_interval <= 0.1f)
        {
            _isShootable = false;
        }
        else
        {
            _isShootable = true;
        }

        if (_isRelaoding == true)
        {
            _isShootable = false;
        }
        if (_chargedBullet == 0)
        {
            _isShootable = false;
        }
    }

    public void UseScope()
    {
        if (!_isZooming)
        {
            _isZooming = true;
            playerCamera.fieldOfView = 20;
            snipe.enabled = true;
        }
        else
        {
            _isZooming = false;
            playerCamera.fieldOfView = 60;
            snipe.enabled = false;
        }
    }

    public void FireBullet()
    {
        if (!_isShootable)
        {
            return;
        }
        _interval = 0f;
        _chargedBullet -= 1;

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        //射撃音
        ShootSound();
        //銃口に火花生成
        MakeSparkle();

        if (Physics.Raycast(ray, out hit))
        {
            bulletHitPoint = hit.point;
            hitObj = hit.collider.gameObject;
            generatedSparkle = Instantiate(sparkle, bulletHitPoint, Quaternion.identity);
            Destroy(generatedSparkle, 0.1f);

            if (hitObj.name == "pCube1")
            {
                hitObj.transform.parent.GetComponent<TargetController>().CountScore(hit.point);
                hitObj.transform.parent.GetComponent<TargetController>().MinusHP();

            }
            if (hitObj.tag == "Player")
            {
                PhotonView enemyPhotonView;
                enemyPhotonView = hitObj.GetComponent<PhotonView>();
                enemyPhotonView.RPC("MinusPlayerHp", PhotonTargets.All);
            }
        }
    }
    [PunRPC]
    public void MinusPlayerHp()
    {

        PlayerHpController playerHpCon = GetComponent<PlayerHpController>();
        playerHpCon.MinusHp();
    }

    IEnumerator ReloadBullet()
    {
        if (_bulletBox == 0)
        {
            yield break;
        }
        if (_chargedBullet == _maxBullet)
        {
            yield break;
        }
        _isRelaoding = true;
        int gap = _maxBullet - _chargedBullet;
        if (_bulletBox < (gap))
        {
            _chargedBullet += _bulletBox;
            _bulletBox = 0;
        }
        else
        {
            _chargedBullet = _maxBullet;
            _bulletBox -= gap;
        }
        audioSource.PlayOneShot(reload);
        yield return new WaitForSeconds(2f);
        _isRelaoding = false;
    }
    void ShootSound()
    {
        audioSource.PlayOneShot(fire);
    }
    void MakeSparkle()
    {
        generatedSparkle = Instantiate(sparkle, transform.position, Quaternion.FromToRotation(transform.position, bulletHitPoint));
        Destroy(generatedSparkle, 0.1f);
    }
}
