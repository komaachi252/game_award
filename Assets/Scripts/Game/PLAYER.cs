using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public SOLID SOLID;
    public AQUA AQUA;
    public CLOUD CLOUD;
    PhysicMaterial PM;
    public int TYPE = 0;
    public int MOVE_D = 1;
    float STARTPOINT = 0;
    public int MOVE_NOW = 0;
    public int STAND = 0;       //接地フラグ
    public int STAND_T = 0;     //頭上接触フラグ
    public int STAND_U = 0;     //足元接触フラグ
    public int stay_HOT_R = 0;  //仮HOT接触フラグ右側
    public int stay_HOT_L = 0;  //仮HOT接触フラグ左側
    public int stay_HOT = 0;    //HOT完全接触フラグ
    public int stay_COLD_R = 0; //仮COKD接触フラグ右側
    public int stay_COLD_L = 0; //仮COLD接触フラグ左側
    public int stay_COLD = 0;   //COLD完全接触フラグ

    int exchangecount = 0;  //状態変化演出カウント

    Color[] colors = new Color[3] { new Color(0.1f, 0.2f, 0.3f, 1.0f),
                                    new Color(0.0f, 0.5f, 0.5f, 1.0f),
                                    new Color(1.0f, 1.0f, 1.0f, 1.0f),};

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = colors[TYPE];
        PM = this.gameObject.GetComponent<PhysicMaterial>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && MOVE_NOW == 0 && exchangecount == 0)
        {
            Debug.Log("入力");
            if(stay_HOT==1)
            {
                exchangecount = 61;
                if (TYPE < 2)
                {
                    Debug.Log("加熱");
                    TYPE++;
                    if(TYPE==1)
                    {
                        SOLID.exchange_s();
                        AQUA.exchange_b();
                        tag = "AQUA";
                    }
                    if(TYPE==2)
                    {
                        AQUA.exchange_s();
                        CLOUD.exchange_b();
                        /*
                        tag = "CLOUD";
                        Debug.Log("浮上");
                        Physics.gravity = new Vector3(0, 9.8f, 0);
                        */
                    }
                }
            }

            if (stay_COLD == 1)
            {
                exchangecount = 61;
                if (TYPE > 0)
                {
                    Debug.Log("冷却");
                    TYPE--;
                    if (TYPE == 0)
                    {
                        AQUA.exchange_s();
                        SOLID.exchange_b();
                        /*
                        tag = "SOLID";
                        Debug.Log("降下");
                        Physics.gravity = new Vector3(0, -9.8f, 0);
                        */
                    }
                    if (TYPE == 1)
                    {
                        CLOUD.exchange_s();
                        AQUA.exchange_b();
                        tag = "AQUA";                        
                    }
                }
            }

            //GetComponent<Renderer>().material.color = colors[TYPE];
        }

        if (Input.GetKey(KeyCode.RightArrow) && MOVE_NOW == 0 && exchangecount == 0 && STAND == 1)      //移動中でなく右キーが押されたら
        {
            MOVE_NOW = 25;
            MOVE_D = 1;
            STARTPOINT = transform.position.x;                      //誤差修正の為移動開始地点を記録
            STAND = 0;
            STAND_T = 0;
            STAND_U = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && MOVE_NOW == 0 && exchangecount == 0 && STAND == 1)      //移動中でなく左キーが押されたら
        {
            MOVE_NOW = 25;
            MOVE_D = -1;
            STARTPOINT = transform.position.x;                      //誤差修正の為移動開始地点を記録
            STAND = 0;
            STAND_T = 0;
            STAND_U = 0;
        }

        if (MOVE_NOW > 0)
        {
            MOVE_NOW--;

            Vector3 pos = transform.position;
            pos.x += 0.04f * MOVE_D;
            transform.position = pos;
            if(MOVE_NOW==0)                                         //移動完了なら
            {
                pos.x = STARTPOINT +1 * MOVE_D;
                transform.position = pos;                           //誤差を整数加算により補正
            }
        }

        if(exchangecount>0)
        {
            exchangecount--;

            if(exchangecount==0)
            {
                if (TYPE == 0)
                {
                    tag = "SOLID";
                    Debug.Log("降下");
                    Physics.gravity = new Vector3(0, -9.8f, 0);
                    STAND = 0;
                }

                if (TYPE==2)
                {
                    tag = "CLOUD";
                    Debug.Log("浮上");
                    Physics.gravity = new Vector3(0, 9.8f, 0);
                    STAND = 0;
                }
            }
        }

        if((TYPE == 0 || TYPE == 1) && MOVE_NOW == 0)
        {
            if(STAND_U==1)
            {
                STAND = 1;
            }
        }

        if (TYPE == 2 && MOVE_NOW == 0)
        {
            if (STAND_T == 1)
            {
                STAND = 1;
            }
        }
    }

    //接地（面）関係　（追加予定）
    public void SETSTAND()
    {
        STAND = 1;
    }

    public void CLEARSTAND()
    {
        STAND = 0;
    }

    //加熱関係
    public void SET_stayHOT_R()
    {
        stay_HOT_R = 1;
        if (stay_HOT_L == 1)    //どっちも接触してれば
        {
            stay_HOT = 1;
        }
    }

    public void SET_stayHOT_L()
    {
        stay_HOT_L = 1;
        if (stay_HOT_R == 1)  //どっちも接触してれば 
        {
            stay_HOT = 1;
        }
    }

    public void CLEAR_stayHOT()
    {
        stay_HOT = 0;
    }

    //冷却関係
    public void SET_stayCOLD_R()
    {
        stay_COLD_R = 1;
        if (stay_COLD_L == 1)    //どっちも接触してれば
        {
            stay_COLD = 1;
        }
    }

    public void SET_stayCOLD_L()
    {
        stay_COLD_L = 1;
        if (stay_COLD_R == 1)  //どっちも接触してれば 
        {
            stay_COLD = 1;
        }
    }

    public void CLEAR_stayCOLD()
    {
        stay_COLD = 0;
    }

    public void SET_STAND_T()
    {
        if (MOVE_NOW == 0)
        {
            STAND_T = 1;
        }
    }

    public void SET_STAND_U()
    {
        if(MOVE_NOW == 0)
        {
            STAND_U = 1;
        }       
    }

    public void CLARE_STAND()
    {
        STAND = 0;
    }
}
