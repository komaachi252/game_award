using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_effect;
    void Start()
    {
        transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        transform.Translate(0.0f, -0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, -3.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("SOLID") || col.gameObject.CompareTag("AQUA") || col.gameObject.CompareTag("CLOUD"))
        {
            FindObjectOfType<Audio_Manager>().Play("help1");
            GameObject.Find("Helpme").GetComponent<Helpme>().Help();
            var pos = this.gameObject.transform.position;
            pos.y -= 0.3f;
            Instantiate(m_effect, pos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
