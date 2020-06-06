using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_Colli_Ride : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_controller;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!col.gameObject.GetComponent<BoxCollider>().isTrigger)
        {
            if (col.gameObject.CompareTag("SOLID"))
            {
                FindObjectOfType<Audio_Manager>().Play("leaf");
                Debug.Log("OnTriggerEnter_Ride_Ice");
                m_controller.GetComponent<Leaf_Controller>().Set_Colli_Type(Leaf.Colli_Type.ICE);
            }
            if (col.gameObject.CompareTag("AQUA"))
            {
                FindObjectOfType<Audio_Manager>().Play("leaf");
                Debug.Log("OnTriggerEnter_Ride_Aqua");
                m_controller.GetComponent<Leaf_Controller>().Set_Colli_Type(Leaf.Colli_Type.AQUA);
            }
        }
    }
}
