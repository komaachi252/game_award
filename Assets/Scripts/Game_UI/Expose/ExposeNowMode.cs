using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Expose Scene Property/NowMode", fileName = "ExposeNowMode")]

public class ExposeNowMode : ScriptableObject
{

    public NoiseController expose_now_mode { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
