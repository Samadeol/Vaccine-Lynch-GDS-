using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
    public Image HealthBar;
    public float currentHealth;
    public float maxHealth = 100f;
    public Gradient gradient;
    Vitals player;
    private void Start()
    {
        player = FindObjectOfType<Vitals>();
    }
    private void Update()
    {
        currentHealth = player.health;
        HealthBar.fillAmount = currentHealth / maxHealth;
        HealthBar.color = gradient.Evaluate(HealthBar.fillAmount);
    }
}
