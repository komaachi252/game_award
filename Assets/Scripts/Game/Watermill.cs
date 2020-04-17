using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermill : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_is_aqua_colli = false;
    float m_rotate_speed = 0.0f;
    public float ADD_ROTATE_SPEED = 0.01f;
    public float Rotate_Speed
    {
        get { return m_rotate_speed; }
    }
    public const float MAX_SPEED = 3.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_is_aqua_colli)
        {
            if(m_rotate_speed < MAX_SPEED)
            {
                m_rotate_speed += ADD_ROTATE_SPEED;
            }
            
        }

        if(!m_is_aqua_colli && m_rotate_speed > 0.0f)
        {
            m_rotate_speed *= 0.98f;
        }
        Rotate();
    }

    void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 0, m_rotate_speed));
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            m_is_aqua_colli = true;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            m_is_aqua_colli = false;
        }
    }
}
