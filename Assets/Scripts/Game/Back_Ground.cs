using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Back_Ground : MonoBehaviour
{

    public Sprite[] m_images;
    public uint m_image_num;
    void Awake()
    {
        this.GetComponentInChildren<Image>().sprite = m_images[m_image_num];

        this.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
