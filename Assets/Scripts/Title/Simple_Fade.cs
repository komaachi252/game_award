using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Simple_Fade : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_is_fade = false;
    bool m_is_trigger = false;
    string m_scene_name;
    public bool Is_Trigger
    {
        get { return m_is_trigger; }
    }
    void Start()
    {
        m_is_fade = false;
        m_is_trigger = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_is_fade) return;
        var color = this.gameObject.GetComponent<Image>().color;
        if (color.a >= 1.0f)
        {
            color.a = 1.0f;
            m_is_fade = false;
            m_is_trigger = true;
            SceneManager.LoadScene(m_scene_name);
        }
        else
        {
            color.a += 0.02f;
            this.gameObject.GetComponent<Image>().color = color;
        }
    }

    public void Start_Fade(string scene_name)
    {
        m_is_fade = true;
        m_scene_name = scene_name;
    }
}
