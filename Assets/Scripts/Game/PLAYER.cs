using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PLAYER : MonoBehaviour
{
    //public PLAYERCAMERA PLAYERCAMERA;
    PLAYERCAMERA PLAYERCAMERA;
    Game_Fade Game_Fade;
    GameObject CAMERA;
    GameObject FADE;
    public SOLID SOLID;
    public AQUA AQUA;
    public AQUA_MOTION AQUA_MOTION;
    public ICE_ATAMA_MOTION ICE_ATAMA_MOTION;
    public ICE_doutai_MOTION ICE_Doutai_MOTION;
    public ICE_kubiwa_MOTION ICE_Kubiwa_MOTION;
    public CLOUD_MOTION CLOUD_MOTION;
    public CLOUD CLOUD;
    public JUGEMOVE JUGEMOVE;
    public HIT_U HIT_U;
    public HIT_RT HIT_RT;
    public HIT_RU HIT_RU;
    public HIT_LT HIT_LT;
    public HIT_LU HIT_LU;
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
    public int VMOVEflag = 0;       //縦移動予約フラグ
    public int AUTOMOVEflag = 0;    //強制移動フラグ
    public int AUTOMOVEflag2 = 0;   //強制移動フラグ２
    public int AUTOMOVEflag3 = 0;   //強制移動フラグ３
    public float VMOVEPOS = 0;
    public float A_VMOVEPOS = 0;
    public float MOVEPOS = 0;       //強制移動目標地点
    public int MOVEFinish = 0;      //強制移動完了フラグ
    public int DROPFLAG = 0;        //水滴落下予約フラグ
    public int GAPMOVE_U = 0;       //GAP通過フラグ下
    public int GAPMOVE_T = 0;       //GAP通過フラグ上
    public float TARGETV = 0;       //
    public float DOWNPOS_L = 0;     //水滴落下準備位置
    public float DOWNPOS_R = 0;     //水滴落下準備位置
    public int FLOATflag = 0;       //水上移動フラグ
    public int MUTEKI = 0;          //無敵フラグ
    public int VIEWflag = -1;       //見渡しフラグ
    public int VIEWBACK = 0;        //見渡し復元カウント
    public int GAMEOVER = 0;        //ゲームオーバー判定フラグ
    public int GAMEOVER_ACT = 0;    //ゲームオーバーアクションカウント
    public int GOAL = 0;            //ゴールフラグ
    public int FADEFLAg = 0;        //フェードフラグ
    public int FADECONT = 0;        //フェードアウトカウント
    public int Leaf_HIT = 0;        //葉っぱ接触フラグ
    public int SPONGE_HIT = 0;      //スポンジ接触フラグ
    public int GAME_STOP = -1;       //ポーズフラグ

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
    public int stay_GAP_U = 0;  //GAP接触フラグ
    public int stay_GAP_T = 0;  //GAP接触フラグ
    public int stay_HARDHOT = 0;//HARDHOTフラグ
    public int stay_HARDCOLD = 0;//HARDCOLDフラグ
    public int stay_WALL_R = 0; //壁（移動不能マス）接触フラグ右
    public int stay_WALL_L = 0; //壁（移動不能マス）接触フラグ左
    public int stay_WATER = 0;  //水接触フラグ
    public int stay_THORN_BLOCK = 0;    //トゲブロック接触フラグ
    public int stay_FIRE = 0;     //炎接触フラグ
    public int stay_DRAIN = 0;    //ドレイン接触フラグ
    public int stay_LIFTZOON = 0;   //リフトゾーン侵入フラグ

    int exchangecount = 0;  //状態変化演出カウント

    Color[] colors = new Color[3] { new Color(0.1f, 0.2f, 0.3f, 1.0f),
                                    new Color(0.0f, 0.5f, 0.5f, 1.0f),
                                    new Color(1.0f, 1.0f, 1.0f, 1.0f),};

    // Start is called before the first frame update
    void Start()
    {
        CAMERA = GameObject.Find("Main Camera");
        PLAYERCAMERA = CAMERA.GetComponent<PLAYERCAMERA>();

        FADE = GameObject.Find("Fade");
        Game_Fade = FADE.GetComponent<Game_Fade>();

        rb = this.GetComponent<Rigidbody>();
        PM = this.gameObject.GetComponent<PhysicMaterial>();
        boxcollider = this.gameObject.GetComponent<BoxCollider>();
        capcollider = this.gameObject.GetComponent<CapsuleCollider>();
        Physics.gravity = new Vector3(0, -9.8f, 0);
        MUTEKI = 6;
    }

    // Update is called once per frame
    void Update()
    {
        //int ARROW = 0;

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
        {
            GAME_STOP *= -1;
            if (GAME_STOP == 1)
            {
                Time.timeScale = 0f;
            }
            if (GAME_STOP == -1)
            {
                Time.timeScale = 1f;
            }
        }

        /*ポーズ画面がコントローラーに対応したら開く
        if(GAME_STOP == 1 && Input.GetKeyDown("joystick button 0"))
        {
            GAME_STOP = -1;
            Time.timeScale = 1f;
        }
        */

        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        float x_axis = Input.GetAxis("Horizontal2");//右マイナス　左プラス
        float x_axis_R = Input.GetAxis("Rstick_H"); //右マイナス　左プラス
        float y_axis_R = Input.GetAxis("Rstick_V"); //上プラス　下マイナス

        int ARROW = 0;

        if (x_axis < 0)
        {
            ARROW = 1;
        }

        if (x_axis > 0)
        {
            ARROW = 2;
        }

        if ((x_axis_R != 0 || y_axis_R != 0) && VIEWflag == -1)
        {
            VIEWflag = 1;
        }

        x_axis = Mathf.Abs(x_axis);


        if (VIEWBACK > 0)
        {
            VIEWBACK--;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0")) && MOVE_NOW == 0 && exchangecount == 0 && VIEWflag == -1)
        {
            Debug.Log("入力");
            if (stay_HOT == 1)
            {
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
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
            }

            if (stay_COLD == 1)
            {
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
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
            }
        }

        if (Input.GetKey(KeyCode.Return))
        {
            if (GOAL == 1 && FADEFLAg == 0)
            {
                Game_Fade.Fade_Start(90, true, "WorldScene");
                FADEFLAg = 1;
            }
        }

        if ((Input.GetKey(KeyCode.D) || ARROW == 1) && MOVE_NOW == 0 && exchangecount == 0 && STAND == 1 && AUTOMOVEflag == 0 && AUTOMOVEflag2 == 0 && AUTOMOVEflag3 == 0 && GAPMOVE_T == 0 && GAPMOVE_U == 0 && VIEWflag == -1 && VIEWBACK == 0 && GOAL == 0 && GAMEOVER == 0 && FLOATflag == 0 && SPONGE_HIT == 0)      //移動中でなく右キーが押されたら
        {
            MOVE_NOW = 1;
            MOVE_D = 1;
        }

        if ((Input.GetKey(KeyCode.A) || ARROW == 2) && MOVE_NOW == 0 && exchangecount == 0 && STAND == 1 && AUTOMOVEflag == 0 && AUTOMOVEflag2 == 0 && AUTOMOVEflag3 == 0 && GAPMOVE_T == 0 && GAPMOVE_U == 0 && VIEWflag == -1 && VIEWBACK == 0 && GOAL == 0 && GAMEOVER == 0 && FLOATflag == 0 && SPONGE_HIT == 0)      //移動中でなく左キーが押されたら
        {
            MOVE_NOW = 1;
            MOVE_D = -1;
        }

        //見渡し関係

        if (Input.GetKeyDown(KeyCode.F))
        {
            VIEWflag *= -1;
            if (VIEWflag == -1)
            {
                VIEWBACK = 30;
            }
        }

        if (Input.GetKeyDown("joystick button 0") && VIEWflag == 1)
        {
            VIEWflag = -1;
            VIEWBACK = 30;

        }

        if (MOVE_NOW > 0)
        {
            MOVE_NOW--;
            Vector3 FORCE;

            if (ARROW == 0)
            {
                FORCE = new Vector3(20.0f * MOVE_D, 1.0f * MOVE_V, 0.0f);
            }
            else
            {
                FORCE = new Vector3(20.0f * MOVE_D * x_axis, 1.0f * MOVE_V, 0.0f);
            }

            if (rb.velocity.magnitude < 3.0f)
            {
                rb.AddForce(FORCE);
            }
        }

        if (AUTOMOVEflag == 0 && AUTOMOVEflag2 == 0 && AUTOMOVEflag3 == 0)
        {
            if (rb.velocity.x > 0)
            {
                MOVE_D = 1;
            }

            if (rb.velocity.x < 0)
            {
                MOVE_D = -1;
            }
        }

        //水滴落下検出

        if (stay_WALL_R == 1 && TYPE == 1 && MOVE_V == -1)
        {
            if (transform.position.x > DOWNPOS_R)
            {
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                MOVEPOS = JUGEMOVE.GET_JUGEPOS();   //目標地点Xを判定マスと同じにする

                if (transform.position.x > MOVEPOS)
                {
                    MOVE_D = -1;
                }
                else
                {
                    MOVE_D = 1;
                }

                stay_HOT = 0;
                stay_COLD = 0;

                AUTOMOVEflag2 = 1;
                DROPFLAG = 1;
            }
        }

        if (stay_WALL_L == 1 && TYPE == 1 && MOVE_V == -1)
        {
            if (transform.position.x < DOWNPOS_L)
            {
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                MOVEPOS = JUGEMOVE.GET_JUGEPOS();   //目標地点Xを判定マスと同じにする

                if (transform.position.x > MOVEPOS)
                {
                    MOVE_D = -1;
                }
                else
                {
                    MOVE_D = 1;
                }

                stay_HOT = 0;
                stay_COLD = 0;

                AUTOMOVEflag2 = 1;
                DROPFLAG = 1;
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

                if (TYPE == 1)
                {
                    tag = "AQUA";
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

        //移動入力受付管理

        if (TYPE == 0 && MOVE_NOW == 0)
        {
            if (STAND_U == 1)
            {
                STAND = 1;
                Leaf_HIT = 0;
                boxcollider.enabled = false;
                capcollider.enabled = true;
            }
        }

        if (TYPE == 1 && MOVE_NOW == 0)
        {
            if (STAND_U == 1 && MOVE_V == 1)
            {
                STAND = 1;
                Leaf_HIT = 0;
            }

            if (STAND_T == 1 && MOVE_V == -1)
            {
                STAND = 1;
                Leaf_HIT = 0;
            }
        }

        if (TYPE == 2 && MOVE_NOW == 0)
        {
            if (STAND_T == 1)
            {
                STAND = 1;
                Leaf_HIT = 0;
            }
        }

        //判定マスの制御
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

        if (transform.position.y < 0)
        {
            Vector3 LIMIT = transform.position;
            LIMIT.y = 0;
            transform.position = LIMIT;
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            Physics.gravity = new Vector3(0.0f, 0.0f, 0.0f);

        }

        //縦移動フラグが準備されている場合
        if (VMOVEflag == 1)
        {
            //移動予定マスに物体が合ったら予約を削除

            if (MOVE_D == 1 && MOVE_V == 1)
            {
                if (HIT_RU.GETAIRFLAG() == 1)
                {
                    VMOVEflag = 0;
                }
            }

            if (MOVE_D == 1 && MOVE_V == -1)
            {
                if (HIT_RT.GETAIRFLAG() == 1)
                {
                    VMOVEflag = 0;
                }
            }

            if (MOVE_D == -1 && MOVE_V == 1)
            {
                if (HIT_LU.GETAIRFLAG() == 1)
                {
                    VMOVEflag = 0;
                }
            }

            if (MOVE_D == -1 && MOVE_V == -1)
            {
                if (HIT_LT.GETAIRFLAG() == 1)
                {
                    VMOVEflag = 0;
                }
            }
        }


        if (posx != B_posx)
        {
            if (MOVE_D == 1 && VMOVEflag == 1 && Leaf_HIT == 0 && (TYPE == 2 || TYPE == 0 || (TYPE == 1 && MOVE_V == 1)))  //縦移動予約が入っていて同じ方向に進んでいたら（雲）
            {
                if (transform.position.x > VMOVEPOS && AUTOMOVEflag == 0)
                {
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    AUTOMOVEflag = 1;
                }
            }

            if (MOVE_D == 1 && VMOVEflag == 1 && TYPE == 1 && MOVE_V == -1 && stay_LIFTZOON == 0)  //縦移動予約が入っていて同じ方向に進んでいたら（水）
            {
                if (transform.position.x > VMOVEPOS && AUTOMOVEflag3 == 0)
                {
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    MOVE_D = -1;
                    AUTOMOVEflag3 = 1;
                    A_VMOVEPOS = VMOVEPOS;
                }
            }

            if (MOVE_D == -1 && VMOVEflag == 1 && Leaf_HIT == 0 && (TYPE == 2 || TYPE == 0 || (TYPE == 1 && MOVE_V == 1)))  //縦移動予約が入っていて同じ方向に進んでいたら（雲）
            {
                if (transform.position.x < VMOVEPOS && AUTOMOVEflag == 0)
                {
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    AUTOMOVEflag = 1;
                }
            }

            if (MOVE_D == -1 && VMOVEflag == 1 && TYPE == 1 && MOVE_V == -1 && stay_LIFTZOON == 0)  //縦移動予約が入っていて同じ方向に進んでいたら（水）
            {
                if (transform.position.x < VMOVEPOS && AUTOMOVEflag3 == 0)
                {
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    MOVE_D = 1;
                    AUTOMOVEflag3 = 1;
                    A_VMOVEPOS = VMOVEPOS;
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

        //水滴落下補正用強制移動

        if (AUTOMOVEflag3 == 1)
        {
            if (MOVE_D == 1)
            {
                if (transform.position.x < A_VMOVEPOS + 0.5f)
                {
                    rb.velocity = new Vector3(2.0f, 0.0f, 0.0f);
                }

                if (transform.position.x >= A_VMOVEPOS + 0.5f)
                {
                    Vector3 T_pos = transform.position;
                    T_pos.x = A_VMOVEPOS + 0.5f;
                    transform.position = T_pos;
                    AUTOMOVEflag3 = 0;
                    VMOVEflag = 0;
                    MOVE_V = 1;
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    Physics.gravity = new Vector3(0, -9.8f, 0);
                }
            }

            if (MOVE_D == -1)
            {
                if (transform.position.x > A_VMOVEPOS - 0.5f)
                {
                    rb.velocity = new Vector3(-2.0f, 0.0f, 0.0f);
                }

                if (transform.position.x <= A_VMOVEPOS - 0.5f)
                {
                    Vector3 T_pos = transform.position;
                    T_pos.x = A_VMOVEPOS - 0.5f;
                    transform.position = T_pos;
                    AUTOMOVEflag3 = 0;
                    VMOVEflag = 0;
                    MOVE_V = 1;
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;
                    rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    Physics.gravity = new Vector3(0, -9.8f, 0);
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

                if (transform.position.x >= MOVEPOS)
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

                if (transform.position.x <= MOVEPOS)
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

            //加熱ブロックを使用
            if (stay_HOT == 1)
            {
                exchangecount = 61;
                if (TYPE < 2)
                {
                    Debug.Log("加熱");
                    TYPE++;
                    if (TYPE == 1)
                    {
                        SOLID.exchange_s();
                        AQUA.exchange_b();
                    }
                    if (TYPE == 2)
                    {
                        AQUA.exchange_s();
                        CLOUD.exchange_b();
                    }
                }

                stay_WALL_L = 0;
                stay_WALL_R = 0;
            }

            //冷却ブロックを使用
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
                    }
                    if (TYPE == 1)
                    {
                        CLOUD.exchange_s();
                        AQUA.exchange_b();
                    }
                }

                stay_WALL_L = 0;
                stay_WALL_R = 0;
            }

            //GAPを使用する場合（水）
            if (stay_GAP_U == 1)
            {
                capcollider.enabled = false;
                //TARGETV = transform.position.y - 2.1f;
                TARGETV = JUGEMOVE.GET_JUGEPOS_y() - 2.1f;
                GAPMOVE_U = 1;
                MOVE_V = -1;
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
            }

            if (stay_GAP_T == 1)
            {
                capcollider.enabled = false;
                //TARGETV = transform.position.y + 2.1f;
                TARGETV = JUGEMOVE.GET_JUGEPOS_y() + 2.1f;
                GAPMOVE_T = 1;
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
            }

            //強制加熱

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
                    }
                    if (TYPE == 2)
                    {
                        AQUA.exchange_s();
                        CLOUD.exchange_b();
                    }
                }
                else if (TYPE == 2)
                {
                    GAMEOVER = 1;
                    Game_Fade.Fade_Start(90, true, "GameScene");
                }
            }

            //強制冷却

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
                    }
                    if (TYPE == 1)
                    {
                        CLOUD.exchange_s();
                        AQUA.exchange_b();
                    }
                }
                else if (TYPE == 0)
                {
                    GAMEOVER = 1;
                    Game_Fade.Fade_Start(90, true, "GameScene");
                }
            }

            //水滴壁落下
            if (DROPFLAG == 1)
            {
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                MOVE_V = 1;
                DROPFLAG = 0;
            }
            /*
            if (stay_WALL_R == 1 && TYPE == 1)
            {
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                MOVE_V = 1;
            }

            if (stay_WALL_L == 1 && TYPE == 1)
            {
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
                Physics.gravity = new Vector3(0, -9.8f, 0);
                MOVE_V = 1;
            }
            */

            //水ギミック利用
            if (stay_WATER == 1)
            {
                if (TYPE == 0)
                {
                    STAND = 0;
                    STAND_T = 0;
                    STAND_U = 0;

                    Physics.gravity = new Vector3(0, 5.0f, 0);
                    MOVE_V = -1;
                    FLOATflag = 1;
                }
                /*
                if(TYPE == 1)
                {
                    GAMEOVER = 1;
                    GAMEOVER_ACT = 90;
                    AQUA.exchange_s();
                }   
                */
            }

            if (stay_THORN_BLOCK == 1)
            {
                GAMEOVER = 1;
                GAMEOVER_ACT = 90;
                if (TYPE == 1)
                {
                    AQUA_MOTION.MOTION_RYU();
                }

                if (TYPE == 2)
                {
                    CLOUD_MOTION.MOTION_RYU();
                }
            }

            if (stay_FIRE == 1)
            {
                stay_FIRE = 0;
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                exchangecount = 61;

                Debug.Log("加熱");
                TYPE++;
                SOLID.exchange_s();
                AQUA.exchange_b();
            }

            if (stay_DRAIN == 1)
            {
                GAMEOVER = 1;
                GAMEOVER_ACT = 90;
                AQUA_MOTION.MOTION_RYU();
            }
        }

        if (GAPMOVE_U == 1)
        {
            if (transform.position.y < TARGETV)
            {
                GAPMOVE_U = 0;
                capcollider.enabled = true;
                Debug.Log("浮上");
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                Physics.gravity = new Vector3(0, 9.8f, 0);
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
            }
        }

        if (GAPMOVE_T == 1)
        {
            if (transform.position.y > TARGETV)
            {
                GAPMOVE_T = 0;
                capcollider.enabled = true;
                rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
            }
        }

        if (FLOATflag == 1)
        {
            if (transform.position.y > TARGETV)
            {
                FLOATflag = 0;
                //capcollider.enabled = true;
                //rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                HIT_U.SETWATERTrigger();
                Physics.gravity = new Vector3(0, -9.8f, 0);
                MOVE_V = 1;
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
            }
        }

        if (MUTEKI > 0)
        {
            MUTEKI--;
        }

        if (SPONGE_HIT > 0)
        {
            SPONGE_HIT--;
        }

        if (posy != B_posy)
        {
            VMOVEflag = 0;
            JUGEMOVE.SETposy(posy);
            VMOVEflag = JUGEMOVE.root(MOVE_D, MOVE_V);
        }

        if (GAMEOVER_ACT > 0)
        {
            GAMEOVER_ACT--;
            if (GAMEOVER_ACT == 0)
            {
                Game_Fade.Fade_Start(90, true, "GameScene", "PauseScene");
            }
        }

        /*
        if (GAMEOVER == 1)
        {
            FADECONT++;

            if (FADECONT == 91)
            {
                SceneManager.LoadScene("GameScene"); //移動先のシーン名　（リスタート）
            }
        }
        */

        B_posx = posx;
        B_posy = posy;
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
        //boxcollider.enabled = false;
        //capcollider.enabled = true;
    }

    public Vector3 GETPLAYERPOS()
    {
        Vector3 pos = transform.position;
        return pos;
    }

    public void SET_V_MOVEPOS(float x)
    {
        //if (VMOVEflag == 0)
        if (AUTOMOVEflag == 0)
        {
            VMOVEPOS = x - 0.5f * MOVE_D;
        }
    }

    public void SET_stay_GAP_U()
    {
        if (TYPE == 1)
        {
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
            stay_GAP_U = 1;
        }
    }

    public void SET_stay_GAP_T()
    {
        if (TYPE == 2)
        {
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
            stay_GAP_T = 1;
        }
    }

    public void CLEAR_stay_GAP_U()
    {
        stay_GAP_U = 0;
    }

    public void CLEAR_stay_GAP_T()
    {
        stay_GAP_T = 0;
    }

    public void SET_stay_HOT()
    {
        stay_HOT = 1;
    }

    public void SET_stay_WALL_R()
    {
        stay_WALL_R = 1;
        DOWNPOS_R = JUGEMOVE.GET_JUGEPOS() + 0.1f;
    }

    public void CLEAR_stay_WALL_R()
    {
        stay_WALL_R = 0;
    }

    public void SET_stay_WALL_L()
    {
        stay_WALL_L = 1;
        DOWNPOS_L = JUGEMOVE.GET_JUGEPOS() - 0.1f;
    }

    public void CLEAR_stay_WALL_L()
    {
        stay_WALL_L = 0;
    }

    public void CLEAR_stay_WATER()
    {
        stay_WATER = 0;
    }

    public void HARDHOT()
    {
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

    public void WATER(float TARGET_V)
    {
        if (TYPE == 0 || TYPE == 1)
        {
            MOVEPOS = JUGEMOVE.GET_JUGEPOS();   //目標地点Xを判定マスと同じにする

            if (transform.position.x > MOVEPOS)
            {
                MOVE_D = -1;
            }
            else
            {
                MOVE_D = 1;
            }

            TARGETV = TARGET_V + 1.1f;

            AUTOMOVEflag2 = 1;
            stay_WATER = 1;

            if (TYPE == 1)
            {
                GAMEOVER = 1;
                GAMEOVER_ACT = 90;
                AQUA.exchange_s();
            }
        }
    }

    public void FIRE()
    {
        if (TYPE == 0)
        {
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
            stay_FIRE = 1;
        }
    }

    public void DRAIN()
    {
        if (TYPE == 1)
        {
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
            stay_DRAIN = 1;
        }
    }

    public void THORN(bool pop, float block_y)
    {
        //トゲが出ていないで液体か気体なら
        if (pop == false && (TYPE == 1 || TYPE == 2))
        {
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
            stay_THORN_BLOCK = 1;
        }
    }

    public void SPONGE()
    {
        if (TYPE == 1)
        {
            GAMEOVER = 1;
            Game_Fade.Fade_Start(90, true, "GameScene");
        }

        if (TYPE == 0)
        {
            SPONGE_HIT = 50;
        }
    }

    public void WIND()
    {
        if (TYPE == 2)
        {
            SceneManager.LoadScene("CLEAR"); //移動先のシーン名
        }
    }

    public int GETFLOATflag()
    {
        return FLOATflag;
    }

    public int GETGAMEOVER()
    {
        return GAMEOVER;
    }


    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("LEAF"))
        {
            Vector3 FORCE = new Vector3(-3.0f, 0.0f, 0.0f);
            rb.AddForce(FORCE);
        }

        if (other.gameObject.CompareTag("LEAF_INV"))
        {
            Vector3 FORCE = new Vector3(3.0f, 0.0f, 0.0f);
            rb.AddForce(FORCE);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (MUTEKI == 0)
        {
            if (other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("LEAF"))
            {
                STAND = 0;
                STAND_T = 0;
                STAND_U = 0;
            }
        }

        if (other.gameObject.CompareTag("LEAF"))
        {
            Leaf_HIT = 1;
        }

        if (other.gameObject.CompareTag("LEAF_INV"))
        {
            Leaf_HIT = 1;
        }

        if (other.gameObject.CompareTag("GOAL") && GOAL == 0)
        {
            GOAL = 1;
            PLAYERCAMERA.F_LOCK();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (MUTEKI == 0)
        {
            if (other.gameObject.CompareTag("WINDMILL") && GAMEOVER == 0)
            {
                if (TYPE == 2)
                {
                    GAMEOVER = 1;
                    GAMEOVER_ACT = 90;
                    CLOUD_MOTION.CRUSH();
                    rb.velocity = new Vector3(0.0f, -0.0f, 0.0f);
                    Physics.gravity = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }

            if (other.gameObject.CompareTag("HAMMER") && GAMEOVER == 0)
            {
                if (TYPE == 0)
                {
                    GAMEOVER = 1;
                    GAMEOVER_ACT = 90;
                    tag = "Untagged";
                    ICE_ATAMA_MOTION.CRUSH();
                    ICE_Doutai_MOTION.CRUSH();
                    ICE_Kubiwa_MOTION.CRUSH();
                }
            }
            /*
            if (other.gameObject.CompareTag("WATER") && GAMEOVER == 0)
            {
                if (TYPE == 1)
                {
                    GAMEOVER = 1;
                    Game_Fade.Fade_Start(90, true, "GameScene");
                }
            }
            */

            if (other.gameObject.CompareTag("GEAR") && GAMEOVER == 0)
            {
                if (TYPE == 0)
                {
                    GAMEOVER = 1;
                    GAMEOVER_ACT = 90;
                    tag = "Untagged";
                    ICE_ATAMA_MOTION.CRUSH();
                    ICE_Doutai_MOTION.CRUSH();
                    ICE_Kubiwa_MOTION.CRUSH();
                }
            }

            if (other.gameObject.CompareTag("THORN") && GAMEOVER == 0)
            {
                if (TYPE == 0)
                {
                    GAMEOVER = 1;
                    GAMEOVER_ACT = 90;
                    tag = "Untagged";
                    //rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
                    ICE_ATAMA_MOTION.CRUSH();
                    ICE_Doutai_MOTION.CRUSH();
                    ICE_Kubiwa_MOTION.CRUSH();
                }
            }

            if (other.gameObject.CompareTag("WATERMILL"))
            {
                if (TYPE == 1)
                {
                    PLAYERCAMERA.SETMILLFIND();
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("LIFTZOON"))
        {
            stay_LIFTZOON = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WATERMILL"))
        {
            PLAYERCAMERA.CLAREMILLFIND();
        }

        if (other.gameObject.CompareTag("LIFTZOON"))
        {
            stay_LIFTZOON = 0;
        }
    }

    public int GET_MOVE_D()
    {
        return MOVE_D;
    }
}