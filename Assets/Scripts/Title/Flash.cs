using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    // Start is called before the first frame update
    Image m_img;

    int m_rand_frame;
    void Start()
    {
        m_img = GetComponent<Image>();
        m_img.color = Color.clear;
        m_rand_frame = Random.Range(120, 240);

    }

    void FixedUpdate()
    {
        m_rand_frame--;
        if(m_rand_frame <= 0)
        {
            Set_Flash();
            m_rand_frame = Random.Range(360, 480);
        }

        this.m_img.color = Color.Lerp(this.m_img.color, Color.clear, Time.deltaTime);
    }
    void Set_Flash()
    {
        FindObjectOfType<Audio_Manager>().Play("thunder");
        this.m_img.color = new Color(1.0f, 1.0f, 0.9f, 1.0f);
    }


}
