using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/camera",fileName = "ExposeCamera")]

public class ExposeCamera : ScriptableObject
{

    public Camera maincamera { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
