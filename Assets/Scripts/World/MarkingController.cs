using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkingController : MonoBehaviour
{
    private Material[] stage_material;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject WorldMarking = GameObject.Find("WorldMarking");
        GameObject StageMarking = GameObject.Find("StageMarking");

        int stage_num = 0;
        foreach (Transform transform in StageMarking.transform)//数数える
        {
            stage_num++;
        }

        stage_material = new Material[stage_num];

        for (int i = 0; i < stage_num; i++)//データ貰う
        {

            stage_material[i] = StageMarking.transform.GetChild(i).GetComponent<Renderer>().material;
            
        }

        stage_material[0].color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        int now_stage = StageController.Get_stage();

        for (int i = 0; i < stage_material.Length; i++)
        {
            
        }
    }
}
