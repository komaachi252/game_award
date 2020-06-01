using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManeger : MonoBehaviour
{
    public GameObject[] gameobject_star;
    private Image[] image_star;
    private Animator[] anime_star;
    

    public Text[] text_star;

    public Sprite true_star;
    public Sprite false_star;

    //フラグ
    private const int TRUE = 0;//星を明るくする
    private const int FALSE = 1;//星を暗くする
    private int[] star_flag;//星の状態


    private int[][] star_id;//星を明るくする条件のID

    //フラグ
    //0 = 何も起きない
    //1 = 一回だけ(クリア後)
    //2 = 星1アニメーション
    //3 = 星2アニメーション
    //4 = 星3アニメーション
    private int flag = 0;//最初の一回だけフラグ

    //フラグ
    //0 = 何もしない
    //1 = 小さくする
    //2 = 大きくする
    private int anime_flag;//アニメーションフラグ
    

    // Start is called before the first frame update
    void Start()
    {
        //=============================================================================
        //星アニメーションとイメージの準備
        //=============================================================================
        image_star = new Image[gameobject_star.Length];
        anime_star = new Animator[gameobject_star.Length];
        for (int i = 0; i < gameobject_star.Length; i++)
        {
            image_star[i] = gameobject_star[i].GetComponent<Image>();//イメージ
            anime_star[i] = gameobject_star[i].GetComponent<Animator>();//アニメーション
        }

        


        //=============================================================================
        //星フラグ
        //=============================================================================
        star_flag = new int[gameobject_star.Length];//星の数を同じ数分生成

        for (int i = 0; i < star_flag.Length; i++)//とりあえずFALSE
        {
            star_flag[i] = FALSE;
        }


        //=============================================================================
        //ステージ分星の設定生成するやつ
        //=============================================================================
        int stage_num = 0;
        for (int i = 0; i < World_Stage_Nm.GET_WORLD_NUM(); i++)//ワールド分回す
        {
            stage_num += World_Stage_Nm.GET_STAGE_NUM(i);//ステージ数をカウントする
        }

        star_id = new int[stage_num][];//ステージ数分生成
        for (int i = 0; i < star_id.Length; i++)
        {
            star_id[i] = new int[gameobject_star.Length];//星の数分生成
        }

        Star_ID_Set();//星の情報をセット
    }

    //======================================================
    //星の情報を設定する関数
    //======================================================
    private void Star_ID_Set()
    {
        //とりあえず
        for (int i = 0; i < star_id.Length; i++)
        {
            star_id[i][0] = 0;
            star_id[i][1] = 1;
            star_id[i][2] = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (flag == 0)
        //{
        //    for (int i = 0; i < image_star.Length; i++)
        //    {
        //        Star_switch(i, star_id[StageController.Get_stage()][i]);//星の状態変更
        //    }

        //    for (int i = 0; i < image_star.Length; i++)
        //    {
        //        if (star_flag[i] == TRUE)//星が明るくなる場合
        //        {
        //            image_star[i].sprite = true_star;
        //        }
        //        else if (star_flag[i] == FALSE)//星が暗くなる場合
        //        {
        //            image_star[i].sprite = false_star;
        //        }
        //    }

        //    flag = 1;//二回目通過するの回避
        //}

        if (Input.GetKeyDown(KeyCode.P))
        {
            Start_Star_Anime();
            
        }

        if (flag == 1)//星準備
        {
            if (anime_star[0].GetCurrentAnimatorStateInfo(0).IsName("End"))//状態確認
            {
                anime_star[1].SetTrigger("Anime");
                flag = 2;
            }
        }

        if (flag == 2)//星1アニメーション
        {
            if (anime_star[1].GetCurrentAnimatorStateInfo(0).IsName("End"))//状態確認
            {
                anime_star[2].SetTrigger("Anime");
                flag = 3;
            }
        }


        if (flag == 3)//星2アニメーション
        {
            if (anime_star[2].GetCurrentAnimatorStateInfo(0).IsName("End"))//状態確認
            {
                anime_star[3].SetTrigger("Anime");
                flag = 4;
            }
        }


        if (flag == 4)//星3アニメーション
        {
            if (anime_star[3].GetCurrentAnimatorStateInfo(0).IsName("End"))//状態確認
            {
                flag = 0;
            }
        }

    }

    public void Start_Star_Anime()
    {
        anime_star[0].SetTrigger("Anime");
        flag = 1;
    }


    //======================================================
    //星の状態確認&変更
    //======================================================
    private void Star_switch(int star_number,int star_id)
    {
        switch (star_id)
        {
            case 0:
                text_star[star_number].text = "ステージクリア";
                star_flag[star_number] = TRUE;//クリアしか出ないはずだからとりあえずTRUE

                break;
            case 1:
                text_star[star_number].text = "変化回数　10回";

                break;
            case 2:
                text_star[star_number].text = "おまけアイテム取得";

                break;
            default:
                text_star[star_number].text = "何もないよ";
                star_flag[star_number] = TRUE;
                break;
        }
    }

    //======================================================
    //星のアニメーションを始める
    //======================================================
    public void Star_AnimeStart()
    {

    }
}
