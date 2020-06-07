using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICE_kubiwa_MOTION : MonoBehaviour
{
    float move_y = 0.05f;
    int CRUSH_F = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CRUSH_F == 0)
        {
            transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));
        }

        if(CRUSH_F == 1)
        {
            transform.Translate(0.0f, move_y, 0.0f);
            move_y -= 0.0015f;
        }
    }

    public void CRUSH()
    {
        CRUSH_F = 1;
    }
}
