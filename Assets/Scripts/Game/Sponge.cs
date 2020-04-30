using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sponge : MonoBehaviour
{
    // Start is called before the first frame update
    private const float SUB_VALUE = 0.015f;

    private bool m_is_crush = false;  //  潰れる判定
    public bool Is_Crush
    {
        set { m_is_crush = value; }
        get { return m_is_crush; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Crush();

    }

    void Crush()
    {
        if (!m_is_crush) return;
        //  スケール縮小
        var scale = this.transform.localScale;
        scale.y -= SUB_VALUE;

        //  その分下げる
        var pos = this.transform.position;
        pos.y -= SUB_VALUE * 0.5f;
        //  0.0f以下なら消す
        if(scale.y <= 0.0f)
        {
            Destroy(this.gameObject);
        }
        this.transform.localScale = scale;
        this.transform.position = pos;

    }

    private void OnCollisionEnter(Collision col)
    {
        //  雲はすり抜ける
        if(col.gameObject.CompareTag("CLOUD"))
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //  すり抜けを解除する
        if (col.gameObject.CompareTag("CLOUD"))
        {
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
