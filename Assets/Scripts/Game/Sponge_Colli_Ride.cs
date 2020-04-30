using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge_Colli_Ride : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("SOLID"))
        {
            //  親の潰れる判定を有効にする
            transform.parent.gameObject.GetComponent<Sponge>().Is_Crush = true;
        }
        //  雲はすり抜ける
        if (col.gameObject.CompareTag("CLOUD"))
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void OnCollisionExit(Collision col)
    {
        if(col.gameObject.CompareTag("SOLID"))
        {
            transform.parent.gameObject.GetComponent<Sponge>().Is_Crush = false;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //  すり抜けを解除する
        if (col.gameObject.CompareTag("CLOUD"))
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }



}
