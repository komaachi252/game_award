using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static readonly int START_WORLD = 0;//初期ワールド　0スタート
    public static readonly int START_STAGE = 0;//初期ステージ　0スタート

    private const float INPUT_COOLTIME = 0.15f;

    public GameObject obje_feed;
    Feed script_feed;//フェード

    public int next_world;//次に選択されてるワールド
    private static int now_world;//現在選択してるワールド

    public int next_stage;//次に選択されているワールド
    private static int now_stage;//現在選択してるステージ

    private int one_read;//最初に一回だけ呼ばれるようにするフラグ

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
    }

    // Update is called once per frame
    void Update()
    {
        input_cooltime += Time.deltaTime;

        if (Input.GetKey(KeyCode.S))//下
        {
            if (select_flag == 0 && input_cooltime >= INPUT_COOLTIME && next_world < World_Stage_Nm.GET_WORLD_NUM() - 1)//ワールド選択画面
            {
                next_world++;
                input_cooltime = 0;
            }
            else if (select_flag == 1 && input_cooltime >= INPUT_COOLTIME && next_stage < World_Stage_Nm.GET_STAGE_NUM(now_world) - 1)//ステージ選択
            {
                next_stage++;
                input_cooltime = 0;
            }

        }

        if (Input.GetKey(KeyCode.W))//上
        {
            if (select_flag == 0 && input_cooltime >= INPUT_COOLTIME && next_world > 0)//ワールド選択画面
            {
                next_world--;
                input_cooltime = 0;
            }
            else if (select_flag == 1 && input_cooltime >= INPUT_COOLTIME && next_stage > 0)//ステージ選択
            {
                next_stage--;
                input_cooltime = 0;
            }
        }

        now_world = next_world;
        now_stage = next_stage;

        if (Input.GetKeyDown(KeyCode.Space))//決定
        {
            if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 1;
            }
            else if (select_flag == 1)//ステージ選択の時
            {
                SceneManager.LoadScene("GameScene");
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))//決定してたら一個前に戻る
        {
            if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 0;
                next_stage = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            script_feed.Start_Feed(1, 100.0f);
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
        return now_stage;
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


}
