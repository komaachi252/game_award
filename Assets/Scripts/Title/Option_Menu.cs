using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Menu : MonoBehaviour
{
    // Start is called before the first frame update
    float m_t;  //  補間用媒介変数
    bool m_is_open; // メニューが開いているか？
    Vector3 m_current_scale;  //  現在のスケール
    Vector3 m_target_scale = new Vector3(1.0f, 1.0f, 1.0f);  //  目標スケール
    Vector3 m_init_scale;  //  初期スケール
    void Start()
    {
        m_init_scale = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Change_Scale();   
    }

    void Change_Scale()
    {
        if (m_t >= 1.0f) return;
        m_t += 0.1f;
        if(m_t >= 1.0f)
        {
            m_t = 1.0f;
            if(!m_is_open)
            {
                GameObject.Find("Cursor").GetComponent<Title_Cursor_Con>().Is_Wait = false;
                this.gameObject.SetActive(false);
            }
        }
        this.gameObject.transform.localScale = Vector3.Lerp(m_current_scale, m_target_scale, m_t);
    }

    public void Open_Menu()
    {
        if (m_is_open) return;
        m_t = 0.0f;
        m_is_open = true;

        m_current_scale = this.transform.localScale;
        m_target_scale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void Close_Menu()
    {
        m_t = 0.0f;
        m_current_scale = this.transform.localScale;
        m_target_scale = new Vector3(0.0f, 0.0f, 0.0f);
        m_is_open = false;
    }
}
