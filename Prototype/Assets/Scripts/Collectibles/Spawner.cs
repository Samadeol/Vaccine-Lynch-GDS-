using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject entity;
    public float delay = 1;
    public bool isfilled = true;

    float timepassed;

    private void Start()
    {
        Instantiate(entity, transform.position, transform.rotation,transform);
        timepassed = delay;
    }

    private void Update()
    {
        if(!isfilled && timepassed > 0)
        {
            timepassed -= Time.deltaTime;
        }
        else if(!isfilled && timepassed <= 0)
        {
            Instantiate(entity, transform.position, transform.rotation, transform);
            timepassed = delay;
            isfilled = true;
        }
    }
}
