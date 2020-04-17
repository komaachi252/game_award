using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_U : MonoBehaviour
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
        PLAYER.SET_STAND_U();
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        PLAYER.CLARE_STAND();
    }
}
