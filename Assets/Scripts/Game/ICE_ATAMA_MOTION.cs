using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_ATAMA_MOTION : MonoBehaviour
{
    float move_x = -0.01f;
    float move_y = 0.04f;
    int CRUSH_F = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CRUSH_F == 1)
        {
            transform.Translate(0.005f,move_y,move_x);
            move_y -= 0.001f;
        }
    }

    public void CRUSH()
    {
        CRUSH_F = 1;
    }
}
