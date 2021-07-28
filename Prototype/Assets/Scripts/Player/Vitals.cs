using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHealth = 100;
    public float infectionTolerance = 100;
    public int Vaccines=30;

    public float health;
    public float infection;
    public GameEnding gameEnding;
    public AudioSource equip;
    void Start()
    {
        health = maxHealth;
        infection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (infection >= infectionTolerance)
        {
            Debug.Log("Game Over");
            gameEnding.Low();
            //Application.Quit();
        }
        if (health <= 0) gameEnding.Low();
    }

    public bool Heal(float amount)
    {
        if (health < maxHealth)
        {
            if(health + amount >= maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += amount;
            }
            return true;
        }
        else return false;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void OnCure()
    {
            if (infection > 0)
            {
                if (infection - 10 <= 0) infection = 0;
                else infection -= 10;
                Vaccines--;
            equip.Play();
            }
    }
}
