﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private const float SPEED = 360.0f;//移動速度
    private const float LEFT_MAX = -40.0f;//左方向の移動限界(ローカル)
    private const float RIGHT_MAX = 43.0f;//右方向の移動限界(ローカル)
    private const float OUT_MAX = 451.0f;//表示エリアの外の移動限界(ローカル&右側)


    public GameObject Select;//今ワールドかステージ選択なのかを示すテキストの入ったImageのオブジェクト
    private Text Select_mode;//今ワールドかステージ選択なのかを示すテキスト

    public GameObject sample_staworld;//選択UIサンプル
    
    private GameObject[] CreateUI;//複製したUI
    private Text[] UI_Text;//複製したUIのテキスト

    //フラグ
    //0 = 未選択
    //1 = 選択中
    //2 = 選択中に移動
    //3 = 未選択に移動
    private int[] flag;

    private int old_select;//直前の状態
    //フラグ
    //0 = 画面外
    //1 = 画面内
    //2 = 画面外に移動
    //3 = 画面内に移動
    private int change_flag;//状態変化フラグ

    private int up;//スクロールの一番上
    private int down;//スクロールの一番下

    //フラグ
    //0 = 停止
    //1 = 上移動
    //2 = 下移動
    private int scroll_flag;
    private float move_max;//移動幅

    private StageController stagecon;//ステージコントローラー

    

    // Start is called before the first frame update
    void Start()
    {
        //=====================================================
        //ステージコントローラー準備
        //=====================================================
        GameObject stagecont = GameObject.Find("StageController");//ステージコントローラーオブジェをもらう
        stagecon = stagecont.GetComponent<StageController>();//ステージコントローラーのスクリプトをもらう

        //=====================================================
        //テキスト情報受け取る&座標調整
        //=====================================================
        Select_mode = Select.transform.GetChild(0).transform.GetComponent<Text>();//テキストの情報貰う
        Select_mode.text = "WORLD SELECT";

        Select.transform.localPosition = new Vector3(841.0f, Select.transform.localPosition.y, Select.transform.localPosition.z);

        //=====================================================
        //複製
        //=====================================================
        int max_num = World_Stage_Nm.GET_WORLD_NUM();//ワールド数とステージ数で一番多いやつを記録する
        for (int i = 0; i < World_Stage_Nm.GET_WORLD_NUM(); i++)
        {
            if (max_num < World_Stage_Nm.GET_STAGE_NUM(i))//今の記録より多かったら
            {
                max_num = World_Stage_Nm.GET_STAGE_NUM(i);
            }
        }

        CreateUI = new GameObject[max_num];//生成
        UI_Text = new Text[max_num];
        flag = new int[max_num];

        for (int i = 0; i < CreateUI.Length; i++)//とりあえず配列の数分生成
        {
            CreateUI[i] = (GameObject)Instantiate(sample_staworld, new Vector3(OUT_MAX, 217.0f - ((75.0f + 20.0f) * i), 0.0f), Quaternion.identity);//複製
            CreateUI[i].transform.SetParent(this.transform.Find("Scroll").transform, false);//複製した奴をCanvasの子供に設定

            UI_Text[i] = CreateUI[i].transform.GetChild(0).transform.GetComponent<Text>();//テキスト受け取る
            UI_Text[i].text = "WORLD " + (i + 1);

            flag[i] = 0;

            if (i >= World_Stage_Nm.GET_WORLD_NUM())//ワールド数を超えてた場合
            {
                CreateUI[i].SetActive(false);
            }
        }

        sample_staworld.SetActive(false);//コピー元非表示

        //=====================================================
        //フラグ準備
        //=====================================================
        old_select = -1;
        change_flag = 0;

        up = 0;
        down = 6;
        move_max = 75.0f + 20.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //=====================================================
        //変数
        //=====================================================
        int now_world = StageController.Get_world();//現在のワールド取得
        int now_stage = StageController.Get_stage();//現在のステージ取得
        int now_select = stagecon.Get_SelectFlag();//現在の選択


        //=====================================================
        //前の状態と違った場合
        //=====================================================
        if (now_select != old_select)//前の状態と違った場合
        {
            if (change_flag == 1)//画面内の時
            {
                change_flag = 2;
            }

            if (change_flag == 0)//画面外の時
            {
                change_flag = 3;
            }

            for (int i = 0; i < CreateUI.Length; i++)
            {
                flag[i] = 0;//フラグリセット
                up = 0;
                down = 6;

                if (change_flag == 2)//画面外に移動中
                {

                    //移動の計算
                    CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition + new Vector3((SPEED * 2) * Time.deltaTime, 0.0f, 0.0f);
                    Select.transform.localPosition = Select.transform.localPosition + new Vector3((SPEED / 5) * Time.deltaTime, 0.0f, 0.0f);

                    //一定の場所に行ったら
                    if (CreateUI[i].transform.localPosition.x >= OUT_MAX && Select.transform.localPosition.x >= 841.0f)
                    {
                        //位置リセット
                        for (int j = 0; j < CreateUI.Length; j++)
                        {
                            CreateUI[j].transform.localPosition = new Vector3(OUT_MAX, 217.0f - ((75.0f + 20.0f) * j), 0.0f);
 
                        }
                        Select.transform.localPosition = new Vector3(841.0f, Select.transform.localPosition.y, 0.0f);

                        change_flag = 3;//画面内に移動

                        if (now_select == 0)//ワールド選択
                        {
                            for (int j = 0; j < CreateUI.Length; j++)
                            {
                                CreateUI[j].SetActive(true);
                                UI_Text[j].text = "WORLD " + (j + 1);
                                

                                if (j >= World_Stage_Nm.GET_WORLD_NUM())//ワールド数を超えてた場合
                                {
                                    CreateUI[j].SetActive(false);
                                }
                            }

                            Select_mode.text = "WORLD SELECT";
                        }
                        else if (now_select == 1)//ステージ選択
                        {
                            for (int j = 0; j < CreateUI.Length; j++)
                            {
                                CreateUI[j].SetActive(true);

                                UI_Text[j].text = "STAGE " + (j + 1);

                                if (j >= World_Stage_Nm.GET_STAGE_NUM(now_stage))//ワールド数を超えてた場合
                                {
                                    CreateUI[j].SetActive(false);
                                }
                            }

                            Select_mode.text = "STAGE SELECT\n" + "WORLD " + (now_world + 1);
                        }

                    }
                }

                if (change_flag == 3)//画面内に移動
                {

                    //移動
                    CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition - new Vector3((SPEED * 2) * Time.deltaTime, 0.0f, 0.0f);
                    Select.transform.localPosition = Select.transform.localPosition - new Vector3((SPEED / 5) * Time.deltaTime, 0.0f, 0.0f);


                    //一定の場所にたどり着いたら
                    if (CreateUI[i].transform.localPosition.x <= RIGHT_MAX && Select.transform.localPosition.x <= 433.0f)
                    {

                        //位置リセット
                        for (int j = 0; j < CreateUI.Length; j++)
                        {
                            CreateUI[j].transform.localPosition = new Vector3(RIGHT_MAX, CreateUI[j].transform.localPosition.y, 0.0f);
                        }
                        Select.transform.localPosition = new Vector3(433.0f, Select.transform.localPosition.y, 0.0f);

                        change_flag = 1;//画面内

                        old_select = now_select;//状態を同じにする
                    }

                }


            }


        }
        else
        {

            //=====================================================
            //選択されてるやつを左にずらす
            //=====================================================
            if (now_select == 0)//ワールドの場合
            {
                if (flag[now_world] == 0)//未選択の時
                {
                    flag[now_world] = 2;
                }

                for (int i = 0; i < flag.Length; i++)
                {
                    if (flag[i] == 2)//選択中に移動
                    {
                        CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition - new Vector3(SPEED * Time.deltaTime, 0.0f, 0.0f);

                        if (CreateUI[i].transform.localPosition.x < LEFT_MAX)
                        {
                            CreateUI[i].transform.localPosition = new Vector3(LEFT_MAX, CreateUI[i].transform.localPosition.y, CreateUI[i].transform.localPosition.z);
                            flag[i] = 1;
                        }
                    }

                    if (flag[i] == 3)
                    {
                        CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition + new Vector3(SPEED * Time.deltaTime, 0.0f, 0.0f);

                        if (CreateUI[i].transform.localPosition.x > RIGHT_MAX)
                        {
                            CreateUI[i].transform.localPosition = new Vector3(RIGHT_MAX, CreateUI[i].transform.localPosition.y, CreateUI[i].transform.localPosition.z);
                            flag[i] = 0;
                        }
                    }

                    if (flag[i] == 1 && i != now_world)//選択中に未選択になったら
                    {
                        flag[i] = 3;
                    }
                }
            }
            else if (now_select == 1)//ステージ選択
            {

                if (flag[now_stage] == 0)//未選択の時
                {
                    flag[now_stage] = 2;
                }

                for (int i = 0; i < flag.Length; i++)
                {
                    if (flag[i] == 2)//選択中に移動
                    {
                        CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition - new Vector3(SPEED * Time.deltaTime, 0.0f, 0.0f);

                        if (CreateUI[i].transform.localPosition.x < LEFT_MAX)
                        {
                            CreateUI[i].transform.localPosition = new Vector3(LEFT_MAX, CreateUI[i].transform.localPosition.y, CreateUI[i].transform.localPosition.z);
                            flag[i] = 1;
                        }
                    }

                    if (flag[i] == 3)
                    {
                        CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition + new Vector3(SPEED * Time.deltaTime, 0.0f, 0.0f);

                        if (CreateUI[i].transform.localPosition.x > RIGHT_MAX)
                        {
                            CreateUI[i].transform.localPosition = new Vector3(RIGHT_MAX, CreateUI[i].transform.localPosition.y, CreateUI[i].transform.localPosition.z);
                            flag[i] = 0;
                        }
                    }

                    if (flag[i] == 1 && i != now_stage)//選択中に未選択になったら
                    {
                        flag[i] = 3;
                    }
                }
            }

            //=====================================================
            //スクロール
            //=====================================================
            if (now_select == 1)//ステージ選択
            {
                if (down <= now_stage && scroll_flag == 0)//上移動
                {
                    scroll_flag = 1;
                    move_max = 75.0f + 20.0f;
                    
                }

                if (up > now_stage && scroll_flag == 0)
                {
                    scroll_flag = 2;
                    move_max = 0.0f;
                    down--;//一番下をずらす
                    up--;
                }

                for (int i = 0; i < CreateUI.Length; i++)
                {
                    CreateUI[i].SetActive(true);

                    if (up > i || down < i)
                    {
                        CreateUI[i].SetActive(false);
                    }

                    
                }

                if (scroll_flag == 1)//上移動
                {

                    float speed = ((75.0f + 20.0f) / 0.2f) * Time.deltaTime;

                    for (int i = 0; i < CreateUI.Length; i++)
                    {
                        CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition + new Vector3(0.0f, speed, 0.0f);
                    }

                    move_max -= speed;

                    if (move_max <= 0.0f)
                    {
                        
                        scroll_flag = 0;
                        down++;//一番下をずらす
                        up++;
                    }
                }

                if (scroll_flag == 2)//下移動
                {

                    float speed = ((75.0f + 20.0f) / 0.2f) * Time.deltaTime;

                    for (int i = 0; i < CreateUI.Length; i++)
                    {
                        CreateUI[i].transform.localPosition = CreateUI[i].transform.localPosition - new Vector3(0.0f, speed, 0.0f);
                    }

                    move_max += speed;

                    if (move_max >= 75.0f + 20.0f)
                    {
                        
                        scroll_flag = 0;

                        
                    }
                }
            }
        }

    }

    
}