using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JUGEMOVE : MonoBehaviour
{
    public PLAYER PLAYER;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 POS = transform.position;
        Vector3 muki = new Vector3(0.0f, 1.0f, 0.0f);
        POS.x += 1;
        POS.y += 0.7f;
        */
        //Ray ray=new Ray(POS,muki);
        //Physics.Raycast(POS, HIT, 1.0f);
        /*
        if (Physics.BoxCast(POS, new Vector3(0.45f, 0.1f, 0.45f), muki, out hit, transform.rotation, 0.2f))
        {
            Debug.Log(hit.transform.name);
        }
        */
        /*
        if (Physics.CheckBox(POS, new Vector3(0.45f, 0.05f, 0.45f), transform.rotation))
        {
            Debug.Log("ある");
        }
        else
        {
            Debug.Log("ない");
        }
        */
    }

    public void SETposx(int x)
    {
        Vector3 POS = transform.position;

        POS.x = x+0.5f;
        transform.position = POS;
        
    }

    public void SETposy(int y)
    {
        Vector3 POS = transform.position;

        POS.y = y;
        transform.position = POS;
        
    }

    public int root(int MOVE_D,int MOVE_V)
    {
        Vector3 POS = transform.position;

        if (MOVE_D == 1)
        {
            POS.x += 1.0f;
        }
        else
        {
            POS.x -= 1.0f;
        }

        if (MOVE_V == 1)
        {
            POS.y -= 0.7f;
        }
        else
        {
            POS.y += 0.7f;
        }

        if (Physics.CheckBox(POS, new Vector3(0.45f, 0.05f, 0.45f), transform.rotation))
        {
           // Debug.Log("ある");
            return 0;
        }

        Debug.Log("ない"+ transform.position + "POS" + POS);

        PLAYER.SET_V_MOVEPOS(POS.x);
        return 1;       //移動予定地が空白の場合縦移動予約フラグをセット
    }

    public float GET_JUGEPOS()
    {
        return transform.position.x;
    }

    /*void OnDrawGizmos()
    {
        Vector3 POS = transform.position;
        POS.x += 1;
        POS.y += 0.5f;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(POS, new Vector3(0.9f,0.2f,0.9f));
    }
    */
}
