using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Decorator : MonoBehaviour
{
    // Start is called before the first frame update
    int m_world_index;
    public GameObject[] m_decos;

    int[] m_pop_list = { 3, 4, 4, 3, 2, 5, 4, 6 };

    int m_item = 0;

    int ITEM_MAX;

    int m_sprseed = 0;
    void Awake()
    {
        m_world_index = DontDestroyManager.Map_Index / 10;
        ITEM_MAX = m_pop_list.Length;
    }

    public void Decorate_Map(in Map_Data map_data)
    {
        //m_world_index = DontDestroyManager.Map_Index / 10;
        var x = 0.5f;
        var y = (map_data.Height - 1) * 1.0f;

        int pop_cnt = 0;
        int seed_num = 0;
        for (int i = 0; i < map_data.Height; i++)
        {
            for (int j = 0; j < map_data.Width; j++)
            {
                if (map_data.Map_data[i, j] == 0) continue;

                if (map_data.Map_data[i, j] == 1)
                {
                    if (m_world_index == 2 && j == 0) continue;
                    if (m_world_index == 2 && j == map_data.Width - 1) continue;
                    pop_cnt++;
                    //seed_num++;
                    if(pop_cnt >= m_pop_list[m_item])
                    {
                        m_item++;
                        if (m_item >= ITEM_MAX) m_item = 0;
                        seed_num++;
                        pop_cnt = 0;
                        Create_Deco(x + j, y - i, seed_num);
                    }
                    continue;
                }
            }
        }
    }

    void Create_Deco(float x, float y, int seed_num)
    {
        int create_index = 0;
        var q = Quaternion.identity;
        var scale = new Vector3(1.0f, 1.0f, 1.0f);
        var trans = new Vector3(x, y, 0.0f);
        if (m_world_index == 0 || m_world_index == 1)
        {
            q = Quaternion.Euler(-20.0f, 0.0f, 0.0f);
            var seed = seed_num % 4;
            if(seed == 0)
            {
                create_index = 0;
            }
            if(seed == 1)
            {
                create_index = 1;
            }
            if(seed == 2)
            {
                create_index = 2;
            }
            if(seed == 3)
            {
                create_index = 3;
            }
            trans.z = -1.0f;
        }
        if(m_world_index == 2)
        {
            var seed = seed_num % 3;
            if(seed == 0)
            {
                create_index = 4;
                q = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }
            if (seed == 1)
            {
                create_index = 5;
            }
            if(seed == 2)
            {
                create_index = 6;
                q = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }

            trans.y += 1.18f;
            trans.z += 0.3f;
            scale.x = 0.7f;
            scale.y = 0.7f;
            scale.z = 0.7f;
        }
        if(m_world_index == 3)
        {
            var seed = seed_num % 4;
            if(seed == 0)
            {
                create_index = 7;

                scale.x = 50.0f;
                scale.y = 50.0f;
                scale.z = 50.0f;

                trans.z = -0.5f;
            }
            if(seed == 1)
            {
                create_index = 8;

                scale.x = 4.0f;
                scale.y = 4.0f;
                scale.z = 4.0f;

                trans.z = 0.3f;
                trans.y += 0.7f;
                if (m_sprseed % 2 == 0) {
                    q = Quaternion.Euler(0.0f, 0.0f, 60.0f);
                }
                else
                {
                    q = Quaternion.Euler(0.0f, 0.0f, -60.0f);
                }
                m_sprseed++;
            }
            if(seed == 2)
            {
                create_index = 9;

                scale.x = 70.0f;
                scale.y = 70.0f;
                scale.z = 70.0f;

                trans.z = 0.36f;
                trans.y += 0.5f;
                q = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
            }
            if(seed == 3)
            {
                create_index = 10;

                scale.x = 70.0f;
                scale.y = 70.0f;
                scale.z = 70.0f;

                trans.z = 0.36f;
                trans.y += 0.7f;
            }
        }
        if(m_world_index == 4)
        {
            var seed = seed_num % 3;
            if(seed == 0)
            {
                create_index = 11;
                scale.x = 50.0f;
                scale.y = 50.0f;
                scale.z = 50.0f;

                trans.z = 0.36f;
                trans.y += 0.6f;

            }
            if (seed == 1)
            {
                create_index = 12;
                scale.x = 15.0f;
                scale.y = 15.0f;
                scale.z = 15.0f;

                trans.z = 0.36f;
                trans.y += 0.6f;

            }
            if (seed == 2)
            {
                create_index = 13;
                scale.x = 20.0f;
                scale.y = 20.0f;
                scale.z = 20.0f;

                trans.z = 0.36f;
                trans.y += 0.6f;

            }
        }
        
        var obj = Instantiate(m_decos[create_index], trans, q);
        obj.transform.localScale = scale;
    }
}
