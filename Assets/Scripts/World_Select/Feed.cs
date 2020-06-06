using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feed : MonoBehaviour
{
    public const int FEED_IN = 0;
    public const int FEED_OUT = 1;

    private Image image;//フェードするやつ
    private float alfa;//アルファ

    private float speed;//フェードスピード

    //フラグ
    //0 = フェード待ち
    //1 = フェード中　イン
    //2 = フェード中　アウト
    public int flag;


    // Start is called before the first frame update
    void Start()
    {
        image = this.gameObject.GetComponent<Image>();//イメージ貰う
        alfa = image.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 1)
        {
            image.color = new Color(0.0f, 0.0f, 0.0f, alfa);
            alfa -= speed;

            if (alfa <= 0.0f)
            {
                flag = 0;
                image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            }
        }

        if (flag == 2)//フェードアウト
        {
            image.color = new Color(0.0f, 0.0f, 0.0f, alfa);
            alfa += speed;

            if (alfa >= 1.0f)//真っ暗になったら
            {
                flag = 0;//フェードを止める
                image.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
    }

    //フェードをスタートさせる関数
    //feed_mode = 0だとフェードイン  1だとフェードアウト
    //feed_speed = フェード時間
    public void Start_Feed(int feed_mode,float feed_speed)
    {
        if (feed_mode == 0)//フェードイン
        {
            flag = 1;
            alfa = 1.0f;
        }

        if (feed_mode == 1)//フェードアウト
        {
            flag = 2;//フラグをセット
            alfa = 0;
        }

        speed = (255 / feed_speed) * Time.deltaTime;//フェード速度をセット


    }

    //現在のフェード状態
    //false = フェードしてない
    //true = フェードしてる
    public bool Feed_State()
    {
        if (flag == 0)
        {
            return false;
        }

        return true;
    }
}
