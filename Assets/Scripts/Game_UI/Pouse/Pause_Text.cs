using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Text : MonoBehaviour
{
    int stage;
    int world;

    // Start is called before the first frame update
    void Start()
    {
        stage = (StageController.Get_Index() - (StageController.Get_world() * 10)) + 1;
        world = StageController.Get_world() + 1;
    }

    // Update is called once per frame
    void Update()
    {
        

        this.GetComponent<Text>().text = "WORLD " + world.ToString() + "-" + stage.ToString();
    }
}
