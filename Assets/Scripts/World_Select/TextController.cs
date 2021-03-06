﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{

    public GameObject Text_obj;//現在のワールドを表示するやつ

    private Text world_tex;//ワールドのテキスト


    // Start is called before the first frame update
    void Start()
    {
        //現在のワールド表示するための準備
        world_tex = Text_obj.GetComponent<Text>();//ワールド表示するテキストをもらう
    }

    // Update is called once per frame
    void Update()
    {
        int stage = 0;
        for (int i = 0; i < StageController.Get_world() - 1; i++)
        {
            stage += World_Stage_Nm.GET_STAGE_NUM(i);
        }

        //現在のワールド表示
        int world_number = StageController.Get_world() + 1;
        int stage_number = StageController.Get_stage() + 1 - stage;

        world_tex.text = "World" + world_number + "\nStage" + stage_number;
    }
}
