using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_UI : MonoBehaviour
{
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
            Debug.Log(m_player_tag);

            //  状態変化カウント
            if (m_phase_cnt < 99)
            {
                m_phase_cnt++;
            }
            //  回数をカウントUIに設定
            GameObject.Find("Canvas").GetComponent<Phase_Count>().View(m_phase_cnt);
            if(m_player_tag == "AQUA" && GameObject.Find("PLAYER_MASTER").gameObject.tag == "SOLID")
            {
                GameObject.Find("Icon").GetComponent<NoiseController>().ChangeIcon(true);//水から氷なる時
            }
            else
            {
                GameObject.Find("Icon").GetComponent<NoiseController>().ChangeIcon();
            }

            m_player_tag = GameObject.Find("PLAYER_MASTER").gameObject.tag;
            // GameObject.Find("Effect_Manager").gameObject.GetComponent<Effect_Manager>().Set_Player_Tag(m_player_tag);
        }
    }


}
