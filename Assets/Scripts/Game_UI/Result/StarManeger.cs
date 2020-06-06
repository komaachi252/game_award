using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarManeger : MonoBehaviour
{
    [SerializeField] ExposePheseCount exphese;//変化回数貰う

    public GameObject[] gameobject_star;
    private Animator[] anime_star;
    

    public Text[] text_star;

    public Sprite true_star;
    public Sprite false_star;

    //フラグ
    private const int TRUE = 0;//星を明るくする
    private const int FALSE = 1;//星を暗くする
    private int[] star_flag;//星の状態

    //星フラグ
    private int[] Phese_num;//ステージごとの変化回数の評価数記録するやつ

    //フラグ
    //0 = 何も起きない
    //1 = 一回だけ(クリア後)
    //2 = 星1アニメーション
    //3 = 星2アニメーション
    //4 = 星3アニメーション
    private int flag = 0;//最初の一回だけフラグ
    

    // Start is called before the first frame update
    void Start()
    {
        //=============================================================================
        //星アニメーションとイメージの準備
        //=============================================================================
        anime_star = new Animator[gameobject_star.Length];
        for (int i = 0; i < gameobject_star.Length; i++)
        {
            anime_star[i] = gameobject_star[i].GetComponent<Animator>();//アニメーション
        }

        


        //=============================================================================
        //星フラグ
        //=============================================================================
        star_flag = new int[gameobject_star.Length];//星の数を同じ数分生成

        for (int i = 0; i < star_flag.Length; i++)//とりあえずFALSE
        {
            star_flag[i] = -1;
        }

        //星ID生成
        int stage_num = 0;
        for (int i = 0; i < World_Stage_Nm.GET_WORLD_NUM(); i++)
        {
            stage_num += World_Stage_Nm.GET_STAGE_NUM(i);
        }

        Phese_num = new int[stage_num];

        //=============================================================================
        //ステージ分星の設定生成するやつ
        //=============================================================================

        Star_ID_Set();//星の情報をセット

        //Star_AnimeStart();
    }

    //======================================================
    //星の情報を設定する関数
    //======================================================
    private void Star_ID_Set()
    {

        for (int i = 0; i < Phese_num.Length; i++)
        {
            Phese_num[i] = 5;
        }

        text_star[1].text = "変化回数　" + Phese_num[StageController.Get_stage()] + "回";
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 1)//星1アニメーション中
        {
            if (anime_star[0].GetCurrentAnimatorStateInfo(0).IsName("End") || star_flag[0] == FALSE)//状態確認
            {
                
                if (Phese_num[StageController.Get_stage()] <= exphese.Phese_cnt.Phase_Cnt)//変化回数制限になってたら
                {
                    anime_star[1].SetTrigger("Anime");
                    star_flag[1] = TRUE;
                }
                else
                {
                    star_flag[1] = FALSE;
                }
                flag = 2;
            }
        }

        if (flag == 2)//星2アニメーション中
        {
            if (anime_star[1].GetCurrentAnimatorStateInfo(0).IsName("End") || star_flag[1] == FALSE)//状態確認
            {
                anime_star[2].SetTrigger("Anime");
                star_flag[2] = TRUE;
                flag = 3;
            }
        }


        if (flag == 3)//星3アニメーション中
        {
            if (anime_star[2].GetCurrentAnimatorStateInfo(0).IsName("End") || star_flag[2] == FALSE)//状態確認
            {
                
                flag = 4;
            }
        }

    }


    //星のアニメーションを再生する
    public void Star_AnimeStart()
    {

        anime_star[0].SetTrigger("Anime");
        star_flag[0] = TRUE;
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

    //星がアニメーションしたかしてないか
    public bool Active_AnimeStar()
    {
        if (flag == 4)
        {
            return false;
        }
        return true;
    }
}
