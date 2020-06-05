using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/Phese_count", fileName = "Phese_count")]

public class ExposePheseCount : ScriptableObject
{

    public Phase_UI Phese_cnt { get; set; }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
