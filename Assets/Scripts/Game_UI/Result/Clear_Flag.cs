using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear_Flag : MonoBehaviour
{
    public Game_Manager GameManeger;//ゲームマネージャーをもらう

    private StarManeger star;//星制御するやつ

    //フラグ
    //0 = 非表示
    //1 = 表示(最初の一回だけ)
    //2 = 表示(常時)
    private int flag;//フラグ

    //=============================================================
    //レンダラーモード変更するやつ
    //=============================================================
    private const float CAMERA_RANGE = 2.0f;
    public Camera Main_Camera;//メインカメラ
    public Canvas Game_Canvas;//ゲームで使ってるキャンバス

    void Awake()
    {
        //SceneManager.UnloadSceneAsync("ResultScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
        {

            //読み込まれているシーンを取得し、リザルトシーンの有無を判定
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name;
            if (sceneName == "ResultScene")
            {
                SceneManager.UnloadSceneAsync("ResultScene");//リザルトシーン削除
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (flag == 0 && GameManeger.Is_Clear_Flag == true)
        {
            //Result.SetActive(true);
            flag = 2;

            Game_Canvas.renderMode = RenderMode.ScreenSpaceCamera;//カメラを変更
            Game_Canvas.worldCamera = Main_Camera;//設定カメラをメインカメラに変更
            Game_Canvas.planeDistance = CAMERA_RANGE;

            //Debug.Log(Game_Canvas.renderMode);

            //star.Start_Star_Anime();

            SceneManager.LoadScene("ResultScene", LoadSceneMode.Additive);//リザルトシーンを読み込む(加算)
        }

        //if (Input.GetKeyDown(KeyCode.L))//Lでクリアにする
        //{
        //    GameManeger.GetComponent<Game_Manager>().Game_Clear();
        //}
    }
}
