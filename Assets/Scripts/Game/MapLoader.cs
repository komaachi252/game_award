using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapLoader : MonoBehaviour
{
    public List<string> m_file_paths;  //  マップファイルパス
    
    List<Map_Data> m_map_datas;  //  マップリスト

    public GameObject[] m_objects;  //  読み込むオブジェクト番号に対応

    public int m_map_index;  //  読み込むマップのインデックス

    private const string FILE_PATH = "/Resources/Game/";

    public Map_Data m_map_data;

    private bool m_is_create = false;  //  マップ生成判定
    public bool Is_Create
    {
        get { return m_is_create; }
    }


    void Start()
    {
        m_map_datas = new List<Map_Data>();

        //  全てのマップファイルのパスを追加する
        m_file_paths.Add(FILE_PATH + "Map1.csv");
        m_file_paths.Add(FILE_PATH + "Map2.csv");
        m_file_paths.Add(FILE_PATH + "test1.csv");
        m_file_paths.Add(FILE_PATH + "test2.csv");
        m_file_paths.Add(FILE_PATH + "test3.csv");
        m_file_paths.Add(FILE_PATH + "test4.csv");

        //  追加されたパス分マップ情報を読み込む
        foreach (var path in m_file_paths){
            Map_Data data = new Map_Data();
            Map_Load(path, ref data);
            m_map_datas.Add(data);
        }

        //  指定したインデックスのマップを生成する
        Map_Create(m_map_datas[m_map_index]);
    }
    void Update()
    {
        
        
    }

    void Map_Load(string file_path, ref Map_Data map_data)
    {
        StreamReader sr = new StreamReader(Application.dataPath + file_path);
        string str_stream = sr.ReadToEnd();
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;

        string[] lines = str_stream.Split(new char[] { '\r', '\n' }, option);
        char[] spliter = new char[1] { ',' };

        map_data.Height = lines.Length;
        map_data.Width = lines[0].Split(spliter, option).Length;
        map_data.Map_data = new int[map_data.Height, map_data.Width];

        for(int i = 0; i < map_data.Height; i++){
            for(int j = 0; j < map_data.Width; j++){
                string[] read_str_data = lines[i].Split(spliter, option);

                map_data.Map_data[i, j] = int.Parse(read_str_data[j]);

                //  デバッグ表示用
                //Debug.Log(map_data.Map_data[i, j]);
            }
        }
    }

    bool Map_Create(in Map_Data map_data)
    {
        m_map_data = map_data;

        //  座標初期値
        var x = 0.5f;
        var y = (map_data.Height - 1) * 1.0f;

        for(int i = 0; i < map_data.Height; i++){
            for(int j = 0; j < map_data.Width; j++){
                if (map_data.Map_data[i, j] == 0) continue;
                var obj = Instantiate(m_objects[map_data.Map_data[i,j]], new Vector3(x + j, y - i, 0.0f), Quaternion.identity);
                if(map_data.Map_data[i, j].Equals(25))
                {
                    obj.gameObject.GetComponent<Lift_Block>().Set_Map_Data_Index(i, j);
                }
            }
        }

        m_is_create = true;

        return true;
    }
}
