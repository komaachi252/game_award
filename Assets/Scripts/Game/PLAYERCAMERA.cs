using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERCAMERA : MonoBehaviour
{
    PLAYER PLAYER;
    GameObject player;
    Vector3 pos;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 3;
        /*
        pos.y += 2;
        pos.z = -5;
        transform.position = pos;
        */

        //player = GameObject.Find("PLAYER_MASTER(Clone)");
        //PLAYER = player.GetComponent<PLAYER>();
    }

    // Update is called once per frame
    void Update()
    {
        if (count > 0)
        {
            count--;
            if (count == 0)
            {
                player = GameObject.Find("PLAYER_MASTER");
                PLAYER = player.GetComponent<PLAYER>();
            }
        }

        if (count == 0)
        {
            pos = PLAYER.GETPLAYERPOS();
            pos.y = pos.y + 2;
            pos.z = -5;
            transform.position = pos;
        }
    }

    public void SETPOS(Vector3 pos_p)
    {
        pos.x = pos_p.x;
        pos.y = pos_p.y + 2;
        pos.z = -5;
    }
}
