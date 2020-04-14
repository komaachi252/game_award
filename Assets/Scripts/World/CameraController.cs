using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{

    private const float EARTH_RANGE = 6.0f;//地球とカメラの距離
    private const int START_WORLD = 1;//最初のワールド
    private const int MOVE_FLAME = 50;//移動フレーム数

    public GameObject earth;//地球
    public GameObject[] marking;//マーキングオブジェクト

    private int now_world = START_WORLD;//今のワールド
    private int next_world = START_WORLD;//次のワールド

    float flame;//一回で移動する速度

    // Start is called before the first frame update
    void Start()
    {

        ChangeWorld(marking[START_WORLD].transform.position, 12);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && next_world != 0)//ワールドを戻る
        {
            next_world--;
            //Debug.Log("戻る");
        }

        if (Input.GetKeyDown(KeyCode.Q) && next_world != marking.Length - 1)//ワールドを進む
        {
            next_world++;
            //Debug.Log("進む");
        }

        if (now_world != next_world)//移動先がちう場合
        {
            //移動量計算
            
            float kakudo;//目標までの角度
            kakudo = Vector3.Angle(this.transform.position - earth.transform.position, marking[next_world].transform.position - earth.transform.position);
            flame = kakudo / MOVE_FLAME;//一回の移動量
            flame = flame * Time.deltaTime;

            now_world = next_world;//目標と現在の位置を同じにする
        }

        ChangeWorld(marking[next_world].transform.position, flame);//ワールド変更
        
    }

    //ワールド変更
    //引数：
    //target_posi = 目標座標
    //move = 移動量
    private void ChangeWorld(Vector3 target_posi,float move)
    {
        Vector3 Front_vec;//目標までのベクトル

        this.transform.position = earth.transform.position;//地球の場所に移動

        Vector3 target_vec = this.transform.position - target_posi;//目標までのベクトル
        Quaternion rot = Quaternion.LookRotation(target_vec);//回転軸計算
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, move);//回転するやつ

        Front_vec = this.transform.rotation * Vector3.forward;//マーキングされているほうのベクトルを取得

        this.transform.position = -Front_vec * EARTH_RANGE;//移動
    }

    //今の選択されているワールドをもらう
    public int Get_NowWorld
    {
        get { return now_world; }
    }
}
