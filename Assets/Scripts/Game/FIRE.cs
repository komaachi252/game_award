using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRE : MonoBehaviour
{
    Vector3 size;
    Vector3 pos;
    public Vector3 pos_BASE;
    int CHANGE = 0;
    int MODE = 0;
    int safe = 0;
    public GameObject m_fire;
    public GameObject m_fire_extinguish;
    int m_frame_cnt;
    bool m_is_pop;
    bool m_is_used;
    GameObject[] m_fires = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        pos_BASE = transform.position;
        pos_BASE.y -= 0.3f;
        m_frame_cnt = 0;
        m_is_pop = false;
        m_is_used = true;
        var f_pos = this.gameObject.transform.position;
        pos.y -= 0.3f;
        m_fires[0] = Instantiate(m_fire, f_pos, Quaternion.identity);
        size = new Vector3(0.5f, 0.5f, 0.5f);
    }



    // Update is called once per frame
    void Update()
    {
        if(MODE == 1)
        {
            size.y -= 0.005f;
            pos.y -= 0.0025f;

            if(size.y<0.001f)
            {
                size.y = 0.001f;
                MODE = 0;
            }

            //transform.localScale = size;
            //transform.position = pos;
        }

        if (MODE == 2)
        {
            size.y += 0.01f;
            pos.y += 0.005f;

            if (size.y > 0.5f)
            {
                size.y = 0.5f;
                pos = pos_BASE;
                MODE = 0;
            }

            //transform.localScale = size;
            //transform.position = pos;
        }

        m_fires[0].transform.localScale = size;
        m_fires[0].transform.position = pos;
        if (m_is_pop)
        {
            m_fires[1].transform.localScale = size;
            m_fires[1].transform.position = pos;
        }     

        if (!m_is_used)
        {
            var pos = this.gameObject.transform.position;
            pos.y -= 0.3f;

            Destroy(m_fires[0]);
            Destroy(m_fires[1]);

            Instantiate(m_fire_extinguish, pos, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (m_is_pop) return;
        m_frame_cnt++;
        if(m_frame_cnt > 30)
        {
            var pos = this.gameObject.transform.position;
            pos.y -= 0.3f;

            m_fires[1] = Instantiate(m_fire, pos, Quaternion.identity);
            m_is_pop = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //  氷との接触で消滅
        if (col.gameObject.CompareTag("SOLID"))
        {
            MODE = 1;
            size = new Vector3(0.5f, 0.5f, 0.5f);
            safe = 1;

            FindObjectOfType<Audio_Manager>().Play("heat1");
        }

        //  水との接触で消滅
        if (col.gameObject.CompareTag("AQUA") && safe == 0)
        {
            m_is_used = false;
            FindObjectOfType<Audio_Manager>().Play("extinguish");
            //this.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("Player"))     //加熱属性から離れた時
        {
            MODE = 2;
            size = new Vector3(0.5f, 0.001f, 0.5f);
            safe = 0;
        }
    }
}
