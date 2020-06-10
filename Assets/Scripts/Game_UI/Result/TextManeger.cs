using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManeger : MonoBehaviour
{

    public Text text_world;//ワールド書くテキスト
    public Text text_stage;//ステージ書くテキスト

    int stage;
    int world;

    // Start is called before the first frame update
    void Start()
    {
        //=====================================================================
        //現在のワールドとステージをもらう
        //=====================================================================
        world = StageController.Get_world() + 1;//現在のワールド

        int stage_count = 0;
        for (int i = 0; i < StageController.Get_world() - 1; i++)
        {
            stage_count += World_Stage_Nm.GET_STAGE_NUM(i);
        }

        stage = StageController.Get_stage() - stage_count + 1;//現在のステージ
    }

    // Update is called once per frame
    void Update()
    {

        

        


        //=====================================================================
        //テキスト書き換え
        //=====================================================================

        text_world.text = "WORLD " + world;
        text_stage.text = "STAGE " + stage;
    }
}
