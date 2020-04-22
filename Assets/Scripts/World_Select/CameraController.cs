using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{

    private const float WORLD_EARTH_RANGE = 6.0f;//地球とカメラの距離　ワールドバージョン
    private const float STAGE_EARTH_RANGE = 4.0f;//地球とカメラの距離　ステージバージョン
    private const float SELECT_RANGE = (WORLD_EARTH_RANGE - STAGE_EARTH_RANGE);//選択移動の距離

    private const float WORLD_MOVE_FLAME = 20.0f;//ワールド移動フレーム数
    private const float SELECT_MOVE_FLAME = 0.3f;//ワールド選択とステージ選択が入れ替わる時のフレーム数

    

    public Transform earth;//地球

    private Transform[] world_marking;//ワールドマーキング

    private StageController stagecontroller;//ステージコントローラーの情報入れる

    private float move_flame;//ワールド移動を一回で移動する速度
    private float select_flame;//ワールド選択とステージ選択が移動するときに一回で移動する速度

    private Vector3 Front_vec;//目標までのベクトル

    private float earth_range;//地球との距離

    //0　ワールド選択　回転してない
    //1　ワールド選択　回転中
    //2　ステージ選択　移動してない
    //3　ステージ選択　移動中　一回切り
    //4　ステージ選択　移動中　近づく
    //5　ワールド選択　手前から戻る　一回切り
    //6　ワールド選択　手前から戻る
    private int flag;//フラグ

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
        float angle = Vector3.Angle(Front_vec, this.transform.position - world_marking[StageController.Get_world()].position);//ワールド目標までの角度

        //=========================

        if (select_flag == 0)//ワールド選択の時
        {

            //ワールド選択時のカメラ
            if (flag == 1 && now_world != next_world)//移動先がちう場合
            {
                //移動量計算

                float kakudo;//目標までの角度
                kakudo = Vector3.Angle(this.transform.position - earth.position, world_marking[next_world].position - earth.position);
                move_flame = kakudo / WORLD_MOVE_FLAME;//一回の移動量
                move_flame = move_flame * Time.deltaTime;

                stagecontroller.Set_world(next_world);//現在選択してるワールドを変更する
            }

            if (flag == 1)
            {

                ChangeWorld(world_marking[next_world].position, move_flame);//ワールド移動

            }

            if (angle == 0.0f)//目標までの角度が0になったら回転しない
            {
                flag = 0;
            }

            if (now_world != next_world)//現在選択ワールドと次に選択してるワールドが違ったら
            {
                flag = 1;
            }

            //===============================

            if (flag == 5)
            {
                move_flame = SELECT_RANGE / SELECT_MOVE_FLAME;//一回で移動する距離
                move_flame = move_flame * Time.deltaTime;
                flag = 6;
            }

            if (flag == 6)
            {
                if (earth_range > WORLD_EARTH_RANGE)//一定より近づいたら
                {
                    earth_range = WORLD_EARTH_RANGE;
                    flag = 0;//移動を止める
                }

                this.transform.position = -Front_vec * earth_range;//移動

                earth_range = earth_range + move_flame;//移動
            }

            if (earth_range < WORLD_EARTH_RANGE)//地球との距離がワールド選択時の時より小さかったら
            {
                flag = 5;
            }


        }
        else if (select_flag == 1)//ステージ選択
        {

            if (flag == 3)//一回きり
            {
                move_flame = SELECT_RANGE / SELECT_MOVE_FLAME;//一回で移動する距離
                move_flame = move_flame * Time.deltaTime;
                flag = 4;
            }

            if (flag == 4)//移動中
            {

                if (earth_range < STAGE_EARTH_RANGE)//一定より近づいたら
                {
                    earth_range = STAGE_EARTH_RANGE;
                    flag = 2;//移動を止める
                }

                this.transform.position = -Front_vec * earth_range;//移動

                earth_range = earth_range - move_flame;//移動
            }

            if (angle == 0.0f)//カメラと目標ワールドの角度が近かったら
            {
                flag = 3;
            }

            if (earth_range > STAGE_EARTH_RANGE)//ステージ距離より大きかったら
            {
                flag = 3;
            }

            

            

        }


        //if (select_flag == 0)//ワールド選択の場合
        //{
        //    if (earth_range >= WORLD_EARTH_RANGE)//現在のワールドの距離がワールドより早かったら
        //    {
        //        earth_range = WORLD_EARTH_RANGE;
        //    }
        //    else
        //    {
        //        select_flame = (SELECT_RANGE / SELECT_MOVE_FLAME) * Time.deltaTime;//一回で移動する距離
        //        earth_range = earth_range + select_flame;
        //    }


           
        //}
        //else if (select_flag == 1)//ステージ選択の場合
        //{
        //    earth_range = STAGE_EARTH_RANGE;
        //}



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
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rot, move);//回転するやつ

        Front_vec = this.transform.rotation * Vector3.forward;//今見てる方向のベクトルを取得

        this.transform.position = -Front_vec * earth_range;//移動
    }

    //目標のワールドとカメラの角度
    public float World_Camera_Angle()
    {
        return Vector3.Angle(Front_vec, this.transform.position - world_marking[StageController.Get_world()].position);
    }

}
