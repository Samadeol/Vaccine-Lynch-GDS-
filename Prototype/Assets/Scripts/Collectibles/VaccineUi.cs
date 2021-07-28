using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VaccineUi : MonoBehaviour
{
    // Start is called before the first frame update
    Text vaccine;
    Vitals vital;
    void Start()
    {
        vaccine = GetComponent<Text>();
        vital = FindObjectOfType<Vitals>();
    }

    // Update is called once per frame
    void Update()
    {
        if(vital.Vaccines<=9) vaccine.text = "0"  +vital.Vaccines;
        else vaccine.text = "" + vital.Vaccines;
    }
}
