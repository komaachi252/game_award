using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forced_Phase : MonoBehaviour
{
    public GameObject m_effect;

    int m_spawn_frame = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_spawn_frame = Random.Range(0, 120);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_spawn_frame--;
        if (m_spawn_frame > 0) return;
        
        var pos = transform.position;
        pos.y += 0.3f;
        pos.z -= 0.5f;

        Instantiate(m_effect, pos, Quaternion.identity);
        m_spawn_frame = Random.Range(30, 120);
    
    }
}
