using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_BGM : MonoBehaviour
{
    // Start is called before the first frame update
    string m_bgm_name;
    void Start()
    {
        FindObjectOfType<Audio_Manager>().Stop("select_world");
        FindObjectOfType<Audio_Manager>().Stop("Result");
        var index = DontDestroyManager.Map_Index / 10 + 1;
        var bgm_name = "world";
        m_bgm_name = bgm_name + index.ToString();
        FindObjectOfType<Audio_Manager>().Play(m_bgm_name);
        FindObjectOfType<Audio_Manager>().Play("rain1");

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            FindObjectOfType<Audio_Manager>().Stop(m_bgm_name);
            SceneManager.LoadScene("WorldScene");
        }
    }

    public void Stop_BGM()
    {
        FindObjectOfType<Audio_Manager>().Stop(m_bgm_name);
        FindObjectOfType<Audio_Manager>().Stop("rain1");

    }

    void OnDestroy()
    {
 //       FindObjectOfType<Audio_Manager>().Stop(m_bgm_name);
    }
}
