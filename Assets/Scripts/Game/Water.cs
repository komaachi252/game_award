using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オブジェクトに紐付いている関数
public class Water : MonoBehaviour
{
    //　DistortShaderを設定したマテリアルを設定
    [SerializeField]
    private Material material;

    float Ripple_bUse = 0;
    float Ripple_On = 0;
    float Ripple_Speed = 100.0f;//波紋の速さ
    float Water_Alpha = 0.4f;//波紋の濃さ

    //カウントアップ
    private float countup = 0.0f;

    //タイムリミット
    public float timeLimit = 1.0f,timecheck = 0.1f;

    Vector3 pos;

    bool m_is_set = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (this.GetComponent<Map_Data_Seeker>().Is_Set &&! m_is_set)
        {
            Seek_Map();
            m_is_set = true;
        }

    }

    // 更新用の関数
    void FixedUpdate()
    {
        if (Ripple_bUse == 1 || Ripple_On == 1)
        {
            //時間をカウントする
            countup += Time.deltaTime;
        }
        if (Ripple_bUse == 0 || Ripple_On == 1)
        {
            //現在の位置を取得
            pos = GameObject.Find("PLAYER_MASTER").transform.position;
        }
        if(countup >= timecheck)
        {
            Ripple_Speed--;
            Water_Alpha += 0.0015f;
            timecheck += 0.1f;
        }

        if (countup >= timeLimit)
        {
            countup = 0;
            Ripple_bUse = 0;
            Ripple_On = 0;
            timecheck = 0.1f;
            Water_Alpha = 0.35f;
        }

        //シェーダーのプロパティ値を決定
        material.SetFloat("_Ripple_bUse", Ripple_bUse);
        material.SetFloat("_WorldposY", pos.y);
        material.SetFloat("_WorldposZ", pos.z);
        material.SetFloat("_WorldposX", pos.x);
        material.SetFloat("_Ripple_Speed", Ripple_Speed);
        material.SetFloat("_Ripple_On", Ripple_On);
        material.SetFloat("_Water_Alpha", Water_Alpha);
    }

    // オブジェクトが重なったとき
    void OnTriggerEnter(Collider other)
    {
        Ripple_bUse = 1;
        Debug.Log("Trigger Enter: " + other.gameObject.name);
    }

    // オブジェクトが重なっている間
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Ripple_On = 1;
            countup = 0;
            timecheck = 0.1f;
            Water_Alpha = 0.4f;
        }
        Debug.Log("Trigger Stay: " + other.gameObject.name);
    }

    void Set_Scale(Map_Data map_data, int index_i, int index_j)
    {
        //幅高さを取得
        int width = 0;
        int height = 0;

        for(int j = index_j; j < map_data.Width; j++)
        {
            if(map_data.Map_data[index_i, j].Equals(7))
            {
                width++;
                continue;
            }
            break;
        }
        for(int i = index_i; i < map_data.Height; i++)
        {
            if(map_data.Map_data[i, index_j].Equals(7))
            {
                height++;
                continue;
            }
            break;
        }
        this.transform.localScale = new Vector3(width, height, 0.95f);
        this.transform.Translate(width / 2.0f - 0.5f, -height / 2.0f + 0.3f, 0);

    }

    void Seek_Map()
    {
        var map_data = GameObject.Find("MapLoader").GetComponent<MapLoader>().m_map_data;

        var index_i = this.gameObject.GetComponent<Map_Data_Seeker>().Index_i;
        var index_j = this.gameObject.GetComponent<Map_Data_Seeker>().Index_j;

        //  自分が左上端か探査
        if(index_j > 0 && index_j > 0 && !map_data.Map_data[index_i - 1, index_j].Equals(7) && !map_data.Map_data[index_i, index_j - 1].Equals(7))
        {
            Set_Scale(map_data, index_i, index_j);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}