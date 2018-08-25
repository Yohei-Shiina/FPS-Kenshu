using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {
	    [SerializeField] int _lifeCount;
	    [SerializeField] private GameObject HeadMaker;

	    private Animator anim;
	    private GameObject TargetObj;
	    private int _totalScore; 
	    private bool _isFalling;

	    public int TotalScore {
		get{ return this._totalScore; }
	}


	    // Use this for initialization
	    void Start () {
		_isFalling = false;
		_lifeCount = 5;
		anim = GetComponent<Animator> ();
		TargetObj = transform.Find("HeadMaker").gameObject;
	}
	    
	    // Update is called once per frame
	    void Update () {
	}

	public void CountScore(Vector3 hitP){  
		

		if(_isFalling){
			return;
		}

		float distance;
		distance = (TargetObj.transform.position - hitP).magnitude;
	
		_totalScore +=(int)( 10 / distance);
		Debug.Log ("トータルスコア" + _totalScore);
	}


	public void MinusHP(){
		if(_isFalling){
			return;
		}
		_lifeCount -= 1;
		if (_lifeCount == 0) {
			StartCoroutine("DownTarget");
		}
	}

	public IEnumerator DownTarget(){
		anim.SetTrigger ("down");
		_isFalling = true;
		yield return new WaitForSeconds (10f);
		WakeTarget ();
	}

	public void WakeTarget(){
		_isFalling = false;
		anim.SetTrigger ("up");
		_lifeCount = 5;
	}
}
	    
