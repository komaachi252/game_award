using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Back : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] m_sprites;
    void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = m_sprites[Random.Range(0, m_sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
