using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermill_Gimmick : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] m_watermill = null;

    const float MAX_DISTANCE = 4.0f;
    float m_total_dist = 0.0f;
    bool m_is_max_dist = false;

    public bool m_is_up;
    void Start()
    {
        //m_watermill = GameObject.FindGameObjectWithTag("Watermill");
        this.gameObject.GetComponent<Transform>().transform.Translate(0, -0.5f, 0);
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
        if (m_is_max_dist) return;

        float move_dist = 0.0f;
        foreach(var watermill in m_watermill)
        {
            move_dist += watermill.GetComponent<Watermill>().Rotate_Speed * 0.01f;
        }

        if(m_total_dist + move_dist > MAX_DISTANCE)
        {
            move_dist = MAX_DISTANCE - m_total_dist;
            m_is_max_dist = true;
        }
        m_total_dist += move_dist;
        if (!m_is_up) move_dist *= -1.0f;
        this.transform.Translate(0.0f, move_dist, 0.0f);
    }

    void Set_Watermill()
    {
        m_watermill = GameObject.FindGameObjectsWithTag("WATERMILL");
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);
        if (col.gameObject.CompareTag("BLOCK"))
        {
            m_is_max_dist = true;
        }

    }
}
