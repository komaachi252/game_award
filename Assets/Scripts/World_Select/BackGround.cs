using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private float speed;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();

        speed = 0.05f;
        
    }

    void OnDestroy()
    {
        image.material.SetTextureOffset("_MainTex", new Vector2(0.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        float move = Time.time * speed;
        image.material.SetTextureOffset("_MainTex", new Vector2(move, 0.0f));
    }
}
