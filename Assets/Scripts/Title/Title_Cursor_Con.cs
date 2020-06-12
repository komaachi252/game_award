using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Cursor_Con : MonoBehaviour
{
    //  現在の選択インデックス
    int m_current_cursor_idx = 0;
    float m_init_pos_y;
    float m_init_pos_x;
    public GameObject m_fade;
    bool m_is_play = false;
    public GameObject[] m_select_bars;
    public GameObject m_option_menu;
    bool m_is_wait; // 使用可能か？

    [SerializeField] SaveData stardata;//星の取得状況

    int lock_R = 0;
    int lock_L = 0;
    int lock_U = 0;
    int lock_D = 0;

    public bool Is_Wait
    {
        get { return m_is_wait; }
        set { m_is_wait = value; }
    }
    void Start()
    {
        m_is_play = true;
        FindObjectOfType<Audio_Manager>().Stop("select_world");
        FindObjectOfType<Audio_Manager>().Play("rain1");
        FindObjectOfType< Audio_Manager>().Play("Title");
        //  NewGameから、セーブデータがあれば1からでもよい
        m_current_cursor_idx = 0;
        if (stardata.Star_SaveRoad() == true)//セーブデータあったらコンテから
        {
            m_current_cursor_idx = 1;
        }
        //  初期Ｙ座標を保持しておく
        m_init_pos_y = this.transform.position.y;
        m_init_pos_x = this.transform.position.x;
        m_fade.GetComponent<Game_Fade>().Fade_Start(50);
        m_is_wait = false;
        foreach(var bar in m_select_bars)
        {
            bar.GetComponent<Title_Select_Bar>().Set_Index(m_current_cursor_idx);
        }

        //開幕移動しておく
        this.transform.position = new Vector3(m_init_pos_x + 97.0f * (m_current_cursor_idx % 2), m_init_pos_y - 20.0f * (m_current_cursor_idx / 2), this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Game_Fade>().Is_Fade) return;
        if (m_is_wait) return;
        Cursor_Input();
        lock_update();
    }

    private void FixedUpdate()
    {
        //  常に回転する
        this.transform.Rotate(0,0,-1);
    }


    void Cursor_Input()
    {
        float x_axis = Input.GetAxis("Horizontal2");//右マイナス　左プラス
        float y_axis = Input.GetAxis("Vertical");//上プラス　下マイナス

        var idx = m_current_cursor_idx;

        int ARROW_L = 0;
        int ARROW_R = 0;
        int ARROW_U = 0;
        int ARROW_D = 0;

        if (x_axis > 0.5f && lock_L == 0)
        {
            ARROW_L = 1;
            lock_L = 50;
        }
        else if(x_axis < 0.5f)
        {
            lock_L = 0;
        }

        if (x_axis < -0.5f && lock_R == 0)
        {
            ARROW_R = 1;
            lock_R = 50;
        }
        else if (x_axis > -0.5f)
        {
            lock_R = 0;
        }

        if (y_axis > 0.5f && lock_U == 0)
        {
            ARROW_U = 1;
            lock_U = 50;
        }
        else if (y_axis < 0.5f)
        {
            lock_U = 0;
        }

        if (y_axis < -0.5f && lock_D == 0)
        {
            ARROW_D = 1;
            lock_D = 50;
        }
        else if (y_axis > -0.5f)
        {
            lock_D = 0;
        }

        if (Input.GetKeyDown(KeyCode.W) || ARROW_U == 1)
        {
            if(idx - 2 >= 0)
            {
                FindObjectOfType<Audio_Manager>().Play("select");
                idx -= 2;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.S) || ARROW_D == 1)
        {
            if (idx + 2 < 4)
            {
                FindObjectOfType<Audio_Manager>().Play("select");
                idx += 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || ARROW_L == 1)
        {
            if (idx - 1 >= 0)
            {
                idx--;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || ARROW_R == 1)
        {
            if (idx + 1 < 4)
            {
                idx++;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
        }

        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0")) && idx == 0)  //  NEWGAME
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_fade.GetComponent<Game_Fade>().Fade_Start(20, true, "WorldScene");
            stardata.DataReset();//星の情報セーブデータ削除
            m_is_wait = true;
        }
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0")) && idx == 1 && stardata.Star_SaveRoad() == true)  //  CONTINUE
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_fade.GetComponent<Game_Fade>().Fade_Start(20, true, "WorldScene");
            m_is_wait = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0")) && idx == 1 && stardata.Star_SaveRoad() == false)
        {
            FindObjectOfType<Audio_Manager>().Play("botton_notpress");
        }
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0")) && idx == 2)  //  OPTION
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_is_wait = true;
            m_option_menu.gameObject.SetActive(true);
            m_option_menu.GetComponent<Option_Menu>().Open_Menu();
        }
        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("joystick button 0")) && idx == 3)  //  EXIT
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            UnityEngine.Application.Quit();
        }
        Cursor_Move(idx);
    }

    void Cursor_Move(int new_cursor_idx)
    {
        //  同じ場合は戻す
        if (m_current_cursor_idx == new_cursor_idx) return;
        foreach (var bar in m_select_bars)
        {
            bar.GetComponent<Title_Select_Bar>().Set_Index(new_cursor_idx);
        }
        //  インデックスに応じてＹ座標を移動
        m_current_cursor_idx = new_cursor_idx;

        this.transform.position = new Vector3(m_init_pos_x + 97.0f * (new_cursor_idx % 2),  m_init_pos_y - 20.0f * (new_cursor_idx / 2), this.transform.position.z);
    }

    void lock_update()
    {
        if(lock_D > 0)
        {
            lock_D--;
        }

        if (lock_U > 0)
        {
            lock_U--;
        }

        if (lock_L > 0)
        {
            lock_L--;
        }

        if (lock_R > 0)
        {
            lock_R--;
        }
    }

}
