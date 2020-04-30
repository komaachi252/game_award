using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public static readonly int START_WORLD = 0;//初期ワールド　0スタート
    public static readonly int START_STAGE = 0;//初期ステージ　0スタート

    public int next_world;//次に選択されてるワールド
    private static int now_world;//現在選択してるワールド

    public int next_stage;//次に選択されているワールド
    private static int now_stage;//現在選択してるステージ

    private int world_marking_num;//ワールド数
    private int[] stage_marking_num;//ステージ数

    private int one_read;//最初に一回だけ呼ばれるようにするフラグ

    private int select_flag;//今ステージ選択中かワールド選択中か

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
        //================================

        //ワールド数をカウントする
        GameObject WorldMarking = GameObject.Find("WorldMarking");
        foreach (Transform child in WorldMarking.transform)
        {
            world_marking_num++;
        }

        //================================

        stage_marking_num = new int[world_marking_num];//ステージメモリ確保

        GameObject StageMarking = GameObject.Find("StageMarking");
        for (int i = 0; i < world_marking_num; i++)
        {
            GameObject world = StageMarking.transform.GetChild(i).gameObject;//子供
            foreach (Transform transform in world.transform)//子供の数カウント
            {
                stage_marking_num[i]++;
            }
        }

        select_flag = 0;//ワールド選択から始める
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//進む
        {
            
            if (select_flag == 0 && next_world < world_marking_num - 1)//ワールド選択画面
            {
                next_world++;
            }
            else if (select_flag == 1)//ステージ選択
            {
                next_stage++;

                if (next_stage > stage_marking_num[now_world] - 1)//次のステージがステージ数を超えていたら
                {
                    next_stage = 0;//ループするようにする
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.A))//戻る
        {
            if (select_flag == 0 && next_world > 0)//ワールド選択画面
            {
                next_world--;
            }
            else if (select_flag == 1)//ステージ選択
            {
                next_stage--;

                if (next_stage < 0)//次のステージがステージ数を超えていたら
                {
                    next_stage = stage_marking_num[now_world] - 1;//ループするようにする
                }
            }
        }

        now_stage = next_stage;

        if (Input.GetKeyDown(KeyCode.Space))//決定
        {
            if (select_flag == 0)//ワールド選択の時
            {
                select_flag = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))//決定してたら一個前に戻る
        {
            if (select_flag == 1)//ステージ選択の時
            {
                select_flag = 0;
            }
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
