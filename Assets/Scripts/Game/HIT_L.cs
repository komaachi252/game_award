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

    void OnTriggerEnter(Collider other)             //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("BLOCK"))     //加熱属性に当たった時
        {
            PLAYER.SET_stay_WALL_L();
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("BLOCK"))     //加熱属性から離れた時
        {
            PLAYER.CLEAR_stay_WALL_L();
        }
    }
}