using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_is_right;
    public float m_rotate_speed;

    void Start()
    {
        if (m_is_right)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            m_rotate_speed *= -1.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        this.transform.Rotate(new Vector3(0, 0, -m_rotate_speed));
    }
}
