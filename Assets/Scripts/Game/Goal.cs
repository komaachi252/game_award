using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    public const string GAME_MANAGER = "Game_Manager";
    void Start()
    {
        this.transform.Rotate(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        //!  プレイヤーだった場合ゲームクリア呼び出し
        if(col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            FindObjectOfType<Audio_Manager>().Play("game_clear");
            GameObject.Find(GAME_MANAGER).GetComponent<Game_Manager>().Game_Clear();
        }
    }
}
