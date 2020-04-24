using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private float m_base_angle_z;  //  最初の角度
    private float m_target_angle_z = 90;  //  目的の角度
    private bool m_is_return = false; //  戻り中？

    private bool m_is_wait = false;
    private float m_move_speed = 0.0f;
    private float m_return_speed = -2.0f;
    private int m_wait_frame;
    public bool m_is_right;
    void Start()
    {
        m_base_angle_z = this.transform.rotation.z;
        if (!m_is_right){
            this.transform.Translate(0.5f, -1.5f, 0);
        }else
        {
            this.transform.Translate(0.5f, -1.5f, 0);
            m_return_speed *= - 1.0f;
            m_target_angle_z *= -1.0f;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_is_wait)
        {
            Wait();
            return;
        }

        if (!m_is_return)
        {
            var target = Quaternion.Euler(new Vector3(0, 0, m_target_angle_z));
            var now_rot = transform.rotation;
            if (Quaternion.Angle(now_rot, target) <= 1)
            {
                transform.rotation = target;
                m_is_wait = true;
                m_wait_frame = 30;
                m_move_speed = 0.0f;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, m_move_speed));
                if (!m_is_right)
                {
                    m_move_speed += 0.15f;
                }else
                {
                    m_move_speed -= 0.15f;
                }
            }
        }
        else
        {
            var target = Quaternion.Euler(new Vector3(0, 0, m_base_angle_z));
            var now_rot = transform.rotation;
            if (Quaternion.Angle(now_rot, target) <= 1)
            {
                transform.rotation = target;
                m_is_return = false;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, m_return_speed));
                
            }
        }
    }

    void Wait()
    {
        m_wait_frame--;
        if(m_wait_frame <= 0)
        {
            m_is_wait = false;
            m_is_return = true;
        }
    }
}
