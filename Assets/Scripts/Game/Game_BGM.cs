using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_BGM : MonoBehaviour
{
    // Start is called before the first frame update
    string m_bgm_name;
    void Start()
    {
        FindObjectOfType<Audio_Manager>().Stop("select_world");
        var index = DontDestroyManager.Map_Index / 10 + 1;
        var bgm_name = "world";
        m_bgm_name = bgm_name + index.ToString();
        FindObjectOfType<Audio_Manager>().Play(m_bgm_name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stop_BGM()
    {
        FindObjectOfType<Audio_Manager>().Stop(m_bgm_name);
    }
}
