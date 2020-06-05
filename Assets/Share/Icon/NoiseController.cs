using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseController : MonoBehaviour
{
    public GameObject []gameObject;
    public float amount;
    private GameObject now,set;
    Canvas canvas;

    IEnumerator GeneratePulseNoise()
    {
        for (int i = 0; i <= 180; i += 1)
        {
            now.GetComponent<Image>().material.SetFloat("_Amount", amount * Mathf.Sin(i * Mathf.Deg2Rad));
            yield return null;
        }
    }

    void Start()
    {
        set = gameObject[1];
        now = gameObject[2];
        StartCoroutine(GeneratePulseNoise());
        canvas = GetComponent<Canvas>();
    }

    public void ChangeIcon(bool waterIce = false)
    {
        now.GetComponent<Image>().gameObject.SetActive(false);

        if (set == now)
        {
            if(!waterIce)
            {
                now = gameObject[0];//水から雲
            }
            else
            {
                now = gameObject[2];//水から氷
            }
        }
        else
        {
            now = gameObject[1];//雲か氷から水
        }
        now.GetComponent<Image>().gameObject.SetActive(true);
        StartCoroutine(GeneratePulseNoise());
    }

    public void CHANGE_CANVAS()
    {
        if(canvas.enabled == true)
        {
            canvas.enabled = false;
        }
        else
        {
            canvas.enabled = true;
        }
    }
}