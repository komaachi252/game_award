using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Fade : MonoBehaviour
{
    bool m_is_fade = false;  // フェード中判定
    public bool Is_Fade
    {
        get { return m_is_fade; }
    }
    bool m_is_fade_out = false; // フェードアウトか？
    int m_frame_cnt = 0;  // 現在のフレーム
    int m_fade_frame = 0; // フェードフレーム
    string m_next_scene; //  次のシーン名
    string m_addition_scene;//　次の加算シーン名
    
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
        if (!m_is_fade) return;
        m_frame_cnt++;

        var color = this.gameObject.GetComponent<Image>().color;
        //var elapsed_frame = m_frame_cnt - m_fade_frame;
        var elapsed_frame = m_frame_cnt;

        var e = elapsed_frame / (float)m_fade_frame;

        float alpha = 0.0f;

        alpha = m_is_fade_out ? e : 1.0f - e;

        if (elapsed_frame >= m_fade_frame)
        {
            m_is_fade = false;
            e = 1.0f;
            if (m_is_fade_out)
            {
                SceneManager.LoadScene(m_next_scene);
                if(m_addition_scene != "NONE")
                    SceneManager.LoadScene(m_addition_scene, LoadSceneMode.Additive);
            }
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

    public void Fade_Start(int fade_frame = 20, bool fade_out = false, string next_scene = "NONE",string addition_scene = "NONE")
    {
        if (m_is_fade) return;

        Debug.Log("フェードスタート");
        m_is_fade = true;
        m_is_fade_out = fade_out;
        m_fade_frame = fade_frame;
        m_next_scene = next_scene;
        m_addition_scene = addition_scene;
        m_frame_cnt = 0;
    }
}
