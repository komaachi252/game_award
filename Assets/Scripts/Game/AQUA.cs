using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AQUA : MonoBehaviour
{
    int exchangeflag = 0;
    int exchangecount = 0;
    Vector3 size;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0.0f, 0.5f, 0.5f, 1.0f);
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
                size.x -= 0.01f;
                size.y -= 0.01f;
                size.z -= 0.01f;
                transform.localScale = size;
            }

            if (exchangecount >= 41 && exchangecount < 61)
            {
                size.x -= 0.03f;
                size.y -= 0.03f;
                size.z -= 0.03f;
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
                size.x += 0.05f;
                size.y += 0.05f;
                size.z += 0.05f;
                transform.localScale = size;
            }

            if (exchangecount == 61)
            {
                exchangecount = 0;
                exchangeflag = 0;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    public void exchange_s()
    {
        exchangeflag = 1;
        size = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void exchange_b()
    {
        exchangeflag = 2;
        size = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
