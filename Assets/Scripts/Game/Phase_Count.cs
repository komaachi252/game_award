using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_Count : MonoBehaviour
{
    // Start is called before the first frame update
    int m_phase_cnt = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Phase_Count(int cnt)
    {
        m_phase_cnt = cnt;
        this.gameObject.GetComponent<Text>().text = m_phase_cnt.ToString();
    }
}
