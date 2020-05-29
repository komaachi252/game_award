using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_rotate_speed;
    private const float SUB_SPEED = 0.96f;
    bool m_aqua_colli = false;
    float m_t = 0.0f;
    Color m_rusted_color = new Color(0.44f, 0.23f, 0.12f);
    void Start()
    {
        //transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        this.gameObject.GetComponent<Transform>().transform.Translate(0.5f, -0.5f, 0);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_aqua_colli && m_rotate_speed > 0.0f)
        {
            m_rotate_speed *= SUB_SPEED;
        }
        if (Mathf.Abs(m_rotate_speed) > 0.0f)
        {
            this.transform.Rotate(new Vector3(0, 0, m_rotate_speed));
        }
        if (m_aqua_colli && m_t < 1.0f)
        {
            m_t += 0.001f;
            for (int i = 0; i < this.gameObject.GetComponent<Renderer>().materials.Length; i++)
            {
                //  錆び色と線形補間
                this.gameObject.GetComponent<Renderer>().materials[i].color = Color.Lerp(this.gameObject.GetComponent<Renderer>().materials[i].color, m_rusted_color, m_t);
            }

        }
        if (m_t > 1.0f)
        {
            m_t = 1.0f;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            m_aqua_colli = true;
            this.gameObject.tag = "Untagged";
        }
    }
}
