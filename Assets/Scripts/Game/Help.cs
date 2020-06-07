using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            GameObject.Find("Helpme").GetComponent<Helpme>().Help();
            Destroy(this.gameObject);
        }
    }
}
