using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Block : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_is_push = false;
    bool m_is_move = false;
    bool m_is_exit = false;
    const float MAX_DIST = 0.15f;
    float m_dist = 0.0f;
    int m_exit_frame = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_is_move)
        {
            Push();
        }

        if (m_is_exit && !m_is_push)
        {
            m_exit_frame++;
            if(m_exit_frame > 20)
            {
                Push_Reset();
            }
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("ICE") && !m_is_exit && !m_is_push)
        {
            m_is_move = true;
            m_dist = 0.0f;
            m_is_push = true;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("ICE"))
        {
            m_is_exit = true;
        }
    }

    void Push()
    {
        if (m_dist >= MAX_DIST)
        {
            m_is_move = false;
            Pushed_Move();
            m_is_push = false;
            m_dist = MAX_DIST;
        }
        else
        {
            this.transform.Translate(0, -0.01f, 0);
            m_dist += 0.01f;
        }
    }

    void Push_Reset()
    {
        if (m_dist <= 0.0f)
        {
            m_is_exit = false;
            m_exit_frame = 0;
            m_dist = 0.0f;
        }
        else
        {
            this.transform.Translate(0, 0.01f, 0);
            m_dist -= 0.01f;
        }
    }

    void Pushed_Move()
    {
        
        foreach(var thorn in GameObject.FindGameObjectsWithTag("THORN"))
        {
            thorn.GetComponent<Thorn_Block>().Pop_Start();
        }
    }
}
