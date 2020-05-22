using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    private const float SPEED = 0.9f;

    private Vector3 Start_posi = new Vector3(0.0f, 1.7f, -1.0f);//初期座標
    private Vector3 Lookat_posi = new Vector3(0.0f, 1.0f, 0.0f);//見る座標

    private StageController sc;//カメラコントローラー

    private float distance;//距離
    private Vector3 Front_vec;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obje_stagecontroller = GameObject.Find("StageController");//ステージコントローラー入ってるやつ貰う
        sc = obje_stagecontroller.GetComponent<StageController>();//ステージコントローラースクリプト貰う

        this.transform.position = Start_posi;//初期位置

        distance = Vector3.Distance(this.transform.position, Lookat_posi);
        this.transform.LookAt(Vector3.up, Lookat_posi);

    }

    // Update is called once per frame
    void Update()
    {
        int select_flag = sc.Get_SelectFlag();//現在の選択モードを受け取る

        if (select_flag == 2)//ステージに移動する場合
        {
            Front_vec = this.transform.rotation * Vector3.forward;//向いている方向を求める
            this.transform.position += Front_vec * SPEED * Time.deltaTime;
        }
        else if(select_flag != 2)
        {
            this.transform.RotateAround(Lookat_posi, Vector3.up, 10.0f * Time.deltaTime);
        }
    }

    
}
