using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	[SerializeField] private GameObject HeadMaker;    

	private bool _isFalling;
	private Animator anim;
	private int _lifeCount;   
	private int _totalScore; 	    

	public int TotalScore {
		set{ this._totalScore = value;
		}
		get{ return this._totalScore; 
		}
	}

	    // Use this for initialization
	    void Start () {
		_isFalling = false;
		_lifeCount = 5;
		anim = GetComponent<Animator> ();

	}
	    
	    // Update is called once per frame
	    void Update () {
	}

	public void CountScore(Vector3 hitP){  
		

		if(_isFalling){
			return;
		}

		float distance;
		distance = (HeadMaker.transform.position - hitP).magnitude;
	
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
	    
