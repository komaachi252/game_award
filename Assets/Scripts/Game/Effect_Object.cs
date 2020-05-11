using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Object : MonoBehaviour
{
    // Start is called before the first frame update
    public int m_life_frame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_life_frame--;
        if(m_life_frame <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
