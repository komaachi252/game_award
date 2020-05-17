using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{

    public GameObject WorldMarking;//ワールドマーキングリスト

    private Transform[] world;//ワールド

    StageController stagecon;//ステージコントローラー

    // Start is called before the first frame update
    void Start()
    {
        //=====================================================
        //ワールド読み込み
        //=====================================================

        world = new Transform[World_Stage_Nm.GET_WORLD_NUM()];
        for (int i = 0; i < World_Stage_Nm.GET_WORLD_NUM(); i++)
        {
            world[i] = WorldMarking.transform.GetChild(i).transform;
        }

        //=====================================================
        //ステージコントローラー準備
        //=====================================================
        GameObject stagecont = GameObject.Find("StageController");//ステージコントローラーオブジェをもらう
        stagecon = stagecont.GetComponent<StageController>();//ステージコントローラーのスクリプトをもらう


        //=====================================================
        //初期位置に移動
        //=====================================================
        float angle = Vector3.Angle(Vector3.up, world[stagecon.Get_nextworld()].transform.position - this.transform.position);
        Earth_Rotate(world[stagecon.Get_nextworld()].transform.position - this.transform.position, angle);
    }

    // Update is called once per frame
    void Update()
    {
        //int now_world;
        int select_mode = stagecon.Get_SelectFlag();//ワールド選択かステージ選択中かを確認する
        int next_world = stagecon.Get_nextworld();//次に選択されるコントローラーを貰う

        float angle = Vector3.Angle(Vector3.up, world[next_world].transform.position - this.transform.position);

        if (angle != 0.0f)
        {
            Earth_Rotate(world[next_world].transform.position - this.transform.position, (angle / 0.3f) * Time.deltaTime);
        }
    }

    private void Earth_Rotate(Vector3 target_posi, float move_speed)
    {
        Vector3 up_vec = Vector3.up;//上方向
        Vector3 target_vec = this.transform.position - target_posi;//ターゲット方向のベクトル
        Vector3 rotate_axis = Vector3.Cross(up_vec, target_vec);//回転軸

        this.transform.RotateAround(this.transform.position, rotate_axis, move_speed);//回転
    }

    
}
