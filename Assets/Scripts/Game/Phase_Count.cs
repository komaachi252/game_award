using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase_Count : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_Phase_Count(int cnt)
    {
        this.gameObject.GetComponent<Text>().text = cnt.ToString();
    }
}
