using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Maneger : MonoBehaviour
{

    [SerializeField] Animator anime01;
    [SerializeField] Animator anime02;

    [SerializeField] ExposeNowMode now_mode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageController.Get_Index() == 2)
        {
            GameObject now = now_mode.expose_now_mode.Get_NowMode;//今のモードを受け取る

            if (now.name == "water")//水
            {
                anime01.SetBool("Start", true);
                anime01.SetBool("No", false);

                anime02.SetBool("Start", true);
                anime02.SetBool("No", false);
            }
            else
            {
                anime01.SetBool("Start", false);
                anime01.SetBool("No", true);

                anime02.SetBool("Start", false);
                anime02.SetBool("No", true);
            }
        }

       
    }
}
