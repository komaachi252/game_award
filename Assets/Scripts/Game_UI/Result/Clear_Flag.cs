using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_Flag : MonoBehaviour
{
    public GameObject GameManeger;//ゲームマネージャーをもらう

    public GameObject Result;//リザルトのゲームオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        Result.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = GameManeger.GetComponent<Game_Manager>().Is_Clear_Flag;

        if (flag == true)
        {
            Result.SetActive(true);
        }
    }
}
