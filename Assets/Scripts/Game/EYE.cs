using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EYE : MonoBehaviour
{
    public PLAYERCAMERA PLAYERCAMERA;
    Image image;
    int FLAG = -1;
    int LOCK_F = 0;
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
        /*
        if (Input.GetKeyDown(KeyCode.F) && LOCK_F == 0 && PLAYERCAMERA.GET_VIEW_OK() == 1)
        {
            FLAG *= -1;
        }
        */
    }

    public void CHANGE_FLAG()
    {
        if (LOCK_F == 0)
        {
            FLAG *= -1;
        }
    }

    public void LOCK()
    {
        LOCK_F = 1;
        FLAG = -1;
    }
}

