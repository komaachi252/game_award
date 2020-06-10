using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf_Controller : MonoBehaviour
{
    // Start is called before the first frame update

    public const int ICE_ROTATE = 64;  //  ICEの時の回転角度
    public const int AQUA_ROTATE = 40;  //  AQUAの時の回転角度
    public const int CLOUD_ROTATE = -40;  //  CLOUDの時の回転角度
    public const int INV_ICE_ROTATE = -ICE_ROTATE; //  戻す回転
    public const int INV_AQUA_ROTATE = -AQUA_ROTATE;
    public const int INV_CLOUD_ROTATE = -CLOUD_ROTATE;


    public float m_move_speed = 0.4f;  //  回転速度
    public float m_return_speed = -0.3f; //  戻る回転速度
    public float m_target_rotate;  //  目標角度
    public bool m_is_rotate = false;  //  回転判定
    public bool m_is_right;  //  向き
    void Start()
    {
        //this.gameObject.GetComponent<Transform>().Rotate(0, 90, -30);
        if (m_is_right)
        {
            this.gameObject.GetComponent<Transform>().transform.Translate(1.7f, -0.55f, 0);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            this.gameObject.GetComponent<Transform>().transform.Translate(-1.7f, -0.55f, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //  回転状態の判定
        if (m_is_rotate)
        {
            var target = Quaternion.Euler(new Vector3(0, 0, m_target_rotate));
            var now_rot = transform.rotation;
            //  自角度と目標角度を比較
            if (Quaternion.Angle(now_rot, target) <= 1)
            {
                //  目標角度にする
                transform.rotation = target;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, m_move_speed));
            }
        }
        else
        {
            //  元に戻す回転
            var target = Quaternion.Euler(new Vector3(0, 0, 0));
            var now_rot = transform.rotation;
            if (Quaternion.Angle(now_rot, target) <= 1)
            {
                transform.rotation = target;
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, m_return_speed));

            }
        }
    }

    public void Set_Colli_Type(Leaf.Colli_Type type)
    {
        //  衝突したプレイヤーのタグによって目標角度を代入
        m_is_rotate = true;
        switch (type)
        {
            case Leaf.Colli_Type.ICE:
                if (m_is_right)
                {
                    m_target_rotate = ICE_ROTATE;
                    Move_Change(true);
                }
                else
                {
                    m_target_rotate = INV_ICE_ROTATE;
                    Move_Change(false);
                }
                break;
            case Leaf.Colli_Type.AQUA:
                if (m_is_right)
                {
                    m_target_rotate = AQUA_ROTATE;
                    Move_Change(true);
                }
                else
                {
                    m_target_rotate = INV_AQUA_ROTATE;
                    Move_Change(false);
                }
                break;
            case Leaf.Colli_Type.CLOUD:
                if (m_is_right)
                {
                    m_target_rotate = CLOUD_ROTATE;
                    Move_Change(false);
                }
                else
                {
                    m_target_rotate = INV_CLOUD_ROTATE;
                    Move_Change(true);
                }
                break;
        }
    }

    public void Return_Angle()
    {
        m_is_rotate = false;
    }
    private void Move_Change(bool is_right)
    {
        //  向きによって回転符号変更
        if (is_right)
        {
            if (m_move_speed < 0)
            {
                m_move_speed *= -1.0f;
            }
            if (m_return_speed > 0)
            {
                m_return_speed *= -1.0f;
            }
        }
        else
        {
            if (m_move_speed > 0)
            {
                m_move_speed *= -1.0f;
            }
            if (m_return_speed < 0)
            {
                m_return_speed *= -1.0f;
            }
        }
    }
}

