using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenUI : MonoBehaviour
{

    [SerializeField] private Text[] text = new Text[4];
    [SerializeField] private TargetController targetCon;

    private ShootBullet shootBul;
    private float _remainingTime;
    private bool _shootBulCheck;

    public bool ShootBulCheck
    {
        set
        {
            this._shootBulCheck = value;
        }
        get
        {
            return this._shootBulCheck;
        }
    }

    // Use this for initialization
    void Start()
    {
        _remainingTime = 100;
        _shootBulCheck = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (ShootBulCheck == true)
        {
            //これだと最初に生成されたCloneの情報をとってしまう。
            //ShootBulletでScreenUIを見つけて
            shootBul = GameObject.Find("Player(Clone)").GetComponent<ShootBullet>();

            _remainingTime -= 1 * Time.deltaTime;
            text[0].text = ("Time:" + _remainingTime.ToString("f1"));
            text[1].text = ("Pt:" + targetCon.TotalScore);
        }
    }

    public void DisplayText(int Magazine, int ReloadedBullet)
    {
        text[2].text = ("BulletBox:" + Magazine);
        text[3].text = ("Bullet:" + ReloadedBullet);

    }
}