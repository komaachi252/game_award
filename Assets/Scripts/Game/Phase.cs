using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_effect;

    public bool m_is_hot;
    //public GameObject instance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create_Effect()
    {
        var pos = this.transform.position;
        if (m_is_hot)
        {
            pos.y += 0.3f;
        }
        else
        {
            pos.y -= 0.5f;
        }
        pos.z -= 0.5f;
        Instantiate(m_effect, pos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            Create_Effect();
        }
    }
}
