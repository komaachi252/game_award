using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIRE : MonoBehaviour
{
    Vector3 size;
    Vector3 pos;
    public Vector3 pos_BASE;
    int CHANGE = 0;
    int MODE = 0;
    // Start is called before the first frame update
    void Start()
    {
        pos_BASE = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(MODE == 1)
        {
            size.y -= 0.01f;
            pos.y -= 0.005f;

            if(size.y<0.001f)
            {
                size.y = 0.001f;
                MODE = 0;
            }

            transform.localScale = size;
            transform.position = pos;
        }

        if (MODE == 2)
        {
            size.y += 0.02f;
            pos.y += 0.01f;

            if (size.y > 1)
            {
                size.y = 1;
                pos = pos_BASE;
                MODE = 0;
            }

            transform.localScale = size;
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //  氷との接触で消滅
        if (col.gameObject.CompareTag("SOLID"))
        {
            MODE = 1;
            size = new Vector3(1.0f, 1.0f, 1.0f);
            pos = transform.position;
        }

        //  水との接触で消滅
        if (col.gameObject.CompareTag("AQUA") && size.y == 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("Player"))     //加熱属性から離れた時
        {
            MODE = 2;
            size = new Vector3(1.0f, 0.001f, 1.0f);
            //pos = transform.position;
        }
    }
}
