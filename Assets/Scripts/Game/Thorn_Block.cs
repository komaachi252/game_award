using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn_Block : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_is_pop = true;
    bool m_is_move = false;
    float m_dist = 0.0f;
    void Start()
    {
        if (!m_is_pop)
        {
            this.transform.Translate(0, -0.9f, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_is_move)
        {
            Move(m_is_pop);
        }
    }

    public void Pop_Start()
    {
        if (m_is_pop)
        {
            m_is_pop = false;
        }
        else
        {
            m_is_pop = true;
        }
        m_is_move = true;
        m_dist = 0.0f;
    }

    private void Move(bool is_pop)
    {
        if (is_pop)
        {
            this.transform.Translate(0, 0.1f, 0);
            m_dist += 0.1f;
        }
        else
        {
            this.transform.Translate(0, -0.1f, 0);
            m_dist += 0.1f;
        }
        if(m_dist >= 0.9f)
        {
            m_is_move = false;
        }
    }
}
