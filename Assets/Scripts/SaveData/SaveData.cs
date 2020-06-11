using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{

    //0 = 星ついてない
    //1 = 星ついてる
    public int[,] Star_SaveData { get; set; }//星の受け取り

    public bool Star_SaveWrite()
    {
        string String_StarData = "";//星の情報まとめる文字列

        for (int i = 0; i < 50; i++)
        {
            String_StarData = String_StarData + Star_SaveData[i, 0].ToString() + ",";//星1
            String_StarData = String_StarData + Star_SaveData[i, 1].ToString() + ",";//星2
            String_StarData = String_StarData + Star_SaveData[i, 2].ToString() + ",";//星3
            String_StarData = String_StarData + Star_SaveData[i, 3].ToString() + ",";//星4
        }

        PlayerPrefs.SetString("StarData", String_StarData);//ここで保存
        PlayerPrefs.Save();//保存
        Debug.Log("セーブしました　↓中身\n" + String_StarData);

        return true;
    }

    //true = セーブデータある
    //false = セーブデータない
    public bool Star_SaveRoad()
    {

        string String_StarData = PlayerPrefs.GetString("StarData");//星の情報貰う;

        Debug.Log("ロードしました 　↓中身\n" + String_StarData);

        if (String_StarData == "")
        {
            return false;//空っぽの時は終わる
        }

        string[] Split_StarData = String_StarData.Split(',');//これできれいに分割できるらしい　めっちゃ便利

        int split_count = 0;
        for (int i = 0; i < 50; i++)//ここでintに変換
        {
            Star_SaveData[i, 0] = int.Parse(Split_StarData[split_count]);//星1
            split_count++;

            Star_SaveData[i, 1] = int.Parse(Split_StarData[split_count]);//星2
            split_count++;

            Star_SaveData[i, 2] = int.Parse(Split_StarData[split_count]);//星3
            split_count++;

            Star_SaveData[i, 3] = int.Parse(Split_StarData[split_count]);//星3
            split_count++;
        }

        //for (int i = 0; i < Star_SaveData.Length; i++)
        //{
        //    Debug.Log(i + "番目　" + Star_SaveData[i, 0] + " " + Star_SaveData[i, 1] + " " + Star_SaveData[i, 2]);
        //}

        return true;
    }

    void Awake()
    {
        Star_SaveData = new int[50, 4];//生成

        for (int i = 0; i < 50; i++)//初期化
        {
            Star_SaveData[i, 0] = 0;
            Star_SaveData[i, 1] = 0;
            Star_SaveData[i, 2] = 0;
            Star_SaveData[i, 3] = 0;
        }

        Star_SaveRoad();//ロード
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    //終了時に呼ばれる
    void OnDestroy()
    {
        //Star_SaveWrite();//今のデータを書き込む
    }

    //セーブデータ削除
    public void DataReset()
    {
        PlayerPrefs.DeleteKey("StarData");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
