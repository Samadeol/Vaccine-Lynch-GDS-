using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform bar;
    public Transform cam;
    private  void Awake()
    {
        bar = transform.Find("Bar");
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.red;
    }
    private void Update()
    {
        transform.LookAt(cam);
    }
    public void SetSize(float sizeNormalized)
    {
        bar.localScale=new Vector3(sizeNormalized, 1f);
    }
    public void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
