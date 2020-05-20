using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    // Start is called before the first frame update
    static int m_map_index;
    public static int Map_Index
    {
        get { return m_map_index; }
        set { m_map_index = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Map_Index = StageController.Get_stage();
    }
}
