using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class StageClearManager : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Start_anime();
    }

    // Update is called once per frame
    void Update()
    {

    }


    //アニメーションを開始する
    public void Start_anime()
    {
        animator.SetTrigger("Big");
    }

    //アニメーションをやり終わったか
    //true = まだ
    //false = おわった
    public bool Active_Anime()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Big_Idle") == true)
        {
            return false;
        }
        return true;
    }
}
