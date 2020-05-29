using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_UI : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] m_sprites; // 各状態のUI
    enum Phase_Index : int
    {
        Solid = 0,
        Aqua,
        Cloud
    };
    int m_sprite_index = (int)Phase_Index.Solid;  //  インデックスによってUIを変える

    string m_player_tag = "SOLID"; //  タグ変化判定用

    //   変化回数
    int m_phase_cnt = 0;
    public int Phase_Cnt
    {
        get { return m_phase_cnt; }
        set { m_phase_cnt = value; }
    }
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
        // プレイヤーのタグを監視
        if (!GameObject.Find("PLAYER_MASTER").gameObject.tag.Equals(m_player_tag))
        {
            m_player_tag = GameObject.Find("PLAYER_MASTER").gameObject.tag;
            Debug.Log(m_player_tag);
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
            //  Spriteを変更
            this.gameObject.GetComponent<Image>().sprite = m_sprites[m_sprite_index];
            //  状態変化カウント
            m_phase_cnt++;
            //  回数をカウントUIに設定
            GameObject.Find("Phase_Count").gameObject.GetComponent<Phase_Count>().Set_Phase_Count(m_phase_cnt);
           // GameObject.Find("Effect_Manager").gameObject.GetComponent<Effect_Manager>().Set_Player_Tag(m_player_tag);
        }
    }


}
