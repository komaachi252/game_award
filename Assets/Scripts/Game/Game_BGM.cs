using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Audio_Manager>().Stop("Result");
        var index = DontDestroyManager.Map_Index / 10 + 1;
        var bgm_name = "world";
        bgm_name = bgm_name + index.ToString();
        FindObjectOfType<Audio_Manager>().Play(bgm_name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
