using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    //public PLAYERCAMERA PLAYERCAMERA;
    public SOLID SOLID;
    public AQUA AQUA;
    public CLOUD CLOUD;
    public JUGEMOVE JUGEMOVE;
    PhysicMaterial PM;
    Rigidbody rb;
    Collider boxcollider;
    Collider capcollider;
    public int posx = 0;
    public int posy = 0;
    public int B_posx = 0;
    public int B_posy = 0;
    public int TYPE = 0;
    public int MOVE_D = 1;
    public int MOVE_V = 1;
    public int B_MOVE_D = 1;
    public int VMOVEflag = 0;   //縦移動予約フラグ
    public int AUTOMOVEflag = 0;    //強制移動フラグ
    public int AUTOMOVEflag2 = 0;   //強制移動フラグ２
    public float VMOVEPOS = 0;
    public float MOVEPOS = 0;        //強制移動目標地点
    public int MOVEFinish = 0;      //強制移動完了フラグ
    public int TIME_T = 0;          //GAP通過用カウンタ
    public int GAPMOVE = 0;         //GAP通過フラグ
    public float TARGETV = 0;         //

    //float STARTPOINT = 0;
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
    public int stay_GAP = 0;    //GAP接触フラグ
    public int stay_HARDHOT = 0;//HARDHOTフラグ
    public int stay_HARDCOLD = 0;//HARDCOLDフラグ

    int exchangecount = 0;  //状態変化演出カウント

    Color[] colors = new Color[3] { new Color(0.1f, 0.2f, 0.3f, 1.0f),
                                    new Color(0.0f, 0.5f, 0.5f, 1.0f),
                                    new Color(1.0f, 1.0f, 1.0f, 1.0f),};

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = colors[TYPE];
        rb = this.GetComponent<Rigidbody>();
        PM = this.gameObject.GetComponent<PhysicMaterial>();
        boxcollider = this.gameObject.GetComponent<BoxCollider>();
        capcollider = this.gameObject.GetComponent<CapsuleCollider>();
        tag = "SOLID";
        Physics.gravity = new Vector3(0, -9.8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MOVE_NOW == 0 && exchangecount == 0)
        {
            Debug.Log("入力");
            if (stay_HOT == 1)
            {
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                exchangecount = 61;
                if (TYPE < 2)
                {
                    Debug.Log("加熱");
                    TYPE++;
                    if (TYPE == 1)
                    {
                        SOLID.exchange_s();
                        AQUA.exchange_b();
                        tag = "AQUA";
                    }
                    if (TYPE == 2)
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
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
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

            if (stay_GAP == 1 && TYPE == 1 && MOVE_V == 1) //水でGAPの上にいるとき
            {
                //capcollider.enabled = false;
                Debug.Log(transform.position);
                MOVEPOS = JUGEMOVE.GET_JUGEPOS();   //目標地点Xを判定マスと同じにする

                if (transform.position.x > MOVEPOS)
                {
                    MOVE_D = -1;
                }
                else
                {
                    MOVE_D = 1;
                }

                AUTOMOVEflag2 = 1;
                Debug.Log(transform.position.x);
                Debug.Log(MOVEPOS);
            }

            //GetComponent<Renderer>().material.color = colors[TYPE];
        }

        if (Input.GetKey(KeyCode.D) && MOVE_NOW == 0 && exchangecount == 0 && STAND == 1)      //移動中でなく右キーが押されたら
        {
            MOVE_NOW = 1;
            MOVE_D = 1;
            //STARTPOINT = transform.position.x;                      //誤差修正の為移動開始地点を記録
            //STAND = 0;
            //STAND_T = 0;
            //STAND_U = 0;
        }

        if (Input.GetKey(KeyCode.A) && MOVE_NOW == 0 && exchangecount == 0 && STAND == 1)      //移動中でなく左キーが押されたら
        {
            MOVE_NOW = 1;
            MOVE_D = -1;
            //STARTPOINT = transform.position.x;                      //誤差修正の為移動開始地点を記録
            //STAND = 0;
            //STAND_T = 0;
            //STAND_U = 0;
        }

        if (MOVE_NOW > 0)
        {
            MOVE_NOW--;

            Vector3 FORCE = new Vector3(20.0f * MOVE_D, 2.0f * MOVE_V, 0.0f);
            if (rb.velocity.magnitude < 4.0f)
            {
                rb.AddForce(FORCE);
            }
            //rb.velocity = new Vector3(15.0f * MOVE_D, 0.0f, 0.0f);

            //Vector3 pos = transform.position;
            //pos.x += 0.1f * MOVE_D;
            //transform.position = pos;
            if (MOVE_NOW == 0)                                         //移動完了なら
            {
                //pos.x = STARTPOINT +1 * MOVE_D;
                //transform.position = pos;                           //誤差を整数加算により補正
            }
        }

        if (exchangecount > 0)
        {
            exchangecount--;

            if (exchangecount == 0)
            {
                if (TYPE == 0)
                {
                    tag = "SOLID";
                    Debug.Log("降下");
                    Physics.gravity = new Vector3(0, -9.8f, 0);
                    MOVE_V = 1;
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                }

                if (TYPE == 2)
                {
                    tag = "CLOUD";
                    Debug.Log("浮上");
                    Physics.gravity = new Vector3(0, 9.8f, 0);
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    MOVE_V = -1;
                }
            }
        }

        if (TYPE == 0 && MOVE_NOW == 0)
        {
            if (STAND_U == 1)
            {
                STAND = 1;
                boxcollider.enabled = false;
                capcollider.enabled = true;
            }
        }

        if (TYPE == 1 && MOVE_NOW == 0)
        {
            if (STAND_U == 1 || STAND_T == 1)
            {
                STAND = 1;
                //boxcollider.enabled = true;
                //capcollider.enabled = false;
            }
        }

        if (TYPE == 2 && MOVE_NOW == 0)
        {
            if (STAND_T == 1)
            {
                STAND = 1;
                //boxcollider.enabled = true;
                //capcollider.enabled = false;
            }
        }

        if (transform.position.x > 0)
        {
            posx = (int)transform.position.x;
        }
        else
        {
            posx = (int)(transform.position.x - 1.0f);
        }

        if (transform.position.y > 0)
        {
            posy = (int)(transform.position.y + 0.5f);
        }
        else
        {
            posy = (int)(transform.position.y - 0.5f);
        }


        if (posx != B_posx)
        {
            if (MOVE_D == 1 && VMOVEflag == 1)  //縦移動予約が入っていて同じ方向に進んでいたら
            {
                if (transform.position.x > VMOVEPOS && AUTOMOVEflag == 0)
                {
                    //Debug.Log("おちる");
                    //rb.velocity = new Vector3(2.0f, 0.0f, 0.0f);
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    AUTOMOVEflag = 1;
                }
            }

            if (MOVE_D == -1 && VMOVEflag == 1)  //縦移動予約が入っていて同じ方向に進んでいたら
            {
                if (transform.position.x < VMOVEPOS && AUTOMOVEflag == 0)
                {
                    Debug.Log("おちる");
                    //rb.velocity = new Vector3(2.0f, 0.0f, 0.0f);
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    AUTOMOVEflag = 1;
                }
            }
            JUGEMOVE.SETposx(posx);
            VMOVEflag = JUGEMOVE.root(MOVE_D, MOVE_V);
            B_MOVE_D = MOVE_D;
        }

        if (MOVE_D != B_MOVE_D)
        {
            VMOVEflag = JUGEMOVE.root(MOVE_D, MOVE_V);
            B_MOVE_D = MOVE_D;
        }

        //落下補正用強制移動

        if (AUTOMOVEflag == 1)
        {
            if (MOVE_D == 1)
            {
                if (transform.position.x < VMOVEPOS + 0.5f)
                {
                    rb.velocity = new Vector3(2.0f, 0.0f, 0.0f);
                }

                if (transform.position.x > VMOVEPOS + 0.5f)
                {
                    Vector3 T_pos = transform.position;
                    T_pos.x = VMOVEPOS + 0.5f;
                    transform.position = T_pos;
                    AUTOMOVEflag = 0;
                    VMOVEflag = 0;
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }

            if (MOVE_D == -1)
            {
                if (transform.position.x > VMOVEPOS - 0.5f)
                {
                    rb.velocity = new Vector3(-2.0f, 0.0f, 0.0f);
                }

                if (transform.position.x < VMOVEPOS - 0.5f)
                {
                    Vector3 T_pos = transform.position;
                    T_pos.x = VMOVEPOS - 0.5f;
                    transform.position = T_pos;
                    AUTOMOVEflag = 0;
                    VMOVEflag = 0;
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
        }

        //ギミック利用よう強制移動

        if (AUTOMOVEflag2 == 1)
        {
            if (MOVE_D == 1)
            {
                if (transform.position.x < MOVEPOS)
                {
                    rb.velocity = new Vector3(1.0f, 0.0f, 0.0f);
                }

                if (transform.position.x > MOVEPOS)
                {
                    Vector3 T_pos = transform.position;
                    T_pos.x = MOVEPOS;
                    transform.position = T_pos;
                    AUTOMOVEflag2 = 0;
                    MOVEFinish = 1;
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }

            if (MOVE_D == -1)
            {
                if (transform.position.x > MOVEPOS)
                {
                    rb.velocity = new Vector3(-1.0f, 0.0f, 0.0f);
                }

                if (transform.position.x < MOVEPOS)
                {
                    Vector3 T_pos = transform.position;
                    T_pos.x = MOVEPOS;
                    transform.position = T_pos;
                    AUTOMOVEflag2 = 0;
                    MOVEFinish = 1;
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
        }

        if (MOVEFinish == 1)
        {
            MOVEFinish = 0;

            //各利用ギミックに対応する処理

            //GAPを使用する場合
            if (stay_GAP == 1)
            {
                capcollider.enabled = false;
                TARGETV = transform.position.y - 2.1f;
                GAPMOVE = 1;
                MOVE_V = -1;
                //TIME_T = 47;
            }

            if (stay_HARDHOT == 1)
            {
                stay_HARDHOT = 0;
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                exchangecount = 61;
                if (TYPE < 2)
                {
                    Debug.Log("加熱");
                    TYPE++;
                    if (TYPE == 1)
                    {
                        SOLID.exchange_s();
                        AQUA.exchange_b();
                        tag = "AQUA";
                    }
                    if (TYPE == 2)
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

            if (stay_HARDCOLD == 1)
            {
                stay_HARDCOLD = 0;
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
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
        }

        if (GAPMOVE == 1)
        {
            //TIME_T--;
            if (transform.position.y < TARGETV)
            {
                capcollider.enabled = true;

                Debug.Log("浮上");
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                Physics.gravity = new Vector3(0, 9.8f, 0);
            }
        }

        if (posy != B_posy)
        {
            VMOVEflag = 0;
            JUGEMOVE.SETposy(posy);
            VMOVEflag = JUGEMOVE.root(MOVE_D, MOVE_V);
        }

        B_posx = posx;
        B_posy = posy;
        //Vector3 pos_p = transform.position;
        //PLAYERCAMERA.SETPOS(pos_p);
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

    public void CLEAR_stay_HOT()
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

    public void SET_stay_COLD()
    {
        stay_COLD = 1;
    }

    public void CLEAR_stay_COLD()
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
        if (MOVE_NOW == 0)
        {
            STAND_U = 1;
        }
    }

    public void CLEAR_STAND()
    {
        STAND = 0;
        boxcollider.enabled = false;
        capcollider.enabled = true;
    }

    public Vector3 GETPLAYERPOS()
    {
        Vector3 pos = transform.position;
        return pos;
    }

    public void SET_V_MOVEPOS(float x)
    {
        if (VMOVEflag == 0)
        {
            VMOVEPOS = x - 0.5f * MOVE_D;
            //VMOVEPOS = x;
        }
        //Debug.Log(transform.position);
    }

    public void SET_stay_GAP()
    {
        stay_GAP = 1;
    }

    public void CLEAR_stay_GAP()
    {
        stay_GAP = 0;
    }

    public void SET_stay_HOT()
    {
        stay_HOT = 1;
    }

    public void HARDHOT()
    {
        //capcollider.enabled = false;
        Debug.Log(transform.position);
        MOVEPOS = JUGEMOVE.GET_JUGEPOS();   //目標地点Xを判定マスと同じにする

        if (transform.position.x > MOVEPOS)
        {
            MOVE_D = -1;
        }
        else
        {
            MOVE_D = 1;
        }

        AUTOMOVEflag2 = 1;
        stay_HARDHOT = 1;
    }

    public void HARDCOLD()
    {
        //capcollider.enabled = false;
        Debug.Log(transform.position);
        MOVEPOS = JUGEMOVE.GET_JUGEPOS();   //目標地点Xを判定マスと同じにする

        if (transform.position.x > MOVEPOS)
        {
            MOVE_D = -1;
        }
        else
        {
            MOVE_D = 1;
        }

        AUTOMOVEflag2 = 1;
        stay_HARDCOLD = 1;
    }
}