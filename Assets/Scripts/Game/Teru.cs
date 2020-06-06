using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teru : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_is_rotate;

    float m_target_rotate = -180.0f;
    bool m_is_rotate_end;
    public bool Is_Rotate_End
    {
        get { return m_is_rotate_end;}
        set { m_is_rotate_end = value; }
    }
    void Start()
    {
        m_is_rotate = false;
        var scale = this.gameObject.transform.localScale;
        scale.y *= -1.0f;
        this.gameObject.transform.localScale = scale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!m_is_rotate) return;
        Rotate();
    }

    public void Rotate_Start()
    {
        m_is_rotate = true;
    }

    void Rotate()
    {
        var target = Quaternion.Euler(new Vector3(0, 0, m_target_rotate));
        var now_rot = transform.rotation;
        //  自角度と目標角度を比較
        Debug.Log(Quaternion.Angle(now_rot, target));
        if (Quaternion.Angle(now_rot, target) <= 90)
        {
            //  目標角度にする
            transform.rotation = target;
            this.gameObject.transform.Rotate(0.0f, 270.0f, 0.0f);
            m_is_rotate_end = true;
            m_is_rotate = false;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 3.0f));
        }
    }
}
