using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public GameObject World_Text_obj;//現在のワールドを表示するやつ
    private Text world_text;//ワールドのテキスト

    public GameObject Stage_text_obj;//現在のステージを表示するやつ
    private Text Stage_text;//ステージのテキスト


    // Start is called before the first frame update
    void Start()
    {
        //現在のワールド表示するための準備
        world_text = World_Text_obj.GetComponent<Text>();//現在のワールド表示するテキスト貰う

        Stage_text = Stage_text_obj.GetComponent<Text>();//現在のステージ表示するテキスト貰う
    }

    // Update is called once per frame
    void Update()
    {
        //現在のワールド表示
        int world = StageController.Get_world() + 1;
        world_text.text = "World" + world;

        //現在のステージ表示
        int stage = StageController.Get_stage() + 1;
        Stage_text.text = "Stage" + stage;
    }
}
