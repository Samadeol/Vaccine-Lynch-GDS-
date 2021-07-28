using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectionBarPlayer : MonoBehaviour
{
    public Image InfectionBar;
    public float currentInfection;
    public Gradient gradient;
    Vitals player;
    private void Start()
    {
        player = FindObjectOfType<Vitals>();
    }
    private void Update()
    {
        currentInfection = player.infection;
        InfectionBar.fillAmount = currentInfection /player.infectionTolerance;
        InfectionBar.color = gradient.Evaluate(InfectionBar.fillAmount);
    }
}