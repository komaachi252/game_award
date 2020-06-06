using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyaraManager : MonoBehaviour
{

    [SerializeField] GameObject[] kyara;//キャラクター

    //0 = Cloud
    //1 = Water
    //2 = ice
    [SerializeField] ExposeNowMode now_mode;//今のモード

    
    int now_flag;//今のモードフラグ

    // Start is called before the first frame update
    void Start()
    {
        GameObject now = now_mode.expose_now_mode.Get_NowMode;//今のモードを受け取る

        if (now.name == "cloud")//雲
        {
            kyara[0].SetActive(true);
            kyara[1].SetActive(false);
            kyara[2].SetActive(false);
        }
        else if (now.name == "water")//水
        {
            kyara[0].SetActive(false);
            kyara[1].SetActive(true);
            kyara[2].SetActive(false);
        }
        else if (now.name == "ice")//氷
        {
            kyara[0].SetActive(false);
            kyara[1].SetActive(false);
            kyara[2].SetActive(true);
        }
        else//例外 雲
        {
            now_flag = 2;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
