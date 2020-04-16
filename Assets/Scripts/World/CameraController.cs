using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{

    private const float WORLD_EARTH_RANGE = 6.0f;//地球とカメラの距離　ワールドバージョン
    private const float STAGE_EARTH_RANGE = 4.0f;//地球とカメラの距離　ステージバージョン
    private const int MOVE_FLAME = 20;//移動フレーム数

    public Transform earth;//地球

    private Transform[] world_marking;//ワールドマーキング

    private StageController stagecontroller;//ステージコントローラーの情報入れる

    private float flame;//一回で移動する速度

    private Vector3 Front_vec;//目標までのベクトル

    private float earth_range;//地球との距離

    // Start is called before the first frame update
    void Start()
    {

        //====================================

        GameObject stagecon = GameObject.Find("StageController");//ステージコントローラーオブジェをもらう
        stagecontroller = stagecon.GetComponent<StageController>();//ステージコントローラーのスクリプトをもらう

        //====================================

        GameObject worldmarking = GameObject.Find("WorldMarking");//ワールドマーキングが入ってるやつ貰う
        int world_child_num = 0;//子供の数
        foreach (Transform tr in worldmarking.transform)//数を数える
        {
            world_child_num++;
        }

        world_marking = new Transform[world_child_num];//数を確定

        for (int i = 0; i < world_child_num; i++)//子供の情報をもらう
        {
            world_marking[i] = worldmarking.transform.GetChild(i).gameObject.transform;
        }

        //====================================

        if (stagecontroller.Get_SelectFlag() == 0)//ワールド選択の場合
        {
            earth_range = WORLD_EARTH_RANGE;
        }
        else if (stagecontroller.Get_SelectFlag() == 1)//ステージ選択の場合
        {
            earth_range = STAGE_EARTH_RANGE;
        }

        //====================================

        ChangeWorld(world_marking[StageController.START_WORLD].position, 12);//初期位置に移動

    }

    // Update is called once per frame
    void Update()
    {
        int now_world = StageController.Get_world();//現在選択してるワールド
        int next_world = stagecontroller.Get_nextworld();//次に選択されるワールド
        int select_flag = stagecontroller.Get_SelectFlag();//ワールド選択かステージ選択かフラグ

        //=========================

        //ワールド選択時のカメラ
        if (now_world != next_world)//移動先がちう場合
        {
            //移動量計算

            float kakudo;//目標までの角度
            kakudo = Vector3.Angle(this.transform.position - earth.position, world_marking[next_world].position - earth.position);
            flame = kakudo / MOVE_FLAME;//一回の移動量
            flame = flame * Time.deltaTime;

            stagecontroller.Set_world(next_world);//現在選択してるワールドを変更する
        }

        ChangeWorld(world_marking[next_world].position, flame);//ワールド移動

        //=========================

        if (stagecontroller.Get_SelectFlag() == 0)//ワールド選択の場合
        {
            earth_range = WORLD_EARTH_RANGE;
        }
        else if (stagecontroller.Get_SelectFlag() == 1)//ステージ選択の場合
        {
            earth_range = STAGE_EARTH_RANGE;
        }

    }

    //ワールド変更
    //引数：
    //target_posi = 目標座標
    //move = 移動量
    private void ChangeWorld(Vector3 target_posi,float move)
    {
        

        this.transform.position = earth.position;//地球の場所に移動

        Vector3 target_vec = this.transform.position - target_posi;//目標までのベクトル

        Quaternion rot = Quaternion.LookRotation(target_vec);//回転軸計算
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, move);//回転するやつ

        Front_vec = this.transform.rotation * Vector3.forward;//今見てる方向のベクトルを取得

        this.transform.position = -Front_vec * earth_range;//移動
    }

}
