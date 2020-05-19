using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MapLoader : MonoBehaviour
{
    public List<string> m_file_paths;  //  マップファイルパス

    List<Map_Data> m_map_datas;  //  マップリスト

    public GameObject[] m_objects;  //  読み込むオブジェクト番号に対応

    static int m_map_index = 1;  //  読み込むマップのインデックス
    public static int Map_Index
    {
        get { return m_map_index; }
        set { m_map_index = value; }
    }

    private const string FILE_PATH = "/Resources/Game/";

    public Map_Data m_map_data;

    public GameObject m_back_ground;

    private bool m_is_create = false;  //  マップ生成判定
    public bool Is_Create
    {
        get { return m_is_create; }
    }

    private void Awake()
    {
        Map_Index = DontDestroyManager.Map_Index;
        //Map_Index = 12;
    }

    void Start()
    {

        m_map_datas = new List<Map_Data>();

        //  全てのマップファイルのパスを追加する
        m_file_paths.Add(FILE_PATH + "world1-1.csv");
        m_file_paths.Add(FILE_PATH + "world1-2.csv");
        m_file_paths.Add(FILE_PATH + "world1-3.csv");
        m_file_paths.Add(FILE_PATH + "world1-4.csv");
        m_file_paths.Add(FILE_PATH + "world1-5.csv");
        m_file_paths.Add(FILE_PATH + "world1-6.csv");
        m_file_paths.Add(FILE_PATH + "world1-7.csv");
        m_file_paths.Add(FILE_PATH + "world1-8.csv");
        m_file_paths.Add(FILE_PATH + "world1-9.csv");
        m_file_paths.Add(FILE_PATH + "Map1.csv");
        m_file_paths.Add(FILE_PATH + "Map2.csv");
        m_file_paths.Add(FILE_PATH + "test1.csv");
        m_file_paths.Add(FILE_PATH + "test2.csv");
        m_file_paths.Add(FILE_PATH + "test3.csv");
        m_file_paths.Add(FILE_PATH + "test4.csv");
        m_file_paths.Add(FILE_PATH + "water1.csv");
        m_file_paths.Add(FILE_PATH + "test5.csv");

        //  追加されたパス分マップ情報を読み込む
        foreach (var path in m_file_paths) {
            Map_Data data = new Map_Data();
            Map_Load(path, ref data);
            m_map_datas.Add(data);
        }

        m_map_index = 5;
        //Debug.Log(m_map_index);
        //  指定したインデックスのマップを生成する
        Map_Create(m_map_datas[m_map_index]);
        Debug.Log(m_file_paths[m_map_index] + "を読み込みました");
    }
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            m_map_index = 0;
            SceneManager.LoadScene("GameScene");

        }
        if (Input.GetKeyDown("2"))
        {
            m_map_index = 4;
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetKeyDown("3"))
        {
            m_map_index = 8;
            SceneManager.LoadScene("GameScene");
        }


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

        for (int i = 0; i < map_data.Height; i++) {
            for (int j = 0; j < map_data.Width; j++) {
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

        float p_x = 0.0f;
        float p_y = 0.0f;

        for (int i = 0; i < map_data.Height; i++) {
            for (int j = 0; j < map_data.Width; j++) {
                if (map_data.Map_data[i, j] == 0) continue;
                if (map_data.Map_data[i, j] == 9)
                {
                    p_x = x + j;
                    p_y = y - i;
                    continue;
                }
                var obj = Instantiate(m_objects[map_data.Map_data[i, j]], new Vector3(x + j, y - i, 0.0f), Quaternion.identity);
                if (map_data.Map_data[i, j].Equals(25))
                {
                    obj.gameObject.GetComponent<Lift_Block>().Set_Map_Data_Index(i, j);
                }
                if (obj.gameObject.GetComponent<Map_Data_Seeker>())
                {
                    obj.gameObject.GetComponent<Map_Data_Seeker>().Set_Index(i, j);
                }

            }
        }

        Instantiate(m_objects[9], new Vector3(p_x, p_y, 0.0f), Quaternion.identity);

        Instantiate(m_back_ground, new Vector3(0, -4, -7), Quaternion.identity);

        m_is_create = true;

        return true;
    }

    static public void Set_Map_index(int index)
    {
        m_map_index = index;

    }
}
