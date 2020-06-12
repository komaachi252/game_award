using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    private const float INPUT_COOLTIME = 0.3f;

    public GameObject obje_feed;
    Feed script_feed;//フェード

    public int next_world;//次に選択されてるワールド
    private static int now_world;//現在選択してるワールド

    public int next_stage;//次に選択されているワールド
    private static int now_stage;//現在選択してるステージ

    public static int one_read;//最初に一回だけ呼ばれるようにするフラグ

    //フラグ
    //0 = ワールド
    //1 = ステージ
    //2 = ゲームシーンに移動
    //3 = タイトルに戻る
    public static int select_flag;//今ステージ選択中かワールド選択中か

    //フラグ
    //0 = キー入力できない
    //1 = キー入力できる
    public int key_flag { get; set; }

    private float input_cooltime;//入力クールタイム

    //スタートより早く呼ばれるらしい
    void Awake()
    {
        
    }

    //これに入ってるオブジェクトが破壊されるタイミングで呼ばれるらしい
    void OnDestroy()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        next_stage = now_stage;
        next_world = now_world;

        key_flag = 0;

        script_feed = obje_feed.GetComponent<Feed>();//フェードのスクリプト貰う
    }

    // Update is called once per frame
    void Update()
    {

        input_cooltime += Time.deltaTime;//クールタイムカウント



        if (key_flag == 1)
        {
            //===============================================
            //キーボード入力処理
            //===============================================
            Key_input();


            //===============================================
            //パッド入力処理
            //===============================================
            pad_input();

            //===============================================
            //その他いろいろ
            //===============================================
            now_world = next_world;
            now_stage = next_stage;
        }
       


        

        if (select_flag == 2 && script_feed.Feed_State() == false)//シーン移動処理
        {
            select_flag = 1;

            SceneManager.LoadScene("GameScene");
            SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);

            
        }
        else if (select_flag == 3 && script_feed.Feed_State() == false)//シーン移動処理
        {
            select_flag = 0;
            SceneManager.LoadScene("TitleScene");
            
        }

    }

    //=========================================================
    //パッド入力処理
    //=========================================================
    private void pad_input()
    {
        //float x_axis = Input.GetAxis("Horizontal");//右プラス　左マイナス
        float y_axis = Input.GetAxis("Vertical");//上プラス　下マイナス
        float arrow_axis = Input.GetAxis("Vertical_Arrow");

        if (y_axis > 0.0f || arrow_axis < 0)//スティックを上に傾けたら
        {
            if (select_flag == 0 && input_cooltime >= INPUT_COOLTIME && next_world > 0)//ワールド選択画面
            {
                next_world--;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (select_flag == 1 && input_cooltime >= INPUT_COOLTIME && next_stage > 0)//ステージ選択
            {
                next_stage--;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (next_world < 1 || next_stage < 1)
            {
                FindObjectOfType<Audio_Manager>().Play("botton_notpress");

            }
        }
        else if (y_axis < 0.0f || arrow_axis > 0)//スティックが下
        {
            if (select_flag == 0 && input_cooltime >= INPUT_COOLTIME && next_world < World_Stage_Nm.GET_WORLD_NUM() - 1)//ワールド選択画面
            {
                next_world++;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (select_flag == 1 && input_cooltime >= INPUT_COOLTIME && next_stage < World_Stage_Nm.GET_STAGE_NUM(now_world) - 1)//ステージ選択
            {
                next_stage++;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (next_world > World_Stage_Nm.GET_WORLD_NUM() - 2 || next_stage > World_Stage_Nm.GET_STAGE_NUM(now_world) - 2)
            {
                FindObjectOfType<Audio_Manager>().Play("botton_notpress");

            }
        }


        if (Input.GetKeyDown("joystick button 0"))//決定
        {
            if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 1;
                key_flag = 0;
            }
            else if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 2;
                script_feed.Start_Feed(1, 30.0f);//フェード開始
                key_flag = 0;
            }
            FindObjectOfType<Audio_Manager>().Play("enter");
        }
        else if (Input.GetKeyDown("joystick button 1"))//戻る
        {
            if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 0;
                next_stage = 0;
                key_flag = 0;
            }
            else if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 3;
                script_feed.Start_Feed(1, 30.0f);//フェード開始
                key_flag = 0;
            }

            FindObjectOfType<Audio_Manager>().Play("cancel");
        }



        
    }

    //=========================================================
    //キーボード入力
    //=========================================================
    private void Key_input()
    {
        if (Input.GetKeyDown(KeyCode.S))//下
        {
            if (select_flag == 0 && next_world < World_Stage_Nm.GET_WORLD_NUM() - 1)//ワールド選択画面
            {
                next_world++;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (select_flag == 1 && next_stage < World_Stage_Nm.GET_STAGE_NUM(now_world) - 1)//ステージ選択
            {
                next_stage++;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if(next_world > World_Stage_Nm.GET_WORLD_NUM() - 2 || next_stage > World_Stage_Nm.GET_STAGE_NUM(now_world) - 2)
            {
                FindObjectOfType<Audio_Manager>().Play("botton_notpress");
                
            }


        }
        else if (Input.GetKeyDown(KeyCode.W))//上
        {
            if (select_flag == 0 && next_world > 0)//ワールド選択画面
            {
                next_world--;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (select_flag == 1 && next_stage > 0)//ステージ選択
            {
                next_stage--;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
                key_flag = 0;
            }
            else if (next_world < 1 || next_stage < 1)
            {
                FindObjectOfType<Audio_Manager>().Play("botton_notpress");
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))//決定
        {
            if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 1;
                key_flag = 0;
            }
            else if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 2;
                script_feed.Start_Feed(1, 30.0f);//フェード開始
                key_flag = 0;

            }
            FindObjectOfType<Audio_Manager>().Play("enter");
        }
        else if (Input.GetKeyDown(KeyCode.Z))//決定してたら一個前に戻る
        {
            if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 0;
                next_stage = 0;
                key_flag = 0;
            }
            else if (select_flag == 0)
            {
                select_flag = 3;
                script_feed.Start_Feed(1, 30.0f);//フェード開始
                key_flag = 0;
            }
            FindObjectOfType<Audio_Manager>().Play("cancel");
        }


        if (Input.GetKeyDown(KeyCode.Escape))//エスケープキーで終了処理
        {
            Application.Quit();
        }
    }


    //現在選択してるワールドをあげる関数
    public static int Get_world()
    {
        return now_world;
    }


    //現在選択してるステージをあげる関数
    public static int Get_stage()
    {
        int stage = 0;
        for (int i = 0; i < now_world - 1; i++)
        {
            stage += World_Stage_Nm.GET_STAGE_NUM(i);
        }
        return now_stage + stage;//全部繋げたやぁつ
    }

    public static int Get_Index()
    {
        return now_world * 10 + now_stage;
    }
    //次のステージに進む
    public static void Set_nextstage()
    {
        now_stage++;

        if (World_Stage_Nm.GET_STAGE_NUM(now_world) <= now_stage)//ワールドのステージ数より増えていたら
        {
            now_stage = 0;//ステージを0にする
            now_world++;//ワールドを次に進める
        }

        DontDestroyManager.IndexUpdate();//インデックスの更新

        Debug.Log("今のステージ " + now_stage + "\n今のワールド " + now_world);

    }


    //次に選択されるワールドをあげる関数
    public int Get_nextworld()
    {
        return next_world;
    }

    //次に選択されるステージをあげる関数
    public int Get_nextstage()
    {
        return next_stage;
    }

    //ワールドを選択してるかステージを選択してるか
    public int Get_SelectFlag()
    {
        return select_flag;
    }

    //現在選択してるワールドの値を変更する関数
    public void Set_world(int set)
    {
        now_world = set;
    }


    //現在選択してるステージの値を変更する関数
    public void Set_stage(int set)
    {
        now_stage = set;
    }
}
