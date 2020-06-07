using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/ExposeIsHelp", fileName = "ExposeIsHelp")]

public class ExposeIsHelp : ScriptableObject
{

    public Helpme Expose_Helpme { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
