using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManeger : MonoBehaviour
{
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

    [SerializeField] Game_Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        Pause_flag = false;//とりあえず非表示
        Pause_canvas.SetActive(false);

        select_flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //===========================================================
        //ポーズするかしないか
        //===========================================================
        if (Pause_flag == false)//ポーズしてない
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
            {
                Pause_flag = true;
            }

            Pause_canvas.SetActive(false);
            
        }
        else if (Pause_flag == true)//ポーズしてる
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown("joystick button 7"))
            {
                Pause_flag = false;
            }

            Pause_canvas.SetActive(true);

            //===========================================================
            //ポーズ中
            //===========================================================
            if (Input.GetKeyDown(KeyCode.W))
            {
                select_flag--;
                if (select_flag < 0)
                {
                    select_flag = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                select_flag++;
                if (select_flag >= text.Length)
                {
                    select_flag = text.Length - 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (select_flag == 0)//戻る
                {
                    Pause_flag = false;
                }
                else if (select_flag == 1)//リスタート
                {
                    fade.Fade_Start(20, true, "GameScene", "PauseScene");
                }
                else if (select_flag == 2)//ワールド選択に戻る
                {
                    fade.Fade_Start(20, true, "WorldScene");
                }
            }

            select.position = new Vector3(select.position.x, text[select_flag].position.y, 0.0f);


            
            

        }

        
    }
}
