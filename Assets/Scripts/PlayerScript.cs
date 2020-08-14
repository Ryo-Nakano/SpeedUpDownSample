using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] float speedForward; //Playerが前進する速度
    [SerializeField] float speedForward_max; //Playerが前進する最大速度
    [SerializeField] float speedHorizontal; //Playerが横移動する速度
    [SerializeField] float deceleration; //Enemyに当たった時の減速量
    [SerializeField] float acceleration; //Playerの前進加速度の大きさ(大きほどすぐ加速する)

    [SerializeField] Text speedText; //Playerの移動速度を表示するText

    void Start()
    {
        
    }

    void Update()
    {
        MoveForward(); //前進する関数
        MoveHorizontal(); //横移動する関数
        Acceleration(); //Playerの前進速度加速を管理する関数

        RefreshUI(); //UI表示を更新する関数
    }

    //前進する関数
    void MoveForward()
    {
        this.transform.position += new Vector3(0, 0, speedForward * Time.deltaTime);
    }

    //横移動する関数
    void MoveHorizontal()
    {
        float dx = Input.GetAxis("Horizontal"); //キーボードの左右キーの入力を取得
        this.transform.position += new Vector3(dx * speedHorizontal * Time.deltaTime, 0, 0);
    }

    //Playerが前進する速度を下げる関数
    void SpeedDown()
    {
        speedForward -= deceleration; //decelerationの値だけPlayerの移動速度を減少
    }

    //UI表示を更新する関数
    void RefreshUI()
    {
        speedText.text = speedForward.ToString("f1");
    }

    //Playerの前進速度加速を管理する関数
    void Acceleration()
    {
        speedForward += acceleration * Time.deltaTime;

        if (speedForward > speedForward_max) //PLayerの前進速度が最大速度より大きい時
        {
            speedForward = speedForward_max;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit!");
            SpeedDown(); //Playerが前進する速度を下げる関数

            Destroy(col.gameObject);
        }
    }
}
