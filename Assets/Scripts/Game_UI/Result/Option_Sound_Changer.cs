using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option_Sound_Changer : MonoBehaviour
{
    public GameObject m_yellow_bar;
    public bool m_is_bgm;
    bool m_is_trigger;
    float m_volume;
    int lock_L = 0;
    int lock_R = 0;
    public float Volume
    {
        get { return m_volume; }
    }
    float m_se_volume;
    bool m_is_wait;
    void Start()
    {
        m_yellow_bar.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        m_is_trigger = false;
        if (m_is_bgm)
        {
            m_volume = FindObjectOfType<MyBinaryReader>().BGM_volume;
        }
        else
        {
            m_volume = FindObjectOfType<MyBinaryReader>().SE_volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float x_axis = Input.GetAxis("Horizontal2");//右マイナス　左プラス

        int ARROW_L = 0;
        int ARROW_R = 0;

        if (x_axis > 0.5f && lock_L == 0)
        {
            ARROW_L = 1;
            lock_L = 25;
        }
        else if (x_axis < 0.5f)
        {
            lock_L = 0;
        }

        if (x_axis < -0.5f && lock_R == 0)
        {
            ARROW_R = 1;
            lock_R = 25;
        }
        else if (x_axis > -0.5f)
        {
            lock_R = 0;
        }

        if (GameObject.Find("Option_Menu").transform.localScale.x >= 1.0f && !m_is_trigger)
        {
            m_yellow_bar.transform.localScale = new Vector3(1.0f - m_volume, 1.0f, 1.0f);
            m_yellow_bar.transform.localPosition = new Vector3((430.0f * 0.5f) * m_volume, -25.3f, 0);

            m_is_trigger = true;
        }
        if (!this.GetComponentInParent<Title_Select_Bar>().Is_Selected)
        {
            return;
        }
        
        var volume = m_volume;

        if (Input.GetKeyDown(KeyCode.A) || ARROW_L == 1)
        {
            volume -= 0.1111f;
            if(volume <= 0.0f)
            {
                volume = 0.0f;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || ARROW_R == 1)
        {
            volume += 0.1111f;
            if(volume >= 1.0f)
            {
                volume = 1.0f;
            }
        }
        Set_Volume(volume);
        lock_update();
    }

    void Set_Volume(float volume)
    {
        if (m_volume == volume) return;
        FindObjectOfType<Audio_Manager>().Play("select");
        FindObjectOfType<Audio_Manager>().Set_Volume(m_is_bgm, volume);
        m_yellow_bar.transform.localScale = new Vector3(1.0f - volume, 1.0f, 1.0f);
        m_yellow_bar.transform.localPosition = new Vector3((430.0f * 0.5f) * volume, -25.3f, 0);

        if (m_is_bgm)
        {
            var se_volume = GameObject.Find("se_bar").GetComponent<Option_Sound_Changer>().Volume;
            GameObject.Find("BinaryReader").GetComponent<MyBinaryReader>().Save(volume, se_volume);
        }
        else
        {
            var bgm_volume = GameObject.Find("bgm_bar").GetComponent<Option_Sound_Changer>().Volume;
            GameObject.Find("BinaryReader").GetComponent<MyBinaryReader>().Save(bgm_volume, volume);
        }

        m_volume = volume;
    }

    void lock_update()
    {
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
