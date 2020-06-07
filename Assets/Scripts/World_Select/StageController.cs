using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static readonly int START_WORLD = 0;//初期ワールド　0スタート
    public static readonly int START_STAGE = 0;//初期ステージ　0スタート

    private const float INPUT_COOLTIME = 0.3f;

    public GameObject obje_feed;
    Feed script_feed;//フェード

    public int next_world;//次に選択されてるワールド
    private static int now_world;//現在選択してるワールド

    public int next_stage;//次に選択されているワールド
    private static int now_stage;//現在選択してるステージ

    private int one_read;//最初に一回だけ呼ばれるようにするフラグ

    //フラグ
    //0 = ワールド
    //1 = ステージ
    //2 = ゲームシーンに移動
    //3 = タイトルに戻る
    private int select_flag;//今ステージ選択中かワールド選択中か

    private float input_cooltime;//入力クールタイム

    //スタートより早く呼ばれるらしい
    void Awake()
    {
        if (one_read == 0)//一回だけ実行
        {
            //初期化
            next_world = START_WORLD;
            now_world = START_WORLD;


            next_stage = START_STAGE;
            now_stage = START_STAGE;

            select_flag = 0;
        }
    }

    //これに入ってるオブジェクトが破壊されるタイミングで呼ばれるらしい
    void OnDestroy()
    {
        one_read = 1;//一回きりフラグ
    }

    // Start is called before the first frame update
    void Start()
    {

        select_flag = 0;//ワールド選択から始める

        script_feed = obje_feed.GetComponent<Feed>();//フェードのスクリプト貰う

        script_feed.Start_Feed(0, 250.0f);//フェード開始
    }

    // Update is called once per frame
    void Update()
    {
        input_cooltime += Time.deltaTime;//クールタイムカウント

        


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

        if (select_flag == 2 && script_feed.Feed_State() == false)//シーン移動処理
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
        }

        if (select_flag == 3 && script_feed.Feed_State() == false)//シーン移動処理
        {
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
            }
            else if (select_flag == 1 && input_cooltime >= INPUT_COOLTIME && next_stage > 0)//ステージ選択
            {
                next_stage--;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
            
        }

        if (y_axis < 0.0f || arrow_axis > 0)//スティックが下
        {
            if (select_flag == 0 && input_cooltime >= INPUT_COOLTIME && next_world < World_Stage_Nm.GET_WORLD_NUM() - 1)//ワールド選択画面
            {
                next_world++;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
            else if (select_flag == 1 && input_cooltime >= INPUT_COOLTIME && next_stage < World_Stage_Nm.GET_STAGE_NUM(now_world) - 1)//ステージ選択
            {
                next_stage++;
                input_cooltime = 0;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
            
        }

        if (Input.GetKeyDown("joystick button 0"))//決定A
        {
            if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 1;
            }
            else if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 2;
                script_feed.Start_Feed(1, 270.0f);//フェード開始
            }
            FindObjectOfType<Audio_Manager>().Play("enter");
        }

        if (Input.GetKeyDown("joystick button 2"))//戻るX
        {
            if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 0;
                next_stage = 0;
            }
            else if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 3;
                script_feed.Start_Feed(1, 270.0f);//フェード開始
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
            }
            else if (select_flag == 1 && next_stage < World_Stage_Nm.GET_STAGE_NUM(now_world) - 1)//ステージ選択
            {
                next_stage++;
                input_cooltime = 0;
            }

            FindObjectOfType<Audio_Manager>().Play("select");

        }

        if (Input.GetKeyDown(KeyCode.W))//上
        {
            if (select_flag == 0 && next_world > 0)//ワールド選択画面
            {
                next_world--;
                input_cooltime = 0;
            }
            else if (select_flag == 1 && next_stage > 0)//ステージ選択
            {
                next_stage--;
                input_cooltime = 0;
            }
            FindObjectOfType<Audio_Manager>().Play("select");
        }

        if (Input.GetKeyDown(KeyCode.Return))//決定
        {
            if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 1;
            }
            else if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 2;
                script_feed.Start_Feed(1, 270.0f);//フェード開始

            }
            FindObjectOfType<Audio_Manager>().Play("enter");
        }

        if (Input.GetKeyDown(KeyCode.Z))//決定してたら一個前に戻る
        {
            if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 0;
                next_stage = 0;
            }
            else if (select_flag == 0)
            {
                select_flag = 3;
                script_feed.Start_Feed(1, 270.0f);//フェード開始
            }
            FindObjectOfType<Audio_Manager>().Play("cancel");
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
