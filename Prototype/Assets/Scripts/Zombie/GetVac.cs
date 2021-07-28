using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetVac : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    public HealthBar healthBar;
    public GameObject hbar;
    private float health;
    private TaskManager taskmanager;
    Score score;
    ZombieMovement zombie;
    private float x = 0f;
    void Start()
    {
        health = maxHealth;
        taskmanager = FindObjectOfType<TaskManager>();
        zombie = GetComponent<ZombieMovement>();
        score = FindObjectOfType<Score>();
    }

    //Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (x==0)
            {
                taskmanager.cured++;
                score.UpdateScore();
                score.deltime = 0f;
                x = 1;
            }
            if(zombie.Isdead == false) zombie.Isdead = true;
            Debug.Log(zombie.Isdead);
            //Destroy(hbar);
            //healthBar.SetSize(0f);
            Invoke(nameof(Death), 3.5f);
            healthBar.SetSize(0f);
        }
        else zombie.Isdead=false;
        if(health>1f)
        {
            healthBar.SetSize(health / 100f);
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }

    public void OnVac(float damage)
    {
        this.health -= damage;
        //Debug.Log("wwhefduhwhefhyuhew");
        healthBar.SetColor(Color.white);
        Invoke(nameof(ChangeColor),0.1f);
        
    }
    private void ChangeColor()
    {
        healthBar.SetColor(Color.red);
    }
}
