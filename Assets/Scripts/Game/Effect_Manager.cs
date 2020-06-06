using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_solid;
    public GameObject m_aqua;
    public GameObject m_gas;

    List<GameObject> m_right_effects = new List<GameObject>();
    List<GameObject> m_left_effects = new List<GameObject>();
    string m_player_tag;
    int m_pop_frame;
    const int POP_FRAME_MAX = 180;
    void Start()
    {
        m_pop_frame = 0;
        m_player_tag = "SOLID";
        Pop_Effect();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        m_pop_frame++;
        if (m_pop_frame >= POP_FRAME_MAX)
        {
            Pop_Effect();
            m_pop_frame = 0;
        }
    }

    void Update()
    {
        var pos_right = this.transform.position;
        pos_right.x += 4.5f;
        var pos_left = this.transform.position;
        pos_left.x -= 4.5f;
        
        for(int i = 0; i < m_right_effects.Count; i++)
        {
            if (m_right_effects[i] == null) continue;
            m_right_effects[i].transform.position = pos_right;
            if(m_right_effects[i].GetComponent<UI_Effect>().Get_Frame() > 480)
            {
                m_right_effects.RemoveAt(i);
                Destroy(m_right_effects[i]);
            }
        }

        for(int i = 0; i < m_left_effects.Count; i++)
        {
            if (m_right_effects[i] == null) continue;
            m_left_effects[i].transform.position = pos_left;
            if(m_left_effects[i].GetComponent<UI_Effect>().Get_Frame() > 480)
            {
                m_left_effects.RemoveAt(i);
                Destroy(m_left_effects[i]);
            }
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
            m_right_effects.Add(Instantiate(m_solid, pos_right, Quaternion.identity));
            m_left_effects.Add(Instantiate(m_solid, pos_left, Quaternion.identity));
        }
        if(m_player_tag == "AQUA")
        {
            m_right_effects.Add(Instantiate(m_aqua, pos_right, Quaternion.identity));
            m_left_effects.Add(Instantiate(m_aqua, pos_left, Quaternion.identity));
        }
        if (m_player_tag == "CLOUD")
        {
            m_right_effects.Add(Instantiate(m_gas, pos_right, Quaternion.identity));
            m_left_effects.Add(Instantiate(m_gas, pos_left, Quaternion.identity));
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
