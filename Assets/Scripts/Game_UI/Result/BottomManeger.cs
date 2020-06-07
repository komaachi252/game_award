using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottomManeger : MonoBehaviour
{

    private const int SELECT = 0;
    private const int RETURN = 1;
    private const int NEXT = 2;

    public Game_Fade fade;//フェード


    public GameObject Select;//ワールドセレクトに移動するボタン
    public GameObject Return;//ステージをもう一度やるボタン
    public GameObject Next;//次のステージに進むボタン

    //0　＝　下の丸いやつ
    //1　＝　ロゴ
    private Image[] image_Select = new Image[2];
    private Image[] image_Return = new Image[2];
    private Image[] image_Next = new Image[2];

    //ボタンフラグ
    //0　＝　ワールド選択画面に移動
    //1　＝　同じステージをもう一度やる
    //2　＝　次のステージにすすむ
    private int now_bottom = 0;

    public Sprite Bottom_up;//ボタン押してない
    public Sprite Bottom_down;//ボタン押してる

    private int stick_flag = 0;//スティックフラグ


    // Start is called before the first frame update
    void Start()
    {
        //=========================================================
        //Imageを受け取る
        //=========================================================
        image_Select[0] = Select.GetComponent<Image>();
        image_Select[1] = Select.transform.GetChild(0).gameObject.GetComponent<Image>();

        image_Return[0] = Return.GetComponent<Image>();
        image_Return[1] = Return.transform.GetChild(0).gameObject.GetComponent<Image>();

        image_Next[0] = Next.GetComponent<Image>();
        image_Next[1] = Next.transform.GetChild(0).gameObject.GetComponent<Image>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))//左に移動
        {
            now_bottom--;

            if (now_bottom <= SELECT)//今のボタンがSELECTの場合
            {
                now_bottom = SELECT;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            now_bottom++;

            if (now_bottom >= NEXT)//今のボタンがMAXの場合
            {
                now_bottom = NEXT;
            }
        }

        if (Input.GetKey(KeyCode.Return))
        {
            Dicision2();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            Dicision();
        }

        Bottom_touch();
    }

    //ボタンの状態を変更する
    private void Bottom_touch()
    {
        for (int i = 0; i < 2; i++)
        {
            if (SELECT == now_bottom)//セレクトの場合
            {
                image_Select[i].color = new Color(image_Select[i].color.r, image_Select[i].color.g, image_Select[i].color.b, 1.0f);
            }
            else
            {
                image_Select[i].color = new Color(image_Select[i].color.r, image_Select[i].color.g, image_Select[i].color.b, 0.5f);
            }


            if (RETURN == now_bottom)//繰り返しの場合
            {
                image_Return[i].color = new Color(image_Return[i].color.r, image_Return[i].color.g, image_Return[i].color.b, 1.0f);
            }
            else
            {
                image_Return[i].color = new Color(image_Return[i].color.r, image_Return[i].color.g, image_Return[i].color.b, 0.5f);
            }


            if (NEXT == now_bottom)//次に進む場合
            {
                image_Next[i].color = new Color(image_Next[i].color.r, image_Next[i].color.g, image_Next[i].color.b, 1.0f);
            }
            else
            {
                image_Next[i].color = new Color(image_Next[i].color.r, image_Next[i].color.g, image_Next[i].color.b, 0.5f);
            }

        }
    }

    private void Dicision()
    {
        if (SELECT == now_bottom)
        {
            fade.Fade_Start(20, true, "WorldScene");
            
        }

        if (RETURN == now_bottom)
        {
            Time.timeScale = 1.0f;
            fade.Fade_Start(20, true, "GameScene", "PauseScene");
        }

        if (NEXT == now_bottom)
        {
            StageController.Set_nextstage();

            fade.Fade_Start(20, true, "GameScene", "PauseScene");
        }
    }

    private void Dicision2()
    {
        if (SELECT == now_bottom)
        {
            image_Select[0].sprite = Bottom_down;
        }
        else
        {
            image_Select[0].sprite = Bottom_up;
        }

        if (RETURN == now_bottom)
        {
            image_Return[0].sprite = Bottom_down;
        }
        else
        {
            image_Return[0].sprite = Bottom_up;
        }

        if (NEXT == now_bottom)
        {
            image_Next[0].sprite = Bottom_down;
        }
        else
        {
            image_Next[0].sprite = Bottom_up;
        }
    }
}
