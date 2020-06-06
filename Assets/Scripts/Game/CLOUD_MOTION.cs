using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLOUD_MOTION : MonoBehaviour
{
    public PLAYER PLAYER;
    int MOVE_D = 1;
    Vector3 SPIN;
    //float TARGET_SPIN = 90.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
}
