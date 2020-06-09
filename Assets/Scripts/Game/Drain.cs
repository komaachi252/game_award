using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drain : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_splash;
    void Awake()
    {
        this.transform.Translate(0, -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("AQUA") && !col.gameObject.GetComponent<BoxCollider>().isTrigger)
        {
            FindObjectOfType<Audio_Manager>().Play("drain");
            var pos = this.gameObject.transform.position;
            pos.y += 1.0f;
            Instantiate(m_splash, pos, Quaternion.identity);
        }
    }
}
