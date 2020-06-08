using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermillTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m_effect;
    bool m_is_colli;
    public bool Is_Colli
    {
        get { return m_is_colli; }
    }

    void Start()
    {
        this.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            m_is_colli = true;
            Instantiate(m_effect, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            m_is_colli = false;
        }
    }
}
