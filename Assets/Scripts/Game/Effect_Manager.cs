using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_solid;
    public GameObject m_aqua;
    public GameObject m_gas;
    string m_player_tag;
    int m_pop_frame;
    const int POP_FRAME_MAX = 180;
    void Start()
    {
        m_pop_frame = 0;
        m_player_tag = "SOLID";
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        m_pop_frame++;
        if(m_pop_frame >= POP_FRAME_MAX)
        {
            Pop_Effect();
            m_pop_frame = 0;
        }
    }

    void Pop_Effect()
    {
        var pos_right = this.transform.position;
        pos_right.x += 4.5f;
        var pos_left = this.transform.position;
        pos_left.x -= 4.5f;
        if (m_player_tag == "SOLID")
        {
            Instantiate(m_solid, pos_right, Quaternion.identity);
            Instantiate(m_solid, pos_left, Quaternion.identity);
        }
        if(m_player_tag == "AQUA")
        {
            Instantiate(m_aqua, pos_right, Quaternion.identity);
            Instantiate(m_aqua, pos_left, Quaternion.identity);

        }
        if (m_player_tag == "CLOUD")
        {
            Instantiate(m_gas, pos_right, Quaternion.identity);
            Instantiate(m_gas, pos_left, Quaternion.identity);
        }
    }

    public void Set_Player_Tag(string tag_name)
    {
        if (m_player_tag == tag_name) return;
        Debug.Log(m_player_tag);
        m_player_tag = tag_name;
        Pop_Effect();
    }

}
