using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkingController : MonoBehaviour
{

    private Material[][] stage_material;//ステージマテリアル

    private Material[] world_obj;//ワールドオブジェクト

    private StageController stagecontroller;

    
    private int stage_flag;//フラグ
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject WorldMarking = GameObject.Find("WorldMarking");
        GameObject StageMarking = GameObject.Find("StageMarking");

        //===============================

        GameObject stagecon = GameObject.Find("StageController");//ステージコントローラーオブジェをもらう
        stagecontroller = stagecon.GetComponent<StageController>();//ステージコントローラーのスクリプトをもらう

        //===============================

        //ワールドの情報をもらう
        int world_num = 0;
        foreach (Transform transform in WorldMarking.transform)//ワールドの数を数える
        {
            world_num++;
        }

        world_obj = new Material[world_num];//動的メモリ確保

        for (int i = 0; i < world_num; i++)//子供の情報をもらう
        {
            world_obj[i] = WorldMarking.transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
        }

        //===============================
        stage_material = new Material[world_num][];//ワールド数分メモリ確保


        int stage_num = 0;//ステージの数
        for (int i = 0; i < world_num; i++)
        {
            foreach (Transform transform in StageMarking.transform.GetChild(i).transform)
            {
                stage_num++;//カウント
            }

            stage_material[i] = new Material[stage_num];//ステージ数分メモリ確保
            stage_num = 0;//リセット
        }

        for (int i = 0; i < world_num; i++)//ワールド番号
        {
            GameObject world = StageMarking.transform.GetChild(i).gameObject;
            for (int j = 0; j < stage_material[i].Length; j++)//ステージ番号
            {
                GameObject child = world.transform.GetChild(j).gameObject;
                stage_material[i][j] = child.GetComponent<Renderer>().material;
            }
        }


        //===============================
    }

    // Update is called once per frame
    void Update()
    {
        int now_stage = StageController.Get_stage();
        int now_world = StageController.Get_world();

        if (stagecontroller.Get_SelectFlag() == 1)//ステージ選択になってたら
        {
            for (int i = 0; i < world_obj.Length; i++)
            {
                world_obj[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);//ワールド非表示
            }

            for (int i = 0; i < stage_material[now_world].Length; i++)
            {
                stage_material[now_world][i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//ステージ表示
            }

            stage_material[now_world][now_stage].color = Color.red;//選択されてるやつの色を変える
        }
        else if (stagecontroller.Get_SelectFlag() == 0)//ワールド選択画面になってたら
        {

            
            for (int i = 0; i < stage_material.Length; i++)//ワールド番号
            {
                for (int j = 0; j < stage_material[i].Length; j++)//ステージ番号
                {
                    stage_material[i][j].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);//非表示
                }
            }

            for (int i = 0; i < world_obj.Length; i++)
            {
                world_obj[i].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);//表示
            }

            world_obj[now_world].color = Color.red;//選択してるやつ色変更

        }
    }
}
