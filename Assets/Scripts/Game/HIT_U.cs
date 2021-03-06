﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_U : MonoBehaviour
{
    public PLAYER PLAYER;
    Thorn_Block Thorn_Block;
    Collider Collider;
    Forced_Phase Forced_Phase;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SETWATERTrigger()
    {
        Debug.Log("着地");
        Collider.isTrigger = false;
    }

    public void USE()
    {
        Forced_Phase.USE();
    }



    void OnTriggerStay(Collider other)             //他のオブジェクトとの接触時の処理
    {
        if (other.gameObject.CompareTag("BLOCK") || other.gameObject.CompareTag("HOT") || other.gameObject.CompareTag("COLD") ||
            other.gameObject.CompareTag("THORN_BLOCK") || other.gameObject.CompareTag("GAP") || other.gameObject.CompareTag("WATER") ||
            other.gameObject.CompareTag("DRAIN") || other.gameObject.CompareTag("LIFT") || other.gameObject.CompareTag("HARD_HOT") || other.gameObject.CompareTag("HARD_COLD"))
        {
            //Debug.Log("下");
            PLAYER.SET_STAND_U();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GAP"))
        {
            PLAYER.SET_stay_GAP_U();
        }

        if (other.gameObject.CompareTag("HOT"))
        {
            PLAYER.SET_stay_HOT();
        }

        if (other.gameObject.CompareTag("COLD"))
        {
            PLAYER.SET_stay_COLD();
        }

        if (other.gameObject.CompareTag("WATER"))
        {
            Debug.Log("水の位置" + other.transform.position.y);
            Collider = other.gameObject.GetComponent<BoxCollider>();
            PLAYER.WATER(other.transform.position.y);
        }

        if (other.gameObject.CompareTag("HARD_HOT"))
        {
            PLAYER.HARDHOT(2);
            Forced_Phase = other.gameObject.GetComponent<Forced_Phase>();
        }

        if (other.gameObject.CompareTag("HARD_COLD"))
        {
            PLAYER.HARDCOLD(2);
            Forced_Phase = other.gameObject.GetComponent<Forced_Phase>();
        }

        if (other.gameObject.CompareTag("SPONGE"))
        {
            PLAYER.SPONGE();
        }

        if (other.gameObject.CompareTag("THORN"))
        {
            Thorn_Block = other.gameObject.GetComponent<Thorn_Block>();
            PLAYER.THORN(Thorn_Block.GETpop(), other.gameObject.transform.position.y);
        }

        if (other.gameObject.CompareTag("FIRE"))
        {
            PLAYER.FIRE();
        }

        if (other.gameObject.CompareTag("DRAIN"))
        {
            PLAYER.DRAIN();
        }
    }

    void OnTriggerExit(Collider other)              //他のオブジェクトとの接触時の処理
    {
        PLAYER.CLEAR_STAND();

        //Debug.Log("はなれた" + other.name);
        if (other.gameObject.CompareTag("GAP"))
        {
            PLAYER.CLEAR_stay_GAP_U();
        }

        if (other.gameObject.CompareTag("HOT"))
        {
            PLAYER.CLEAR_stay_HOT();
        }

        if (other.gameObject.CompareTag("COLD"))
        {
            PLAYER.CLEAR_stay_COLD();
        }

        if (other.gameObject.CompareTag("WATER"))
        {
            PLAYER.CLEAR_stay_WATER();
            Collider.isTrigger = true;
            /*
            int flag = PLAYER.GETFLOATflag();
            if (flag ==  1)
            {
                Debug.Log("着地");
                Collider collider = other.gameObject.GetComponent<BoxCollider>();
                collider.isTrigger = false;

            }
            */
        }
    }

    void OnCollisionEnter(Collision other)
    {
        /*
        if (other.gameObject.CompareTag("THORN_BLOCK"))
        {
            Thorn_Block = other.gameObject.GetComponent<Thorn_Block>();
            PLAYER.THORN(Thorn_Block.GETpop(), other.gameObject.transform.position.y);
        }
        */
    }
}