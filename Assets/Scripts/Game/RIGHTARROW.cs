using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RIGHTARROW : MonoBehaviour
{
    Image image;
    public PLAYERCAMERA PLAYERCAMERA;
    int count = 0;
    int FLAG = -1;
    int LOCK_F = 0;
    float a = 1.0f;
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
            if (PLAYERCAMERA.check_H() == 1)
            {
                image.enabled = false;
            }
            count++;
            if (count < 51)
            {
                a += 0.02f;
            }
            if (count > 101)
            {
                a -= 0.02f;
            }
            if (count > 150)
            {
                count = 0;
            }

            image.color = new Vector4(1.0f, 1.0f, 1.0f, a);

            /*
            count++;
            if (count < 61)
            {
                image.enabled = true;
                if (PLAYERCAMERA.check_H() == 1)
                {
                    image.enabled = false;
                }
            }
            else if (count < 121)
            {
                image.enabled = false;
            }

            if (count == 121)
            {
                count = 0;
            }
            */
        }
        else
        {
            image.enabled = false;
        }
        /*
        if (Input.GetKeyDown(KeyCode.F) && LOCK_F == 0 && PLAYERCAMERA.GET_VIEW_OK() == 1)
        {
            FLAG *= -1;
            count = 0;
        }
        */
    }

    public void CHANGE_FLAG()
    {
        if (LOCK_F == 0)
        {
            FLAG *= -1;
            count = 50;
            a = 1.0f;
        }
    }

    public void LOCK()
    {
        LOCK_F = 1;
        FLAG = -1;
    }
}

