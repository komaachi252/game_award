using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLOUD_MOTION : MonoBehaviour
{
    public PLAYER PLAYER;
    int MOVE_D = 1;
    Vector3 SPIN;
    int CRUSH_F = 0;

    int MODE = 0;
    int TIME_COUNT = 0;
    Vector3 size;
    //float TARGET_SPIN = 90.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CRUSH_F == 0)
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

        if (CRUSH_F == 1)
        {
            transform.Rotate(13.0f, 8.0f, 8.0f);
            transform.Translate(0.0f, -0.01f, -0.02f,Space.World);
        }

        if (MODE == 1)
        {
            if (TIME_COUNT < 10)
            {
                transform.Rotate(new Vector3(0.0f, 6.0f, 0.0f));
            }
            if (TIME_COUNT >= 10)
            {
                transform.Rotate(new Vector3(0.0f, 18.0f, 0.0f));
            }

            if (TIME_COUNT > 10 && TIME_COUNT <= 40)
            {
                size.x -= 0.3f;
                size.z -= 0.3f;
                transform.localScale = size;
            }

            if (TIME_COUNT > 30 && TIME_COUNT<51)
            {
                transform.Translate(0.0f, 0.05f, 0.0f);
            }

            TIME_COUNT++;
        }
    }

    public void CRUSH()
    {
        CRUSH_F = 1;
    }

    public void MOTION_RYU()
    {
        MODE = 1;
        size = new Vector3(10.0f, 10.0f, 10.0f);
    }
}
