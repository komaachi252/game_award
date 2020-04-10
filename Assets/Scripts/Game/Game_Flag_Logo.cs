using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Flag_Logo : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] m_images;
    public uint m_image_num;
    void Awake()
    {
        this.GetComponentInChildren<Image>().sprite = m_images[m_image_num];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
