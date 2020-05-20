using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    public GameObject m_controller;
    public enum Colli_Type
    {
        ICE,
        AQUA,
        CLOUD
    };

    void Start()
    {
        
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
    }

    private void OnTriggerExit(Collider col)
    {
        //  プレイヤーがこのCollisionから離れたら元に戻す回転
        Debug.Log("OnTriggerExit_Leaf");
        if (col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA"))
        {
            if (!col.gameObject.GetComponent<BoxCollider>().isTrigger){
                m_controller.GetComponent<Leaf_Controller>().Return_Angle();
            }
        }
    }
}