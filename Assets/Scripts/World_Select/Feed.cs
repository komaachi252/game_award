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
    int flag;


    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();//イメージ貰う
        alfa = image.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 2)//フェードアウト
        {
            image.color = image.color + new Color(0.0f, 0.0f, 0.0f, alfa);
        }

        

    }

    //フェードをスタートさせる関数
    //feed_mode = 0だとフェードイン  1だとフェードアウト
    //feed_speed = フェード時間
    public void Start_Feed(int feed_mode,float feed_speed)
    {
        if (feed_mode == 0)//フェードイン
        {

        }
        else if (feed_mode == 1)//フェードアウト
        {
            flag = 2;//フラグをセット
            speed = (255 / feed_speed) * Time.deltaTime;//フェード速度をセット
            image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }

        
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
        else
        {
            return true;
        }
    }
}
