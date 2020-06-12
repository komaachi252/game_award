using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatermillTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject m_effect;
    bool m_is_colli;
    int wave_count = 0;
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
            FindObjectOfType<Audio_Manager>().Play("splash");
            m_is_colli = true;
            Instantiate(m_effect, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            wave_count++;
            if(wave_count==20)
            {
                FindObjectOfType<Audio_Manager>().Play("splash");
                Instantiate(m_effect, this.gameObject.transform.position, Quaternion.identity);
                wave_count = 0;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("AQUA"))
        {
            m_is_colli = false;
            wave_count = 0;
        }
    }
}
