using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Data_Seeker : MonoBehaviour
{
    // Start is called before the first frame update
    private int m_index_i = 0;
    public int Index_i
    {
        get { return m_index_i; }
    }
    private int m_index_j = 0;
    public int Index_j
    {
        get { return m_index_j; }
    }

    private bool m_is_set = false;

    public bool Is_Set
    {
        get { return m_is_set; }
    }
    void Start()
    {
        
    }
    
    public void Set_Index(int i, int j)
    {
        m_index_i = i;
        m_index_j = j;
        m_is_set = true;
    }
}
