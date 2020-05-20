using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class World_Stage_Nm : MonoBehaviour
{

    private const int WORLD_NUM = 5;//ワールド数
    private static readonly int[] STAGE_NUM = { 10, 10, 10, 10, 10 };//ステージ数

    void Awake()
    { 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //ワールド数を受け取るやぁつ
    public static int GET_WORLD_NUM()
    {
        return WORLD_NUM;
    }

    //ステージ数を受け取るやつ
    //●引数
    //stage_number = ステージ数が欲しいワールドの番号
    //●戻り値
    //指定したワールドにステージ数が登録されてなかったら -1 が返ってくる
    //登録されていたらその値が返ってくる
    public static int GET_STAGE_NUM(int world_number)
    {
        if (world_number >= STAGE_NUM.Length)
        {
            return -1;
        }
        return STAGE_NUM[world_number];
    }
}
