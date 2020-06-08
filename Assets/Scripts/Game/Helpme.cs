using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helpme : MonoBehaviour
{
    // Start is called before the first frame update
    // 救出したver
    public Sprite m_another_sprite;

    // 救出フラグ
    bool m_is_help = false;
    public bool Is_Help
    {
        get { return m_is_help; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Help()
    {
        //  救出フラグON
        this.gameObject.GetComponent<Image>().sprite = m_another_sprite;
        m_is_help = true;
    }

}
