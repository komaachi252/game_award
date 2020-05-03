using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLOUD : MonoBehaviour
{
    int exchangeflag = 0;
    int exchangecount = 0;
    Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //縮小処理
        if (exchangeflag == 1)
        {
            exchangecount++;
            if (exchangecount < 41)
            {
                size.x -= 0.1f;
                size.y -= 0.1f;
                size.z -= 0.1f;
                transform.localScale = size;
            }

            if (exchangecount >= 41 && exchangecount < 61)
            {
                size.x -= 0.3f;
                size.y -= 0.3f;
                size.z -= 0.3f;
                transform.localScale = size;
            }

            if (exchangecount == 61)
            {
                exchangecount = 0;
                exchangeflag = 0;
                transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }

        //拡大処理
        if (exchangeflag == 2)
        {
            exchangecount++;

            if (exchangecount > 51 && exchangecount < 61)
            {
                size.x += 0.5f;
                size.y += 0.5f;
                size.z += 0.5f;
                transform.localScale = size;
            }

            if (exchangecount == 61)
            {
                exchangecount = 0;
                exchangeflag = 0;
                transform.localScale = new Vector3(10.0f, 10.0f, 10.0f);
            }
        }
    }

    public void exchange_s()
    {
        exchangeflag = 1;
        size = new Vector3(10.0f, 10.0f, 10.0f);
    }

    public void exchange_b()
    {
        exchangeflag = 2;
        size = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
