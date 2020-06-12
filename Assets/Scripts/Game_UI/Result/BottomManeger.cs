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
    //-1 ＝　ボタン受け付けない
    //0　＝　ワールド選択画面に移動
    //1　＝　同じステージをもう一度やる
    //2　＝　次のステージにすすむ
    
    private int now_bottom = 0;

    public Sprite Bottom_up;//ボタン押してない
    public Sprite Bottom_down;//ボタン押してる

    //パッドのフラグ
    //入力フラグ
    //0 = 未入力
    //1 = 上
    //2 = 下
    //3 = 入力中
    private int pad_flag = 0;//スティックフラグ


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
        if (now_bottom == -1)
        {
            return;
        }

        //===========================================================
        //パッド処理
        //===========================================================
        float y_axis = Input.GetAxis("Horizontal2");//スティック
        float arrow_axis = Input.GetAxis("Horizontal_Arrow");//パッド

        if (y_axis > 0.0f || arrow_axis < 0.0f)//上
        {
            if (pad_flag == 0 || pad_flag == 2)
            {
                pad_flag = 1;
            }
            else if (pad_flag == 1)
            {
                pad_flag = 3;
            }
        }
        else if (y_axis < 0.0f || arrow_axis > 0.0f)//下
        {
            if (pad_flag == 0 || pad_flag == 1)
            {
                pad_flag = 2;
            }
            else if (pad_flag == 2)
            {
                pad_flag = 3;
            }
        }
        else
        {
            pad_flag = 0;
        }

        //==========================================================
        //入力処理
        //==========================================================

        if (Input.GetKeyDown(KeyCode.A) || pad_flag == 1)//左に移動
        {
            now_bottom--;

            if (now_bottom <= SELECT)//今のボタンがSELECTの場合
            {
                now_bottom = SELECT;
            }

            FindObjectOfType<Audio_Manager>().Play("select");
        }

        if (Input.GetKeyDown(KeyCode.D) || pad_flag == 2)
        {
            now_bottom++;

            if (now_bottom >= NEXT)//今のボタンがMAXの場合
            {
                now_bottom = NEXT;
            }
            FindObjectOfType<Audio_Manager>().Play("select");
        }

        if (Input.GetKey(KeyCode.Return) || Input.GetKey("joystick button 0"))
        {
            

            Dicision2();

        }
        else if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp("joystick button 0"))
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
            now_bottom = -1;
            FindObjectOfType<Audio_Manager>().Play("enter");
        }
        else if (RETURN == now_bottom)
        {
            Time.timeScale = 1.0f;
            fade.Fade_Start(20, true, "GameScene", "PauseScene");
            now_bottom = -1;
            FindObjectOfType<Audio_Manager>().Play("enter");
        }
        else if(NEXT == now_bottom)
        {
            if (StageController.Get_Index() < 49)
            {
                StageController.Set_nextstage();

                fade.Fade_Start(20, true, "GameScene", "PauseScene");
                now_bottom = -1;
                FindObjectOfType<Audio_Manager>().Play("enter");
            }
            
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
            if (StageController.Get_Index() < 49)
            {
                image_Next[0].sprite = Bottom_down;
            }
            else
            {
                FindObjectOfType<Audio_Manager>().Play("botton_notpress");
            }
        }
        else
        {
            image_Next[0].sprite = Bottom_up;
        }

        
        
    }
}
