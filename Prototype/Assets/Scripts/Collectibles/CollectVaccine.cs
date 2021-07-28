using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectVaccine : MonoBehaviour
{
    public GameObject player;
    public int ammoVal = 10;
 
    private void OnTriggerEnter(Collider other)
    {
        Vitals player = other.gameObject.GetComponent<Vitals>();
        if (player)
        {
            player.Vaccines += ammoVal;
            gameObject.GetComponentInParent<Spawner>().isfilled = false;
            Destroy(gameObject);
        }
    }
}
