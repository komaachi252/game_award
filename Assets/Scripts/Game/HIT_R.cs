using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_R : MonoBehaviour
{
    public PLAYER PLAYER;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)             //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("Hot"))     //加熱属性に当たった時
        {
            PLAYER.SET_stayHOT_R();
        }

        if (other.gameObject.CompareTag("Cold"))    //冷却属性に当たった時
        {
            PLAYER.SET_stayCOLD_R();
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("Hot"))     //加熱属性から離れた時
        {
            PLAYER.CLEAR_stayHOT();
        }

        if (other.gameObject.CompareTag("Cold"))    //冷却属性から離れた時
        {
            PLAYER.CLEAR_stayCOLD();
        }
    }
}
