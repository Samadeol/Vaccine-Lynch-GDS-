using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthCrate : MonoBehaviour
{
    // Start is called before the first frame update
    public float healAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        Vitals player = other.gameObject.GetComponent<Vitals>();

        if (player)
        {
            if(player.Heal(healAmount))
            {
                Destroy(gameObject);
                gameObject.GetComponentInParent<Spawner>().isfilled = false;
            }
        }
    }
}
