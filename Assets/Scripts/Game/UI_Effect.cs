using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Effect : MonoBehaviour
{
    // Start is called before the first frame update
    int m_life_frame;
   
    void Start()
    {
        m_life_frame = 0;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        m_life_frame++;
    }

    public int Get_Frame()
    {
        return m_life_frame;
    }
}
