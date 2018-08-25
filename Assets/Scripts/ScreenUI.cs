using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenUI : MonoBehaviour
{
    private float _remainingTime;

    [SerializeField] private Text[] text = new Text[4];
    [SerializeField] private ShootBullet shootBul;
    [SerializeField] private TargetController targetCon;

    public bool _shootBulCheck;

    // Use this for initialization
    void Start()
    {
        _remainingTime = 100;
        _shootBulCheck = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (_shootBulCheck == true)
        {
			//これだと最初に生成されたCloneの情報をとってしまう。
			//ShootBulletでScreenUIを見つけて
            shootBul = GameObject.Find("Player(Clone)").GetComponent<ShootBullet>();

            _remainingTime -= 1 * Time.deltaTime;
			text[0].text = ("Time:" + _remainingTime.ToString("f1"));
			text[1].text = ("Pt:" + targetCon.TotalScore);  
        }
    }

	public void DisplayText(int Magazine, int ReloadedBullet){
		text[2].text = ("BulletBox:" + Magazine);
		text[3].text = ("Bullet:" + ReloadedBullet);

	}
} 
