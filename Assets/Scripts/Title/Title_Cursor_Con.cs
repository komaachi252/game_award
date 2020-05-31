using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Cursor_Con : MonoBehaviour
{
    //  現在の選択インデックス
    int m_current_cursor_idx = 0;
    float m_pos_y;
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
        m_pos_y = this.transform.position.y;
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
            idx--;
            if (idx < 0)
            {
                idx = 3;
            }
            FindObjectOfType<Audio_Manager>().Play("select");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            idx++;
            if (idx > 3)
            {
                idx = 0;
            }
            FindObjectOfType<Audio_Manager>().Play("select");
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
        Debug.Log(m_pos_y + m_current_cursor_idx * -20.0f);
        this.transform.position = new Vector3(this.transform.position.x,  m_pos_y + m_current_cursor_idx * -20.0f, this.transform.position.z);
    }


}
