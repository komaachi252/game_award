using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Select_Bar : MonoBehaviour
{
    public int m_index; //  自分インデックス番号
    bool m_is_selected; //  選択中？
    public bool Is_Selected
    {
        get { return m_is_selected; }
        set { m_is_selected = value; }
    }
    bool m_is_wait;
    public bool Is_Wait
    {
        get{ return m_is_wait; }
        set { m_is_wait = value; }
    }

    float m_t;  //  補間用媒介変数
    Vector3 m_current_scale;  //  現在のスケール
    Vector3 m_target_scale = new Vector3(1.2f, 1.2f, 1.0f);  //  目標スケール
    Vector3 m_init_scale;  //  初期スケール
    void Awake()
    {
        m_init_scale = this.gameObject.transform.localScale;
        m_is_selected = false;
        m_t = 1.0f;
    }

    void FixedUpdate()
    {
        Change_Scale();
    }

    public void Set_Index(int index)
    {
        if (!m_is_wait) m_is_wait = true;
        //  自分と同じインデックスか？
        if(m_index == index)
        {
            m_t = 0.0f;
            m_is_selected = true;
            m_current_scale = this.transform.localScale;
            m_target_scale = m_current_scale;
            m_target_scale.x += 0.02f;
            m_target_scale.y += 0.02f;
        }
        //  選択から外れたか？
        if(m_index != index && m_is_selected)
        {
            m_t = 0.0f;
            m_is_selected = false;
            m_current_scale = this.transform.localScale;
            m_target_scale = m_init_scale;
        }
    }

    public void Change_Scale()
    {
        if (m_t >= 1.0f) return;
        m_t += 0.1f;
        if(m_t >= 1.0f)
        {
            m_t = 1.0f;
        }
        this.gameObject.transform.localScale = Vector3.Lerp(m_current_scale, m_target_scale, m_t);
    }

    public void Reset_Scale()
    {
        this.gameObject.transform.localScale = m_init_scale;
        m_is_wait = true;
    }
}
