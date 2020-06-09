using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgmkun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         FindObjectOfType<Audio_Manager>().Stop("Title");
         FindObjectOfType<Audio_Manager>().Stop("Result");
         FindObjectOfType<Audio_Manager>().Stop("rain1");
         FindObjectOfType<Audio_Manager>().Stop("");
         FindObjectOfType<Audio_Manager>().Play("select_world");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
