using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenUI : MonoBehaviour {
	private float _remainingTime;

	[SerializeField] private Text[] text = new Text[4];
	[SerializeField ]private ShootBullet shootBul;
	[SerializeField] private TargetController targetCon;

	// Use this for initialization
	void Start () {
		_remainingTime = 100;
	}
	// Update is called once per frame
	void Update () {
		_remainingTime -= 1*Time.deltaTime;

		text[0].text = ("Time:"+_remainingTime.ToString("f1"));
		text [1].text = ("Pt:"+ targetCon.TotalScore);
		text[2].text = ("BulletBox:"+ shootBul.BulletBox);
		text [3].text = ("Bullet:"+ shootBul.ChargedBullet);
	}
} 
