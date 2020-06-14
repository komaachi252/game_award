using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManeger : MonoBehaviour
{
    //星
    [SerializeField] Image[] image_star;//星イメージ
    [SerializeField] Sprite[] sprite_star;//星スプライト

    [SerializeField] Text text_Phese;//変身回数記録
    [SerializeField] ExposePheseCount phesecount;//今の変化回数
    [SerializeField] Star_PheseSet pheseset;//変化回数目標を受け取る
    [SerializeField] ExposeIsHelp help;//仲間を救出したかしてないか
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < image_star.Length; i++)
        {
            image_star[i].sprite = sprite_star[0];
        }

        text_Phese.text = "ミス";
        
    }

    // Update is called once per frame
    void Update()
    {
        //=========================================
        //星01(ステージクリア)
        //=========================================


        //=========================================
        //星02(変化回数)
        //=========================================
        if (phesecount.Phese_cnt.Phase_Cnt <= pheseset.Get_StarPhese(StageController.Get_Index()))
        {
            image_star[1].sprite = sprite_star[1];//星明るくする

        }
        else
        {
            image_star[1].sprite = sprite_star[0];//星明るくする
        }

        if (phesecount.Phese_cnt.Phase_Cnt >= 99)//変化回数99回超えたら
        {
            text_Phese.text = "99" + " / " + pheseset.Get_StarPhese(StageController.Get_Index());
        }
        else
        {
            text_Phese.text = phesecount.Phese_cnt.Phase_Cnt.ToString() + " / " + pheseset.Get_StarPhese(StageController.Get_Index());
        }

        //=========================================
        //星03(仲間救出)
        //=========================================
        if (help.Expose_Helpme.Is_Help == true)
        {
            image_star[2].sprite = sprite_star[1];
        }

    }
}
