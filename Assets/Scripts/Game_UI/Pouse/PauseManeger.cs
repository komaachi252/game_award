using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManeger : MonoBehaviour
{

    [SerializeField] ExposeClearFlag clearflag;

    //フラグ
    //true = ポーズ中
    //false = ポーズじゃない
    public bool Pause_flag { get; set; }

    [SerializeField] GameObject Pause_canvas;


    [SerializeField] Transform[] text;
    [SerializeField] Transform select;

    //フラグ
    //0 = Back
    //1 = Restart
    //2 = WorldSelect
    int select_flag;

    //フラグ
    //0 = 入力できない
    //1 = 入力できる
    //2 = 移動中
    int Key_flag;

    [SerializeField] Game_Fade fade;

    //パッドのフラグ
    //入力フラグ
    //0 = 未入力
    //1 = 上
    //2 = 下
    //3 = 入力中
    int pad_flag;

    float move_flame;//移動フレーム時間
    float move_speed;//移動速度
    

    // Start is called before the first frame update
    void Start()
    {
        Pause_flag = false;//とりあえず非表示
        Pause_canvas.SetActive(false);

        select_flag = 0;
        Key_flag = 1;

        move_flame = 1.0f;
        select.localPosition = new Vector3(select.localPosition.x, text[select_flag].localPosition.y, 0.0f);
        
    }

    
    

    // Update is called once per frame
    void Update()
    {
        if (clearflag.Expose_CrearFlag.Is_Clear_Flag == true)
        {
            return;
        }

        //===========================================================
        //ポーズするかしないか
        //===========================================================
        if (Pause_flag == false)//ポーズしてない
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
            {
                FindObjectOfType<Audio_Manager>().Play("enter");
                Pause_flag = true;
                Key_flag = 1;
            }

            Pause_canvas.SetActive(false);
            
        }
        else if (Pause_flag == true)//ポーズしてる
        {

            if (Key_flag == 0)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick button 1"))
            {
                FindObjectOfType<Audio_Manager>().Play("cancel");
                Time.timeScale = 1.0f;
                Pause_flag = false;
            }

            Pause_canvas.SetActive(true);


            //===========================================================
            //パッド処理
            //===========================================================
            float y_axis = Input.GetAxis("Vertical");//スティック
            float arrow_axis = Input.GetAxis("Vertical_Arrow");//パッド

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



            //===========================================================
            //ポーズ中
            //===========================================================
            if (Input.GetKeyDown(KeyCode.W) || pad_flag == 1)//上移動
            {
                select_flag--;
                if (select_flag < 0)
                {
                    select_flag = 0;
                }

                FindObjectOfType<Audio_Manager>().Play("select");
            }
            else if (Input.GetKeyDown(KeyCode.S) || pad_flag == 2)
            {
                select_flag++;
                if (select_flag >= text.Length)
                {
                    select_flag = text.Length - 1;
                }
                FindObjectOfType<Audio_Manager>().Play("select");
            }


            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0"))
            {
                Time.timeScale = 1.0f;
                
                if (select_flag == 0)//戻る
                {
                    FindObjectOfType<Audio_Manager>().Play("cancel");
                    Pause_flag = false;
                }
                else if (select_flag == 1)//リスタート
                {
                    FindObjectOfType<Audio_Manager>().Play("enter");

                    fade.Fade_Start(20, true, "GameScene", "PauseScene");


                }
                else if (select_flag == 2)//ワールド選択に戻る
                {
                    FindObjectOfType<Audio_Manager>().Play("enter");

                    fade.Fade_Start(20, true, "WorldScene");

                }
                Key_flag = 0;
            }

            select.localPosition = new Vector3(select.localPosition.x, text[select_flag].localPosition.y, 0.0f);
        }
    }

    void FixedUpdate()
    {

        

    }
}
