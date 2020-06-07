using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_Colli_Under : MonoBehaviour
{
    // Start is called before the first frame update
    bool m_is_colli;
    public GameObject m_controller;
    public bool Is_Colli
    {
        set { m_is_colli = value; }
        get { return m_is_colli; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("CLOUD") && !col.GetComponent<BoxCollider>().isTrigger)
        {
            FindObjectOfType<Audio_Manager>().Play("leaf");
            Debug.Log("OnTriggerEnter_Under");
            m_controller.gameObject.GetComponent<Leaf_Controller>().Set_Colli_Type(Leaf.Colli_Type.CLOUD);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("CLOUD") && !col.GetComponent<BoxCollider>().isTrigger)
        {
            Debug.Log("OnTriggerExit_Under");
            m_controller.gameObject.GetComponent<Leaf_Controller>().Return_Angle();
        }
    }

}
