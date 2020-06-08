using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Back_Ground : MonoBehaviour
{

    public Sprite[] m_images;
    public int m_image_num;
    bool m_sun_shine;
    float m_t;
    void Awake()
    {
        m_sun_shine = false;
        m_image_num = DontDestroyManager.Map_Index / 10;
        m_t = 0.0f;
        this.GetComponentInChildren<Image>().sprite = m_images[m_image_num];
        this.GetComponentInChildren<Image>().color = new Color(0.7f, 0.7f, 0.7f);

        this.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_sun_shine) return;
        if (m_t >= 1.0f) return;
        m_t += 0.05f;
        if (m_t > 1.0f) m_t = 1.0f;
        this.GetComponentInChildren<Image>().color = Color.Lerp(new Color(0.7f, 0.7f, 0.7f), new Color(1.0f, 1.0f, 1.0f), m_t);
    }

    public void Set_Sun_Shine()
    {
        //this.GetComponentInChildren<Image>().color = new Color(1.0f, 1.0f, 1.0f);
        m_sun_shine = true;
    }
}
