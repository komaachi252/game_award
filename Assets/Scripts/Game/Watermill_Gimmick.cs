using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Watermill_Gimmick : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] m_watermill = null;
    public GameObject m_trigger_up;
    public GameObject m_trigger_down;

    const float MAX_DISTANCE = 4.0f;
    float m_total_dist = 0.0f;
    bool m_is_move;  //  動かされているか？
    bool m_is_trigger;  //  動かされているか？

    float m_pos_y;
    //bool m_is_max_dist = false;
    enum Gate_Phase
    {
        Default,
        UP_Max
    };
    void Start()
    {
        this.gameObject.GetComponent<Transform>().transform.Translate(0, -0.5f, 0);
        m_is_move = false;
        m_is_trigger = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameObject.Find("MapLoader").GetComponent<MapLoader>().Is_Create) return;

        if (m_watermill == null)
        {
            Set_Watermill();
        }
        if (m_watermill == null) return;
        Move();

        
    }

    private void Move()
    {
        float move_dist = 0.0f;
        foreach(var watermill in m_watermill)
        {
            move_dist += watermill.GetComponent<Watermill>().Rotate_Speed * 0.01f;
        }
        if(!m_is_move && Mathf.Abs(move_dist) > 0.0f)
        {
            m_is_move = true;
        }

        if(!m_is_trigger && Mathf.Abs(move_dist) == 0.0f && m_is_move)
        {
            m_is_trigger = true;
        }
        
        if (move_dist > 0.0f && m_trigger_up.GetComponent<WaterGate_Trigger>().Is_Colli)
        {
            Debug.Log(move_dist + "up");
            return;
        }
        if (move_dist < 0.0f && m_trigger_down.GetComponent<WaterGate_Trigger>().Is_Colli)
        {
            Debug.Log(move_dist + "down");
            return;
        }
        
        if(m_is_trigger && m_is_move)
        {
            //m_pos_y = Mathf.Round(this.gameObject.transform.position.y + 0.5f);
            m_pos_y = (float)Math.Round(this.gameObject.transform.position.y + 0.5f, MidpointRounding.AwayFromZero);
           // m_is_trigger = false;
            m_is_move = false;
        }

        if (move_dist == 0.0f  && m_is_trigger)
        {
            if (m_pos_y > this.gameObject.transform.position.y + 0.5f)
            {
                move_dist += 0.01f;
                if (m_pos_y <= this.gameObject.transform.position.y + 0.5f + move_dist)
                {
                    m_is_trigger = false;
                    var pos = this.gameObject.transform.position;
                    pos.y = m_pos_y;
                }
            }
            if (m_pos_y < this.gameObject.transform.position.y + 0.5f)
            {
                move_dist -= 0.01f;
                if (m_pos_y >= this.gameObject.transform.position.y + 0.5f + move_dist)
                {
                    m_is_trigger = false;
                    var pos = this.gameObject.transform.position;
                    pos.y = m_pos_y;
                }
            }
        }


        this.transform.Translate(0.0f, move_dist, 0.0f);


    }

    void Set_Watermill()
    {
        m_watermill = GameObject.FindGameObjectsWithTag("WATERMILL");
    }

}
