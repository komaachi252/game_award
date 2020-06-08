using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Enable()
    {
        FindObjectOfType<Audio_Manager>().Stop("rain1");
        FindObjectOfType<Back_Ground>().Set_Sun_Shine();
        this.gameObject.SetActive(false);
    }
}
