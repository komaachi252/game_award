using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermill : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_trigger_right;
    public GameObject m_trigger_left;

    float m_rotate_speed = 0.0f;
    public float ADD_ROTATE_SPEED = 0.01f;
    int m_effect_frame_cnt;

    int splsh_count = 0;
    public float Rotate_Speed
    {
        get { return m_rotate_speed; }
    }
    public const float MAX_SPEED = 3.0f;
    void Start()
    {
        m_effect_frame_cnt = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_trigger_right.GetComponent<WatermillTrigger>().Is_Colli)
        {
            if (splsh_count == 0)
            {
                FindObjectOfType<Audio_Manager>().Play("splash");
                splsh_count = 170;
            }
            if (m_rotate_speed < MAX_SPEED)
            {
                m_rotate_speed += ADD_ROTATE_SPEED;
            }
        }
        else if (m_trigger_left.GetComponent<WatermillTrigger>().Is_Colli)
        {
            if (splsh_count == 0)
            {
                FindObjectOfType<Audio_Manager>().Play("splash");
                splsh_count = 170;
            }
            if (m_rotate_speed > -MAX_SPEED)
            {
                m_rotate_speed -= ADD_ROTATE_SPEED;
            }
        }
        else if (Mathf.Abs(m_rotate_speed) > 0.0f)
        {
            //Debug.Log(m_rotate_speed);
            FindObjectOfType<Audio_Manager>().Stop("splash");
            splsh_count = 0;
            m_rotate_speed *= 0.98f;
            if (Mathf.Abs(m_rotate_speed) < 0.5f)
            {
                m_rotate_speed = 0;
            }
        }
        Rotate();

        if (splsh_count > 0)
        {
            splsh_count--;
        }
    }

    void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 0, m_rotate_speed));
    }
}

