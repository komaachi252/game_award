using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIT_U : MonoBehaviour
{
    public PLAYER PLAYER;
    Collider Collider;
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
            Debug.Log("水の位置"+other.transform.position.y);
            Collider = other.gameObject.GetComponent<BoxCollider>();
            PLAYER.WATER(other.transform.position.y);
        }

        if (other.gameObject.CompareTag("HARD_HOT"))
        {
            PLAYER.HARDHOT();
        }

        if (other.gameObject.CompareTag("HARD_COLD"))
        {
            PLAYER.HARDCOLD();
        }

        if (other.gameObject.CompareTag("SPONGE"))
        {
            PLAYER.SPONGE();
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
}
