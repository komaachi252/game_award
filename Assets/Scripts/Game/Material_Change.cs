using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_Change : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] m_materials;
    void Awake()
    {
       this.gameObject.GetComponent<Renderer>().material = m_materials[DontDestroyManager.Map_Index / 10];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
