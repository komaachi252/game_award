﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UPARROW : MonoBehaviour
{
    Image image;
    public PLAYERCAMERA PLAYERCAMERA;
    int count = 0;
    int FLAG = -1;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FLAG == 1)
        {
            count++;
            if (count < 61)
            {
                image.enabled = true;
                if(PLAYERCAMERA.check_V() == 1)
                {
                    image.enabled = false;
                }
            }
            else if(count < 121)
            {
                image.enabled = false;
            }
            
            if(count == 121)
            {
                count = 0;
            }
        }
        else
        {
            image.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            FLAG *= -1;
            count = 0;
        }
    }
}