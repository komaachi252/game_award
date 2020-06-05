using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Camera : MonoBehaviour
{

    [SerializeField] ExposeCamera excamera;

    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = this.GetComponent<Canvas>();

        canvas.worldCamera = excamera.maincamera;//カメラをもらう
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
