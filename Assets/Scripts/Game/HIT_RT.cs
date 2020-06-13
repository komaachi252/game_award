using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_RT : MonoBehaviour
{
    public int HIT = 0;
    public PLAYER PLAYER;
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
        int type = PLAYER.GET_TYPE();

        if (type != 2)
        {
            if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY") || other.gameObject.CompareTag("LEAF") || other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("AQUA") || other.gameObject.CompareTag("SOLID") || other.gameObject.CompareTag("CLOUD") || other.gameObject.CompareTag("LIFTZOON"))     //加熱属性に当たった時
            {
                HIT = 0;
            }
        }

        if (type == 2)
        {
            if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY") || other.gameObject.CompareTag("LEAF") || other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("AQUA") || other.gameObject.CompareTag("SOLID") || other.gameObject.CompareTag("CLOUD") || other.gameObject.CompareTag("LIFTZOON") || other.gameObject.CompareTag("SPONGE"))     //加熱属性に当たった時
            {
                HIT = 0;
            }
        }
    }

    void OnTriggerStay(Collider other)             //他のオブジェクトとの接触時の処理
    {
        HIT = 1;
        int type = PLAYER.GET_TYPE();

        if (type != 2)
        {
            if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY") || other.gameObject.CompareTag("LEAF") || other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("AQUA") || other.gameObject.CompareTag("SOLID") || other.gameObject.CompareTag("CLOUD") || other.gameObject.CompareTag("LIFTZOON"))     //加熱属性に当たった時
            {
                HIT = 0;
            }
        }

        if (type == 2)
        {
            if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY") || other.gameObject.CompareTag("LEAF") || other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("AQUA") || other.gameObject.CompareTag("SOLID") || other.gameObject.CompareTag("CLOUD") || other.gameObject.CompareTag("LIFTZOON") || other.gameObject.CompareTag("SPONGE"))     //加熱属性に当たった時
            {
                HIT = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        int type = PLAYER.GET_TYPE();

        if (type != 2)
        {
            if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY") || other.gameObject.CompareTag("LEAF") || other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("AQUA") || other.gameObject.CompareTag("SOLID") || other.gameObject.CompareTag("CLOUD") || other.gameObject.CompareTag("LIFTZOON"))     //加熱属性に当たった時
            {
                //HIT = 0;
            }
            else
            {
                HIT = 0;
            }
        }

        if (type == 2)
        {
            if (other.gameObject.CompareTag("HAMMER") || other.gameObject.CompareTag("WINDMILL") || other.gameObject.CompareTag("SKY") || other.gameObject.CompareTag("LEAF") || other.gameObject.CompareTag("LEAF_INV") || other.gameObject.CompareTag("AQUA") || other.gameObject.CompareTag("SOLID") || other.gameObject.CompareTag("CLOUD") || other.gameObject.CompareTag("LIFTZOON") || other.gameObject.CompareTag("SPONGE"))     //加熱属性に当たった時
            {
                //HIT = 0;
            }
            else
            {
                HIT = 0;
            }
        }
    }
}
