using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public bool m_is_down;
    void Start()
    {

        if (m_is_down)
            this.transform.localScale = new Vector3(1, -1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
