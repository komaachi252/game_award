using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    public const string GAME_MANAGER = "Game_Manager";
    public GameObject m_teru;
    public GameObject m_effect;
    bool once = false;
    void Start()
    {
        this.transform.Rotate(0, 90, 0);
        this.transform.Translate(-0.5f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_teru.GetComponent<Teru>().Is_Rotate_End)
        {
            if (once == false)
            {
                GameObject.Find(GAME_MANAGER).GetComponent<Game_Manager>().Game_Clear();
                once = true;
            }
        }

    }

    private void OnCollisionEnter(Collision col)
    {
        //!  プレイヤーだった場合ゲームクリア呼び出し
        if (col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            FindObjectOfType<Audio_Manager>().Play("game_clear");
       
            //GameObject.Find(GAME_MANAGER).GetComponent<Game_Manager>().Game_Clear();
            m_teru.GetComponent<Teru>().Rotate_Start();
            Instantiate(m_effect, this.gameObject.transform.position, Quaternion.identity);
        }
    }
}
