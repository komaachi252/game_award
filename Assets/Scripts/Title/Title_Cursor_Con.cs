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
        //  初期Ｙ座標を保持しておく
        m_init_pos_y = this.transform.position.y;
        m_init_pos_x = this.transform.position.x;
        m_fade.GetComponent<Game_Fade>().Fade_Start();
        m_is_wait = false;
        foreach(var bar in m_select_bars)
        {
            bar.GetComponent<Title_Select_Bar>().Set_Index(m_current_cursor_idx);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Game_Fade>().Is_Fade) return;
        if (m_is_wait) return;
        Cursor_Input();
    }

    private void FixedUpdate()
    {
        //  常に回転する
        this.transform.Rotate(0,0,-1);
    }


    void Cursor_Input()
    {
        var idx = m_current_cursor_idx;
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(idx - 2 >= 0)
            {
                FindObjectOfType<Audio_Manager>().Play("select");
                idx -= 2;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (idx + 2 < 4)
            {
                FindObjectOfType<Audio_Manager>().Play("select");
                idx += 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (idx - 1 >= 0)
            {
                idx--;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (idx + 1 < 4)
            {
                idx++;
                FindObjectOfType<Audio_Manager>().Play("select");
            }
        }

        if(Input.GetKeyDown(KeyCode.Return) && idx == 0)  //  NEWGAME
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_fade.GetComponent<Game_Fade>().Fade_Start(20, true, "WorldScene");
            m_is_wait = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && idx == 1)  //  CONTINUE
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_fade.GetComponent<Game_Fade>().Fade_Start(20, true, "WorldScene");
            m_is_wait = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && idx == 2)  //  OPTION
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_is_wait = true;
            m_option_menu.gameObject.SetActive(true);
            m_option_menu.GetComponent<Option_Menu>().Open_Menu();
        }
        if(Input.GetKeyDown(KeyCode.Return) && idx == 3)  //  EXIT
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


}
