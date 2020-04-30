using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Block : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_is_right = false;
    public bool m_is_colli = false;
    public bool m_is_cloud = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_is_colli)
        {
            //this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }
        if (m_is_cloud)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Parent_Reset();
                m_is_cloud = false;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Parent_Reset();
                m_is_cloud = false;
            }
            return;
        }


        if (m_is_right)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //Debug.Log("Left_null");
                Parent_Reset();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Parent_Reset();
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("SOLID"))
        {
            if(col.gameObject.transform.position.x < this.transform.position.x)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    m_is_right = true;
                    m_is_colli = true;
                    this.transform.parent = col.gameObject.transform;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    m_is_right = false;
                    m_is_colli = true;
                    this.transform.parent = col.gameObject.transform;
                }
            }
        }
        if(col.gameObject.CompareTag("CLOUD"))
        {
            this.transform.parent = col.gameObject.transform;
            m_is_colli = true;
            m_is_cloud = true;
        }

    }

    private void OnCollisionExit(Collision col)
    {
        if(col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("CLOUD"))
        {
            Parent_Reset();
        }
    }

    void Parent_Reset()
    {
        this.transform.parent = null;
        m_is_colli = false;
    }
}
