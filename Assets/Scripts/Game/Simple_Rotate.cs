using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_rotate_speed_X;
    public float m_rotate_speed_Y;
    public float m_rotate_speed_Z;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Rotate(m_rotate_speed_X, m_rotate_speed_Y, m_rotate_speed_Z);
    }
}
