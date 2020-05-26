using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Fade : MonoBehaviour
{
    bool m_is_fade = false;  // フェード中判定
    bool m_is_fade_out = false; // フェードアウトか？
    int m_frame_cnt = 0;  // 現在のフレーム
    int m_fade_frame = 0; // フェードフレーム
    void Awake()
    {
        m_is_fade = false;
        m_is_fade_out = false;
        m_frame_cnt = 0;
        m_fade_frame = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_frame_cnt++;
        if (!m_is_fade) return;
        var color = this.gameObject.GetComponent<Image>().color;
        var elapsed_frame = m_frame_cnt - m_fade_frame;

        var e = elapsed_frame / (float)m_fade_frame;

        float alpha = 0.0f;

        alpha = m_is_fade_out ? e : 1.0f - e;

        if (elapsed_frame >= m_fade_frame)
        {
            m_is_fade = false;
            e = 1.0f;
        }
        if (m_is_fade_out)
        {
            alpha = e;
        }
        else
        {
            alpha = 1.0f - e;
        }
        color.a = alpha;
        this.gameObject.GetComponent<Image>().color = color;
    }

    public void Fade_Start(int fade_frame = 20, bool fade_out = false)
    {
        Debug.Log("フェードスタート");
        m_is_fade = true;
        m_is_fade_out = fade_out;
        m_fade_frame = fade_frame;
    }
}
