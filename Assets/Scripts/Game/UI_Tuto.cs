using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tuto : MonoBehaviour
{
    // Start is called before the first frame update
    float m_sin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_sin += 0.1f;
        this.gameObject.transform.Translate(0.0f, Mathf.Sin(m_sin) * 0.01f, 0.0f);
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
