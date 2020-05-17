using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    private Vector3 Start_posi = new Vector3(0.0f, 1.7f, -1.0f);//初期座標
    private Vector3 Lookat_posi = new Vector3(0.0f, 1.0f, 0.0f);//見る座標

    private float distance;//距離
    private Vector3 Front_vec;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = Start_posi;//初期位置

        distance = Vector3.Distance(this.transform.position, Lookat_posi);
        this.transform.LookAt(Vector3.up, Lookat_posi);

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.RotateAround(Lookat_posi, Vector3.up, 10.0f * Time.deltaTime);

    }

    
}
