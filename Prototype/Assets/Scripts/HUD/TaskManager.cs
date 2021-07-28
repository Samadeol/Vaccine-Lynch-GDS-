using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskManager : MonoBehaviour
{
    // Start is called before the first frame update
    Text task;
    public float cured = 0f;
    public GameEnding gameEnding;
    void Start()
    {
        task = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        task.text = cured+"/17 people cured";
        if (cured == 17) gameEnding.Ending();
    }
}
