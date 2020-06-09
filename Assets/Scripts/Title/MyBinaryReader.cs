using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MyBinaryReader : MonoBehaviour
{
    // Start is called before the first frame update
    float m_bgm_volume;
    public float BGM_volume
    {
        get { return m_bgm_volume; }
    }
    float m_se_volume;
    public float SE_volume
    {
        get { return m_se_volume; }
    }
    void Start()
    {
        (m_bgm_volume, m_se_volume) = Load();
        FindObjectOfType<Audio_Manager>().Set_Volume(true, m_bgm_volume);
        FindObjectOfType<Audio_Manager>().Set_Volume(false, m_se_volume);
        Debug.Log(m_bgm_volume + "は" + m_se_volume);
    }

    public (float, float) Load()
    {
        var fileName = Application.dataPath + "/Resources/Game/plevel.dat";
        var reader = new BinaryReader(new FileStream(fileName, FileMode.Open));
        //読み込む処理
        var bgm_volume = reader.ReadSingle();
        var se_volume = reader.ReadSingle();
        reader.Close();

        return (bgm_volume, se_volume);
    }

    public void Save(float bgm_volume, float se_volume)
    {
        var fileName = Application.dataPath + "/Resources/Game/plevel.dat";
        var writer = new BinaryWriter(new FileStream(fileName, FileMode.OpenOrCreate));
        try
        {
            //書き込む処理
            writer.Write(bgm_volume);
            writer.Write(se_volume);
        }
        finally
        {
            writer.Close();
        }
    }
}
