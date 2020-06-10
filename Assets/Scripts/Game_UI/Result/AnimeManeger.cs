using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeManeger : MonoBehaviour
{

    [SerializeField] StarManeger star;
    [SerializeField] StageClearManager clear;

    //フラグ
    //0 = 何もしない
    //1 = 星アニメーション開始
    //2 = 星アニメーション中
    //3 = ゲームクリアアニメ開始
    //4 = ゲームクリアアニメ中
    int flag;

    // Start is called before the first frame update
    void Start()
    {
        flag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 1)//星アニメスタート
        {
            star.Star_AnimeStart();//星のアニメーションスタート
            flag = 2;
        }
        else if (flag == 2 && star.Active_AnimeStar() == false)//星アニメーションが終わったら
        {
            flag = 3;
        }
        else if (flag == 3)//ゲームクリアアニメスタート
        {
            FindObjectOfType<Audio_Manager>().Play("stage_clear");
            clear.Start_anime();//アニメーション開始
            flag = 4;
        }
        else if (flag == 4 && clear.Active_Anime() == false)//ゲームクリアアニメーション終わったら
        {
            flag = 5;
        }

        
    }
}
