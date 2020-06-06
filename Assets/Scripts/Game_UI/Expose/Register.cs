using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-100)]
public class Register : MonoBehaviour
{

    [SerializeField] UnityEvent OnAwake;

    // Start is called before the first frame update
    void Start()
    {
        OnAwake.Invoke();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
