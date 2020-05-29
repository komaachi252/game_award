using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_LT : MonoBehaviour
{
    public int HIT = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HIT = 0;
    }

    public int GETAIRFLAG()
    {
        return HIT;
    }

    void OnTriggerEnter(Collider other)             //他のオブジェクトとの接触時の処理
    {
        HIT = 1;
        if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY"))     //加熱属性に当たった時
        {
            HIT = 0;
        }
    }

    void OnTriggerStay(Collider other)             //他のオブジェクトとの接触時の処理
    {
        HIT = 1;
        if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY"))     //加熱属性に当たった時
        {
            HIT = 0;
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY"))     //加熱属性に当たった時
        {
            //HIT = 0;
        }
        else
        {
            HIT = 0;
        }
    }
}
