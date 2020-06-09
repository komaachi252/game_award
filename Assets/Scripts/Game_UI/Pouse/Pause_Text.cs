using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Text : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int stage = StageController.Get_Index() - (StageController.Get_world() * 10) + 1;
        int world = StageController.Get_world() + 1;

        this.GetComponent<Text>().text = "WORLD " + world.ToString() + "-" + stage.ToString();
    }
}
