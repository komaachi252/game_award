﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Back_Ground : MonoBehaviour
{

    public Sprite[] m_images;
    public int m_image_num;
    void Awake()
    {
        m_image_num = DontDestroyManager.Map_Index / 10;

        this.GetComponentInChildren<Image>().sprite = m_images[m_image_num];
        this.GetComponentInChildren<Image>().color = new Color(0.8f, 0.8f, 0.8f);

        this.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
