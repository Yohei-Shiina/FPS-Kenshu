using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShootBullet : MonoBehaviour {

	    [SerializeField]private AudioClip fire; 
	    [SerializeField]private AudioClip reload;

	    private AudioSource audioSource;
	    private GameObject sparkle;
	    private GameObject generatedSparkle;
	    private Vector3 bulletHitPoint;
	    private Vector3 screenCenter;
	    private GameObject hitObj;

	    [SerializeField]private Image gunScope;
	    [SerializeField]private Camera playerCamera;

	    [SerializeField]private int _chargedBullet;
	    [SerializeField]private int _maxBullet;
	    [SerializeField]private int _bulletBox;
	    [SerializeField]private float _interval;
	    private bool _isShootable;
	    private bool _isRelaoding;
	    private bool _isZooming;

	    public int BulletBox {
		        get{ return this._bulletBox; }
		    }
	    public int ChargedBullet {
		        get{ return this._chargedBullet; }
		    }

	    // Use this for initialization
	    void Start () {


		        audioSource = GetComponent<AudioSource> ();
		screenCenter = new Vector3 (Screen.width / 2, Screen.height / 2);
		sparkle = (GameObject)Resources.Load("prefabs/Sparkle");
		_isZooming = false;

		_chargedBullet = 30;
		_maxBullet = 30;
		_bulletBox = 150;
		_isShootable = true;
		_isRelaoding = false;
	}

	    // Update is called once per frame
	    void Update () {
		if (Input.GetMouseButtonDown (1)) {
			UseScope ();
		}
		if (Input.GetMouseButton (0)) {
			FireBullet ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			StartCoroutine ("ReloadBullet");
		}
		_interval += Time.deltaTime;

		if (_interval <= 0.1f) {
			_isShootable = false;
		} else {
			_isShootable = true;
		}

		if (_isRelaoding == true) {
			_isShootable = false;
		}
		if (_chargedBullet == 0) {
			_isShootable = false;
		}
	}

	public void UseScope(){
		if (!_isZooming) {
			_isZooming = true;
			playerCamera.fieldOfView = 20;
			gunScope.enabled = true;
		} else {
			_isZooming = false;
			playerCamera.fieldOfView = 60;
			gunScope.enabled = false;
		}
	}

	public void FireBullet(){
		if (!_isShootable) {
			return;
		}
		_interval = 0f;
		_chargedBullet -= 1;

		Ray ray = Camera.main.ScreenPointToRay (screenCenter);
		RaycastHit hit;

		//射撃音
		ShootSound ();
		//銃口に火花生成
		MakeSparkle ();

		if (Physics.Raycast (ray, out hit)) {
			bulletHitPoint = hit.point;
			hitObj = hit.collider.gameObject;
			generatedSparkle = Instantiate (sparkle, bulletHitPoint, Quaternion.identity);
			Destroy (generatedSparkle, 0.1f);
			if (hitObj.name == "pCube1") {
				hitObj.transform.parent.GetComponent<TargetController> ().CountScore (hit.point); 
				hitObj.transform.parent.GetComponent<TargetController> ().MinusHP (); 

			}
		}
	}
	IEnumerator ReloadBullet(){
		if (_bulletBox == 0) {
			yield break;
		}
		if (_chargedBullet == _maxBullet) {
			yield break;
		}
		_isRelaoding = true;
		int gap = _maxBullet - _chargedBullet;
		if (_bulletBox < (gap)) {
			_chargedBullet += _bulletBox;
			_bulletBox = 0;
		}else{
			_chargedBullet = _maxBullet;
			_bulletBox -= gap;
		}
		audioSource.PlayOneShot (reload);
		yield return new WaitForSeconds (2f);
		_isRelaoding = false;
	}
	void ShootSound(){
		audioSource.PlayOneShot (fire);
	}
	void MakeSparkle(){
		generatedSparkle = Instantiate(sparkle,transform.position,Quaternion.FromToRotation(transform.position,bulletHitPoint));
		Destroy (generatedSparkle, 0.1f);
	}
} 
