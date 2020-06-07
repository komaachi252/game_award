using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift_Block : MonoBehaviour
{
    // Start is called before the first frame update
    int m_index_i;
    int m_index_j;
    bool m_is_up;
    bool m_is_move = false;
    float m_dist_max;
    float m_dist;
    float m_move_speed = 0.05f;
    int m_wait_frame;
    int WAIT_FRAME = 20;
    void Start()
    {
        Set_Move_Distance();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }


    void Set_Move_Distance()
    {
        var map_data = GameObject.Find("MapLoader").GetComponent<MapLoader>().m_map_data;

        //  リフトレールが上か下か
        if(m_index_i <= 0)
        {
            m_is_up = false;
        }
        else if(m_index_i >= map_data.Height)
        {
            m_is_up = true;
        }
        else
        {
            //  上にレールがあるか
            if(map_data.Map_data[m_index_i - 1, m_index_j].Equals(26))
            {
                //Debug.Log("まずは");
                m_is_up = true;
            }
            else
            {
                m_is_up = false;
            }
        }

        int dist_cnt = 1;
        if(m_is_up)
        {
            while (true)
            {
                if (m_index_i - dist_cnt >= map_data.Height)
                    break;
                if(map_data.Map_data[m_index_i - dist_cnt, m_index_j].Equals(26))
                {
                    dist_cnt++;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            while (true)
            {
                if (m_index_i + dist_cnt <= map_data.Height)
                    break;
                if (map_data.Map_data[m_index_i + dist_cnt, m_index_j].Equals(26))
                {
                    dist_cnt++;
                }
                else
                {
                    break;
                }
            }
        }
        m_dist_max = (float)dist_cnt;
        m_dist = 0.0f;
        m_is_move = true;
    }

    public void Set_Map_Data_Index(int i, int j)
    {
        m_index_i = i;
        m_index_j = j;
    }
    private void Move()
    {
        if (!m_is_move)
        {
            m_wait_frame++;
            if(m_wait_frame >= WAIT_FRAME)
            {
                m_wait_frame = 0;
                m_is_move = true;
                m_dist = 0.0f;
            }
            return;
        }

        if (m_is_up)
        {
            this.transform.Translate(0, m_move_speed, 0);
            m_dist += m_move_speed;
            if(m_dist + m_move_speed > m_dist_max)
            {
                m_is_move = false;
                m_is_up = false;
            }

        }
        else
        {
            this.transform.Translate(0, -m_move_speed, 0);
            m_dist += m_move_speed;
            if (m_dist + m_move_speed > m_dist_max)
            {
                m_is_move = false;
                m_is_up = true;
            }
        }
    }
}
