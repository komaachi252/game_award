using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/StarPhese", fileName = "StarPhese")]

public class ExposeStarPhese : ScriptableObject
{

    public Star_PheseSet pheseset { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
