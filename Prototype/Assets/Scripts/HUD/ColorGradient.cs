using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorGradient : MonoBehaviour
{
    // Start is called before the first frame update
    public Gradient gradient;
    public Image image;
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        image.color = gradient.Evaluate(1);
    }
}
