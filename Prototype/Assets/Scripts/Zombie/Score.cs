using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    TaskManager taskman;
    public int score;
    public float deltime = 0f;
    public float currtime = 0f;
    public float XP = 50000;
    Text scoretext;
    void Start()
    {
        taskman = FindObjectOfType<TaskManager>();
        score = 0;
        scoretext = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        currtime += Time.deltaTime;
        deltime += Time.deltaTime;
        if (score > 0) scoretext.text = ""+score;
        else scoretext.text = "0000";
    }
    public void UpdateScore()
    {
        score += Mathf.RoundToInt(XP / (Mathf.RoundToInt(50 - 40*Mathf.Exp(-currtime/300)) *Mathf.RoundToInt(50-40*Mathf.Exp(-deltime/300))));
    }
}
