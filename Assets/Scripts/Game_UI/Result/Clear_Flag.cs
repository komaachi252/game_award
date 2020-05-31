using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Flag : MonoBehaviour
{
    public GameObject GameManeger;//ゲームマネージャーをもらう

    public GameObject Result;//リザルトのゲームオブジェクト

    //フラグ
    //0 = 非表示
    //1 = 表示(最初の一回だけ)
    //2 = 表示(常時)
    private int flag;//フラグ

    //=============================================================
    //レンダラーモード変更するやつ
    //=============================================================
    private const float CAMERA_RANGE = 50.0f;
    public Camera Main_Camera;//メインカメラ
    public Canvas Game_Canvas;//ゲームで使ってるキャンバス

    // Start is called before the first frame update
    void Start()
    {
        Result.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (flag == 0 && GameManeger.GetComponent<Game_Manager>().Is_Clear_Flag == true)
        {
            Result.SetActive(true);
            flag = 2;

            Game_Canvas.renderMode = RenderMode.ScreenSpaceCamera;//カメラを変更
            Game_Canvas.worldCamera = Main_Camera;//設定カメラをメインカメラに変更
            Game_Canvas.planeDistance = CAMERA_RANGE;

            Debug.Log(Game_Canvas.renderMode);
        }

        if (Input.GetKeyDown(KeyCode.L))//Lでクリアにする
        {
            GameManeger.GetComponent<Game_Manager>().Game_Clear();
        }
    }
}
