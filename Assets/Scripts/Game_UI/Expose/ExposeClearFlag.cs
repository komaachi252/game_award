using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/ClearFlag", fileName = "ExposeClearFlag")]

public class ExposeClearFlag : ScriptableObject
{

    public Game_Manager Expose_CrearFlag { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
