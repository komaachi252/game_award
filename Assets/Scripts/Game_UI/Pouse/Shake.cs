using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    int count = 0;
    float spin = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count++;

        if(count >= 61 && count < 66)
        {
            spin += 1.0f;
        }

        if(count >= 66 && count < 71)
        {
            spin -= 1.0f;
        }

        if(count >= 71 && count < 76)
        {
            spin += 1.0f;
        }

        if (count >= 76 && count < 81)
        {
            spin -= 1.0f;
        }

        if(count == 120)
        {
            count = 0;
        }

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, spin);
    }
}
