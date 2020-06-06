using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AQUA_MOTION : MonoBehaviour
{
    public PLAYER PLAYER;
    int MOVE_D = 1;
    Vector3 SPIN;

    int MODE = 0;
    int TIME_COUNT = 0;
    Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MODE == 0)
        {
            MOVE_D = PLAYER.GET_MOVE_D();

            if (MOVE_D == 1)
            {
                if (transform.rotation.y != 90)
                {
                    transform.Rotate(0.0f, -3.0f, 0.0f);

                    if (transform.localEulerAngles.y < 90)
                    {
                        transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                    }
                }
            }

            if (MOVE_D == -1)
            {
                if (transform.rotation.y != 270)
                {
                    transform.Rotate(0.0f, 3.0f, 0.0f);

                    if (transform.localEulerAngles.y > 270)
                    {
                        transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                    }
                }
            }
        }

        if (MODE==1)
        {
            if (TIME_COUNT < 10)
            {
                transform.Rotate(new Vector3(0.0f, 6.0f, 0.0f));
            }
            if(TIME_COUNT >= 10)
            {
                transform.Rotate(new Vector3(0.0f, 18.0f, 0.0f));
            }

            if(TIME_COUNT > 10 && TIME_COUNT <= 40)
            {
                size.x -= 0.3f;
                size.z -= 0.3f;
                transform.localScale = size;
            }

            if(TIME_COUNT >30)
            {
                transform.Translate(0.0f, -0.05f, 0.0f);
            }

            TIME_COUNT++;
        }
    }

    public void MOTION_RYU()
    {
        MODE = 1;
        size = new Vector3(10.0f, 10.0f, 10.0f);
    }
}
