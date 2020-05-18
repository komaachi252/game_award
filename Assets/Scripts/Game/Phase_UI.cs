using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] m_sprites;
    enum Phase_Index : int
    {
        Solid = 0,
        Aqua,
        Cloud
    };
    int m_sprite_index = (int)Phase_Index.Solid;
    string m_player_tag = "SOLID";
    int m_phase_cnt = 0;
    void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = m_sprites[m_sprite_index];
        m_phase_cnt = 0;
        m_player_tag = "SOLID";
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("PLAYER_MASTER")) return;
        if (!GameObject.Find("PLAYER_MASTER").gameObject.tag.Equals(m_player_tag))
        {
            m_player_tag = GameObject.Find("PLAYER_MASTER").gameObject.tag;
            if(m_player_tag == "SOLID")
            {
                m_sprite_index = (int)Phase_Index.Solid;
            }
            if(m_player_tag == "AQUA")
            {
                m_sprite_index = (int)Phase_Index.Aqua;
            }
            if (m_player_tag == "CLOUD")
            {
                m_sprite_index = (int)Phase_Index.Cloud;
            }
            this.gameObject.GetComponent<Image>().sprite = m_sprites[m_sprite_index];
            m_phase_cnt++;
            GameObject.Find("Phase_Count").gameObject.GetComponent<Phase_Count>().Set_Phase_Count(m_phase_cnt);
        }
    }


}
