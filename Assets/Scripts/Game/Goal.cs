using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    public const string GAME_MANAGER = "Game_Manager";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        //!  プレイヤーだった場合ゲームクリア呼び出し
        if(col.gameObject.CompareTag("Player")){
            GameObject.Find(GAME_MANAGER).GetComponent<Game_Manager>().Game_Clear();
        }
    }
}
