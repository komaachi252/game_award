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
        if (m_trigger_right.GetComponent<WatermillTrigger>().Is_Colli)
        {
            Debug.Log("Right");
            if (m_rotate_speed < MAX_SPEED)
            {
                m_rotate_speed += ADD_ROTATE_SPEED;
            }
        }
        else if (m_trigger_left.GetComponent<WatermillTrigger>().Is_Colli)
        {

            Debug.Log("Left");
            if (m_rotate_speed > -MAX_SPEED)
            {
                m_rotate_speed -= ADD_ROTATE_SPEED;
            }
        }
        else if(Mathf.Abs(m_rotate_speed) > 0.0f)
        {
            m_rotate_speed *= 0.98f;
        }
        Rotate();
    }

    void Rotate()
    {
        this.transform.Rotate(new Vector3(0, 0, m_rotate_speed));
    }
}
