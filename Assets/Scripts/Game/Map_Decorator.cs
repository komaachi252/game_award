using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Decorator : MonoBehaviour
{
    // Start is called before the first frame update
    int m_world_index;
    public GameObject[] m_decos;

    void Start()
    {
        
    }

    public void Decorate_Map(in Map_Data map_data)
    {
        m_world_index = DontDestroyManager.Map_Index / 10;
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
                    pop_cnt++;
                    //seed_num++;
                    if(pop_cnt >= 3)
                    {
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
        if(m_world_index == 0 || m_world_index == 1)
        {
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
        }
        if(m_world_index == 2)
        {
            var seed = seed_num % 3;
            if(seed == 0)
            {
                create_index = 4;
            }
            if(seed == 1)
            {
                create_index = 5;
            }
            if(seed == 2)
            {
                create_index = 6;
            }
        }
        if(m_world_index == 3)
        {
            var seed = seed_num % 4;
            if(seed == 0)
            {
                create_index = 7;
            }
            if(seed == 1)
            {
                create_index = 8;
            }
            if(seed == 2)
            {
                create_index = 9;
            }
            if(seed == 3)
            {
                create_index = 10;
            }
        }
        if(m_world_index == 4)
        {
            var seed = seed_num % 3;
            if(seed == 0)
            {
                create_index = 11;
            }
            if(seed == 1)
            {
                create_index = 12;
            }
            if(seed == 2)
            {
                create_index = 13;
            }
        }
        Instantiate(m_decos[create_index], new Vector3(x, y, -1.0f), Quaternion.identity);
    }
}
