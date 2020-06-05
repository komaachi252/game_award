using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManeger : MonoBehaviour
{
    [SerializeField] Text score_text;//スコアテキスト
    [SerializeField] ExposePheseCount Phese_count;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int phese = Phese_count.Phese_cnt.Phase_Cnt;
        score_text.text = phese.ToString();
    }
}
