using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Star_PheseSet", fileName = "Star_PheseSet")]

public class Star_PheseSet : ScriptableObject
{

    public int[] star_phese;
    public int Get_StarPhese(int stage_num)
    {
        return star_phese[stage_num];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
