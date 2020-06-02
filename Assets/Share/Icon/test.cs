using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject.Find("Icon").GetComponent<NoiseController>().ChangeIcon();//水から氷なる時以外
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject.Find("Icon").GetComponent<NoiseController>().ChangeIcon(true);//水から氷なる時
        }
    }
}
