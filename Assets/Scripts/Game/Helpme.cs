using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helpme : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite m_another_sprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Help()
    {
        this.gameObject.GetComponent<Image>().sprite = m_another_sprite;
    }

}
