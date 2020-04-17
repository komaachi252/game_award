using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_L : MonoBehaviour
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

    void OnTriggerStay(Collider other)             //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("HOT"))     //加熱属性に当たった時
        {
            Debug.Log("fe");
            PLAYER.SET_stayHOT_L();
        }

        if (other.gameObject.CompareTag("COLD"))     //冷却属性に当たった時
        {
            PLAYER.SET_stayCOLD_L();
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("HOT"))     //加熱属性から離れたとき
        {
            PLAYER.CLEAR_stayHOT();
        }

        if (other.gameObject.CompareTag("COLD"))    //冷却属性から離れたとき
        {
            PLAYER.CLEAR_stayCOLD();
        }
    }
}
