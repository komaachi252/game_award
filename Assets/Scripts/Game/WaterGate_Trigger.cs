using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGate_Trigger : MonoBehaviour
{
    // Start is called before the first frame update

    bool m_is_colli = false;
    public bool Is_Colli
    {
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
        if(col.gameObject.CompareTag("BLOCK") && col.gameObject.transform != this.gameObject.transform.parent)
        Debug.Log("塩田");
        m_is_colli = true;
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("BLOCK") && col.gameObject.transform != this.gameObject.transform.parent)
        Debug.Log("絵杭");
        m_is_colli = false;
    }


}
