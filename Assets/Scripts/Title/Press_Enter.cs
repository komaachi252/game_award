using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Press_Enter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("WorldScene");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, (Mathf.Sin(Time.time) + 1) / 2);
    }
}
