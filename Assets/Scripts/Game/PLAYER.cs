using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER : MonoBehaviour
{
    public int TYPE = 0;
    public int MOVE_D = 1;
    public int MOVE_NOW = 0;
    public int STAND = 0;
    public int stay_HOT_R = 0; //仮HOT接触フラグ右側
    public int stay_HOT_L = 0; //仮HOT接触フラグ左側
    public int stay_HOT = 0;   //HOT完全接触フラグ
    public int stay_COLD_R = 0; //仮COKD接触フラグ右側
    public int stay_COLD_L = 0; //仮COLD接触フラグ左側
    public int stay_COLD = 0;   //COLD完全接触フラグ

    Color[] colors = new Color[3] { new Color(0.1f, 0.2f, 0.3f, 1.0f),
                                    new Color(0.0f, 0.5f, 0.5f, 1.0f),
                                    new Color(1.0f, 1.0f, 1.0f, 1.0f),};

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = colors[TYPE];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && MOVE_NOW == 0)
        {
            Debug.Log("入力");
            if(stay_HOT==1)
            {
                if (TYPE < 2)
                {
                    Debug.Log("加熱");
                    TYPE++;

                    if(TYPE==2)
                    {
                        Debug.Log("浮上");
                        Physics.gravity = new Vector3(0, 9.8f, 0);
                    }
                }
            }

            if (stay_COLD == 1)
            {
                if (TYPE > 0)
                {
                    Debug.Log("冷却");
                    TYPE--;
                    if (TYPE == 0)
                    {
                        Debug.Log("降下");
                        Physics.gravity = new Vector3(0, -9.8f, 0);
                    }
                }
            }

            GetComponent<Renderer>().material.color = colors[TYPE];
        }

        if (Input.GetKey(KeyCode.RightArrow) && MOVE_NOW == 0)      //移動中でなく右キーが押されたら
        {
            MOVE_NOW = 25;
            MOVE_D = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && MOVE_NOW == 0)      //移動中でなく右キーが押されたら
        {
            MOVE_NOW = 25;
            MOVE_D = -1;
        }

        if (MOVE_NOW > 0)
        {
            MOVE_NOW--;

            Vector3 pos = transform.position;
            pos.x += 0.04f * MOVE_D;
            transform.position = pos;
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
}
