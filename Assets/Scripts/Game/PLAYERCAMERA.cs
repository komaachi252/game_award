using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYERCAMERA : MonoBehaviour
{
    public MapLoader MapLoader;
    public LEFTARROW LEFTARROW;
    public RIGHTARROW RIGHTARROW;
    public UPARROW UPARROW;
    public DOWNARROW DOWNARROW;
    public EYE EYE;
    public NoiseController NoiseController;
    PLAYER PLAYER;
    GameObject player;
    Vector3 pos;
    int count;
    int MODE_L = -1;
    int GOAL = 0;
    int BACKcount = 0;
    float BACKMOVE_x = 0;
    float BACKMOVE_y = 0;
    public int MILLFIND = 0;

    public float offset_x = 0;
    public float offset_y = 0;
    public float offset_z = -5;
    public int offset_x_MAX = 0;

    // Start is called before the first frame update
    void Start()
    {
        count = 3;
    }

    // Update is called once per frame
    void Update()
    {
        float x_axis_R = Input.GetAxis("Rstick_H"); //右マイナス　左プラス
        float y_axis_R = Input.GetAxis("Rstick_V"); //上プラス　下マイナス

        if (count > 0)
        {
            count--;
            if (count == 1)
            {
                player = GameObject.Find("PLAYER_MASTER");
                PLAYER = player.GetComponent<PLAYER>();
            }
        }

        //見渡し状態でなくRスティックがいじられたら
        if ((x_axis_R != 0 || y_axis_R != 0) && MODE_L == -1 && BACKcount == 0 && count == 0 && GOAL == 0)
        {
            MODE_L = 1;

            LEFTARROW.CHANGE_FLAG();
            RIGHTARROW.CHANGE_FLAG();
            UPARROW.CHANGE_FLAG();
            DOWNARROW.CHANGE_FLAG();
            EYE.CHANGE_FLAG();
            NoiseController.CHANGE_CANVAS();
        }

        //見渡し中にアクションボタンが押されたら
        if (Input.GetKeyDown("joystick button 0") && MODE_L == 1 && BACKcount == 0)
        {
            MODE_L = -1;
            BACKcount = 30;
            Vector3 TARGETPOS = PLAYER.GETPLAYERPOS();
            TARGETPOS.y += 1.1f + offset_y;

            if (TARGETPOS.x > MapLoader.Get_Map_Width() - 4.6f)
            {
                TARGETPOS.x = MapLoader.Get_Map_Width() - 4.6f;
            }

            if (TARGETPOS.x < 4.6f)
            {
                TARGETPOS.x = 4.6f;
            }

            if (TARGETPOS.y > MapLoader.Get_Map_Height() - 3.1f)
            {
                TARGETPOS.y = MapLoader.Get_Map_Height() - 3.1f;
            }

            if (TARGETPOS.y < 2.1f)
            {
                TARGETPOS.y = 2.1f;
            }

            BACKMOVE_x = (TARGETPOS.x - pos.x) / 30;
            BACKMOVE_y = (TARGETPOS.y - pos.y) / 30;

            LEFTARROW.CHANGE_FLAG();
            RIGHTARROW.CHANGE_FLAG();
            UPARROW.CHANGE_FLAG();
            DOWNARROW.CHANGE_FLAG();
            EYE.CHANGE_FLAG();
            NoiseController.CHANGE_CANVAS();
        }

        if (Input.GetKeyDown(KeyCode.F) && BACKcount == 0 && count == 0 && GOAL == 0)
        {
            MODE_L *= -1;

            if (MODE_L == -1)
            {
                BACKcount = 30;
                Vector3 TARGETPOS = PLAYER.GETPLAYERPOS();
                TARGETPOS.y += 1.1f + offset_y;

                if (TARGETPOS.x > MapLoader.Get_Map_Width() - 4.6f)
                {
                    TARGETPOS.x = MapLoader.Get_Map_Width() - 4.6f;
                }

                if (TARGETPOS.x < 4.6f)
                {
                    TARGETPOS.x = 4.6f;
                }

                if (TARGETPOS.y > MapLoader.Get_Map_Height() - 3.1f)
                {
                    TARGETPOS.y = MapLoader.Get_Map_Height() - 3.1f;
                }

                if (TARGETPOS.y < 2.1f)
                {
                    TARGETPOS.y = 2.1f;
                }

                BACKMOVE_x = (TARGETPOS.x - pos.x) / 30;
                BACKMOVE_y = (TARGETPOS.y - pos.y) / 30;
            }

            NoiseController.CHANGE_CANVAS();
        }


        if (count == 0 && MODE_L == -1 && BACKcount == 0)
        {
            pos = PLAYER.GETPLAYERPOS();
            pos.x += offset_x;
            pos.y = pos.y + 1.1f + offset_y;
            pos.z = offset_z;

            if (pos.x > MapLoader.Get_Map_Width() - 4.6f)
            {
                pos.x = MapLoader.Get_Map_Width() - 4.6f;
            }

            if (pos.x < 4.6f)
            {
                pos.x = 4.6f;
            }

            if (pos.y > MapLoader.Get_Map_Height() - 3.1f)
            {
                pos.y = MapLoader.Get_Map_Height() - 3.1f;
            }

            if (offset_x_MAX == 1)
            {
                pos.x = MapLoader.Get_Map_Width() / 2;
            }
            transform.position = pos;
        }

        if (BACKcount > 0)
        {
            BACKcount--;
            pos.x += BACKMOVE_x;
            pos.y += BACKMOVE_y;

            transform.position = pos;
        }

        if (MILLFIND == 1)
        {
            pos = PLAYER.GETPLAYERPOS();

            if (pos.x > MapLoader.Get_Map_Width() / 2)
            {
                if (pos.x + offset_x > MapLoader.Get_Map_Width() / 2 && offset_x_MAX == 0)
                {
                    offset_x -= 0.03f;
                }
                else if (pos.x + offset_x < MapLoader.Get_Map_Width() / 2 || offset_x_MAX == 1)
                {
                    offset_x_MAX = 1;
                    offset_x = MapLoader.Get_Map_Width() / 2 - pos.x;
                }
            }

            if (pos.x < MapLoader.Get_Map_Width() / 2)
            {
                if ((pos.x + offset_x < MapLoader.Get_Map_Width() / 2) && offset_x_MAX == 0)
                {
                    if (MapLoader.Get_Map_Width() < 20)
                    {
                        offset_x += 0.03f;
                    }
                    else
                    {
                        offset_x += 0.06f;
                    }
                }

                if ((pos.x + offset_x > MapLoader.Get_Map_Width() / 2) || offset_x_MAX == 1)
                {
                    offset_x_MAX = 1;
                    offset_x = (MapLoader.Get_Map_Width() / 2 - pos.x);

                }
            }

            if (pos.y > MapLoader.Get_Map_Height() / 2 - 1.1f)
            {
                if (pos.y + offset_y > MapLoader.Get_Map_Height() / 2 - 1.1f)
                {
                    offset_y -= 0.02f;
                }
            }
            else if (pos.y < MapLoader.Get_Map_Height() / 2 - 1.1f)
            {
                if (pos.y + offset_y < MapLoader.Get_Map_Height() / 2 - 1.1f)
                {
                    offset_y += 0.02f;
                }
            }

            if (MapLoader.Get_Map_Width() < 20)
            {
                offset_z *= 1.002f;
            }
            else
            {
                offset_z *= 1.004f;
            }

            if (offset_z < MapLoader.Get_Map_Width() / 2 * Mathf.Tan(30f * Mathf.Deg2Rad) * -1.8f)
            {
                offset_z = MapLoader.Get_Map_Width() / 2 * Mathf.Tan(30f * Mathf.Deg2Rad) * -1.8f;
            }

        }
        else
        {
            if (offset_x != 0)
            {
                offset_x *= 0.98f;
                //Debug.Break();
                if (Mathf.Abs(offset_x) < 0.01f)
                {
                    offset_x = 0;
                }
            }

            if (offset_y != 0)
            {
                offset_y *= 0.98f;
                if (Mathf.Abs(offset_y) < 0.01f)
                {
                    offset_y = 0;
                }
            }

            if (offset_z != -5)
            {
                offset_z *= 0.995f;
                if (offset_z > -5)
                {
                    offset_z = -5;
                }
            }
        }

        if (count == 0 && MODE_L == 1)
        {
            pos = transform.position;

            if (Input.GetKey(KeyCode.A) || x_axis_R > 0)
            {
                pos.x -= 0.1f;
            }

            if (Input.GetKey(KeyCode.D) || x_axis_R < 0)
            {
                pos.x += 0.1f;
            }

            if (Input.GetKey(KeyCode.W) || y_axis_R > 0)
            {
                pos.y += 0.1f;
            }

            if (Input.GetKey(KeyCode.S) || y_axis_R < 0)
            {
                pos.y -= 0.1f;
            }

            if (pos.x > MapLoader.Get_Map_Width() - 4.6f)
            {
                pos.x = MapLoader.Get_Map_Width() - 4.6f;
            }

            if (pos.x < 4.6f)
            {
                pos.x = 4.6f;
            }

            if (pos.y > MapLoader.Get_Map_Height() - 3.1f)
            {
                pos.y = MapLoader.Get_Map_Height() - 3.1f;
            }

            if (pos.y < 2.1f)
            {
                pos.y = 2.1f;
            }
            transform.position = pos;
        }

        /*
        if (count == 0 && MODE_L == 1)
        {
            pos.x = MapLoader.Get_Map_Width() / 2;
            pos.y = MapLoader.Get_Map_Height() / 2;
            pos.z = pos.x * Mathf.Tan(30f * Mathf.Deg2Rad) * -1.8f;

            transform.position = pos;
        }
        */
    }

    public void SETPOS(Vector3 pos_p)
    {
        pos.x = pos_p.x;
        pos.y = pos_p.y + 2;
        pos.z = -5;
    }

    public int check_V()
    {
        pos = transform.position;
        if (pos.y == (float)(MapLoader.Get_Map_Height() - 3.1f))
        {
            return 1;
        }

        if (pos.y == 2.1f)
        {
            return 2;
        }

        return 0;
    }

    public int check_H()
    {
        pos = transform.position;
        if (pos.x == (float)(MapLoader.Get_Map_Width() - 4.6f))
        {
            return 1;
        }

        if (pos.x == 4.6f)
        {
            return 2;
        }

        return 0;
    }

    public void SETMILLFIND()
    {
        MILLFIND = 1;
    }

    public void CLAREMILLFIND()
    {
        MILLFIND = 0;
        offset_x_MAX = 0;
    }

    public void F_LOCK()
    {
        GOAL = 1;
        EYE.LOCK();
        DOWNARROW.LOCK();
        UPARROW.LOCK();
        RIGHTARROW.LOCK();
        LEFTARROW.LOCK();

        MODE_L = -1;

        BACKcount = 30;
        Vector3 TARGETPOS = PLAYER.GETPLAYERPOS();
        TARGETPOS.y += 1.1f + offset_y;

        if (TARGETPOS.x > MapLoader.Get_Map_Width() - 4.6f)
        {
            TARGETPOS.x = MapLoader.Get_Map_Width() - 4.6f;
        }

        if (TARGETPOS.x < 4.6f)
        {
            TARGETPOS.x = 4.6f;
        }

        if (TARGETPOS.y > MapLoader.Get_Map_Height() - 3.1f)
        {
            TARGETPOS.y = MapLoader.Get_Map_Height() - 3.1f;
        }

        if (TARGETPOS.y < 2.1f)
        {
            TARGETPOS.y = 2.1f;
        }

        BACKMOVE_x = (TARGETPOS.x - pos.x) / 30;
        BACKMOVE_y = (TARGETPOS.y - pos.y) / 30;
    }
}

