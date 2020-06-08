using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forced_Phase : MonoBehaviour
{
    public GameObject m_effect;
    public GameObject m_push_effect;
    public GameObject m_hard_effect;
    int m_spawn_frame = 0;
    bool m_is_trriger = false;
    public bool m_is_hot;
    Color m_current_color;
    Color m_break_color = new Color(0.9f, 0.9f, 0.9f);
    Material m_material;
    float m_t = 0.0f;
    bool m_is_stay = false;
    string m_tag_name;
    // Start is called before the first frame update
    void Start()
    {
        m_spawn_frame = Random.Range(0, 120);
    }


    private void Update()
    {
        if (m_is_stay)
        {
            Trans_Check();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_is_trriger)
        {
            Color_Change();
            
        }
        else if(gameObject.tag != "BLOCK")
        {
            m_spawn_frame--;
            if (m_spawn_frame > 0) return;

            var pos = transform.position;
            pos.y += 0.3f;
            pos.z -= 0.5f;
            Instantiate(m_effect, pos, Quaternion.identity);
            m_spawn_frame = Random.Range(30, 120);
        }
        
    }

    private void OnDestroy()
    {
        Destroy(m_material);
    }
    void Create_Effect()
    {
        var pos = this.transform.position;
        if (m_is_hot)
        {
            //pos.y += 0.3f;
        }
        else
        {
            //pos.y -= 0.5f;
        }
        pos.z -= 0.5f;
        Instantiate(m_push_effect, pos, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider col)
    {

    }

    void Color_Change()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(m_current_color, m_break_color, m_t);
        m_t += 0.01f;
        if(m_t > 1.0f)
        {
            m_is_trriger = false;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.CompareTag("SOLID") && !m_is_hot)
        {
            Instantiate(m_hard_effect, col.gameObject.transform.position, Quaternion.identity);
            FindObjectOfType<Audio_Manager>().Play("frozen");
        }

        if (col.gameObject.tag == "SOLID" || col.gameObject.tag == "AQUA" || col.gameObject.tag == "CLOUD")
        {
            if (!m_is_stay)
            {
                m_tag_name = col.gameObject.tag;
                m_is_stay = true;
            }
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "SOLID" || col.gameObject.tag == "AQUA" || col.gameObject.tag == "CLOUD")
        {
            if (m_is_stay)
            {
                m_is_stay = false;
            }
        }
    }

    void Trans_Check()
    {
        if (!GameObject.Find("PLAYER_MASTER")) return;
        if (GameObject.Find("PLAYER_MASTER").gameObject.tag != m_tag_name)
        {
            m_is_trriger = true;
            if (m_is_hot)
            {
                Material mat = Resources.Load<Material>("Game/Material/hot_2");
                m_material = new Material(mat);
                FindObjectOfType<Audio_Manager>().Play("heat1");
            }
            else
            {
                Material mat = Resources.Load<Material>("Game/Material/cold_2");
                m_material = new Material(mat);
                FindObjectOfType<Audio_Manager>().Play("frozen");
            }
            gameObject.GetComponent<Renderer>().material = m_material;
            m_current_color = m_material.color;
            gameObject.tag = "BLOCK";
            Create_Effect();

            m_tag_name = GameObject.Find("PLAYER_MASTER").gameObject.tag;
            Debug.Log(m_tag_name + "がでる");
            m_is_stay = false;
        }
    }
}

