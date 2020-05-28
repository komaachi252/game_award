using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Cursor_Con : MonoBehaviour
{
    // Start is called before the first frame update
    //  現在の選択インデックス
    int m_current_cursor_idx = 0;
    //  ボタン長押し用
    int m_press_key_time = 0;
    float m_pos_y;
    public GameObject m_fade;
    void Start()
    {
        FindObjectOfType<Audio_Manager>().Play("rain1");
        FindObjectOfType<Audio_Manager>().Play("Title");
        //  NewGameから、セーブデータがあれば1からでもよい
        m_current_cursor_idx = 0;
        //  初期Ｙ座標を保持しておく
        m_pos_y = this.transform.position.y;
        m_fade.GetComponent<Game_Fade>().Fade_Start();
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKey(KeyCode.W))
        {
            //m_press_key_time++;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            //m_press_key_time--;
        }
        else
        {
            m_press_key_time = 0;
        }

        if(Mathf.Abs(m_press_key_time) > 20)
        {
            if (Input.GetKey(KeyCode.W))
            {
                idx--;
                if (idx < 0)
                {
                    idx = 3;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                idx++;
                if (idx > 3)
                {
                    idx = 0;
                }
            }

        }

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
        if(Input.GetKeyDown(KeyCode.Return) && idx == 0)
        {
            FindObjectOfType<Audio_Manager>().Play("enter");
            m_fade.GetComponent<Game_Fade>().Fade_Start(20, true, "WorldScene");
        }


        Cursor_Move(idx);

    }

    void Cursor_Move(int new_cursor_idx)
    {
        //  同じ場合は戻す
        if (m_current_cursor_idx == new_cursor_idx) return;

        //  インデックスに応じてＹ座標を移動
        m_current_cursor_idx = new_cursor_idx;
        this.transform.position = new Vector3(this.transform.position.x,  m_pos_y + m_current_cursor_idx * -20.0f, this.transform.position.z);
    }


}
