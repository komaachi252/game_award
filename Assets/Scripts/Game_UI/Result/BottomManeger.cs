using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BottomManeger : MonoBehaviour
{
    private enum BOTTOM_ID
    {
        SELECT,
        RETURN,
        NEXT,
        MAX
    }


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

            if (now_bottom <= (int)BOTTOM_ID.SELECT)//今のボタンがSELECTの場合
            {
                now_bottom = (int)BOTTOM_ID.SELECT;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            now_bottom++;

            if (now_bottom >= (int)BOTTOM_ID.NEXT)//今のボタンがMAXの場合
            {
                now_bottom = (int)BOTTOM_ID.NEXT;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Dicision2();
        }

        if (Input.GetKeyUp(KeyCode.Space))
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
            if ((int)BOTTOM_ID.SELECT == now_bottom)//セレクトの場合
            {
                image_Select[i].color = new Color(image_Select[i].color.r, image_Select[i].color.g, image_Select[i].color.b, 1.0f);
            }
            else
            {
                image_Select[i].color = new Color(image_Select[i].color.r, image_Select[i].color.g, image_Select[i].color.b, 0.5f);
            }


            if ((int)BOTTOM_ID.RETURN == now_bottom)//繰り返しの場合
            {
                image_Return[i].color = new Color(image_Return[i].color.r, image_Return[i].color.g, image_Return[i].color.b, 1.0f);
            }
            else
            {
                image_Return[i].color = new Color(image_Return[i].color.r, image_Return[i].color.g, image_Return[i].color.b, 0.5f);
            }


            if ((int)BOTTOM_ID.NEXT == now_bottom)//次に進む場合
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
        if ((int) BOTTOM_ID.SELECT == now_bottom)
        {
            SceneManager.LoadScene("WorldScene");
        }

        if ((int)BOTTOM_ID.RETURN == now_bottom)
        {
            SceneManager.LoadScene("GameScene");
        }

        if ((int)BOTTOM_ID.NEXT == now_bottom)
        {
            StageController.Set_nextstage();
            SceneManager.LoadScene("GameScene");
        }
    }

    private void Dicision2()
    {
        if ((int)BOTTOM_ID.SELECT == now_bottom)
        {
            image_Select[0].sprite = Bottom_down;
        }
        else
        {
            image_Select[0].sprite = Bottom_up;
        }

        if ((int)BOTTOM_ID.RETURN == now_bottom)
        {
            image_Return[0].sprite = Bottom_down;
        }
        else
        {
            image_Return[0].sprite = Bottom_up;
        }

        if ((int)BOTTOM_ID.NEXT == now_bottom)
        {
            image_Next[0].sprite = Bottom_down;
        }
        else
        {
            image_Next[0].sprite = Bottom_up;
        }
    }
}
