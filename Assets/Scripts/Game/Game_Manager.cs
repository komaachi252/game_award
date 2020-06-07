using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool m_is_clear_flag;
    public GameObject m_game_flag_logo;
    public bool Is_Clear_Flag
    {
        set { m_is_clear_flag = value; }
        get { return m_is_clear_flag; }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Game_Clear()
    {
        Instantiate(m_game_flag_logo, new Vector3(1,1,1), Quaternion.identity);
    }
}
