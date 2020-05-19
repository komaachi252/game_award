using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_effect;

    public bool m_is_hot;
    public string m_tag_name = "P";
    bool m_is_stay = false;
    //public GameObject instance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_is_stay)
        {
            Trans_Check();
        }
    }

    void Create_Effect()
    {
        var pos = this.transform.position;
        if (m_is_hot)
        {
            pos.y += 0.3f;
        }
        else
        {
            pos.y -= 0.5f;
        }
        pos.z -= 0.5f;
        Instantiate(m_effect, pos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            //Create_Effect();
            if (m_is_hot)
            {
               // FindObjectOfType<Audio_Manager>().Play("heat1");
            }
            else
            {
                //FindObjectOfType<Audio_Manager>().Play("frozen");
            }

        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            if (!m_is_stay)
            {
                m_is_stay = true;
                m_tag_name = col.gameObject.tag;
            
            }
            /*
            if (m_tag_name != col.gameObject.tag && !m_is_stay)
            {
                m_tag_name = col.gameObject.tag;
                Debug.Log(m_tag_name);
                m_is_stay = true;
            }
            */
        }
    }

    void Trans_Check()
    {
        if (!GameObject.Find("PLAYER_MASTER")) return;
        if (GameObject.Find("PLAYER_MASTER").gameObject.tag != m_tag_name)
        {
            Create_Effect();
            if (m_is_hot)
            {
                FindObjectOfType<Audio_Manager>().Play("heat1");
            }
            else
            {
                FindObjectOfType<Audio_Manager>().Play("frozen");
            }
            m_tag_name = GameObject.Find("PLAYER_MASTER").gameObject.tag;
            Debug.Log(m_tag_name + "がでる");
            m_is_stay = false;
        }

    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            if (m_is_stay)
            {
                m_is_stay = false;
                
                //m_tag_name = col.gameObject.tag;
            }
            /*
            if (m_tag_name != col.gameObject.tag && !m_is_stay)
            {
                m_tag_name = col.gameObject.tag;
                Debug.Log(m_tag_name);
                m_is_stay = true;
            }
            */
        }
    }


    private void OnCollisionStay(Collision col)
    {
        /*
        if (col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            //Debug.Log("ステイ");
            if (m_tag_name != col.gameObject.tag)
            {
                Create_Effect();
                if (m_is_hot)
                {
                    FindObjectOfType<Audio_Manager>().Play("heat1");
                }
                else
                {
                    FindObjectOfType<Audio_Manager>().Play("frozen");
                }
                m_tag_name = col.gameObject.tag;
                Debug.Log(m_tag_name + "がでる");
                m_is_stay = false;
            }
        }*/
    }
}
