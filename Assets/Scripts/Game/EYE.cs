using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EYE : MonoBehaviour
{
    Image image;
    int FLAG = -1;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FLAG == 1)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FLAG *= -1;
        }
    }

    public void CHANGE_FLAG()
    {
        FLAG *= -1;
    }
}
