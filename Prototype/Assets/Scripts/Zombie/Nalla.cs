using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nalla : MonoBehaviour
{
    public float fov = 120f;
    public Transform target;
    public bool inSight;
    public Animator anima;
    public float awakeDistance = 200f;
    public bool awareOfPlayer;
    public UnityEngine.AI.NavMeshAgent zombieAgent;
    bool playerInVision;

    private void Update()
    {
        Drawray();
        float PlayerDistance = Vector3.Distance(target.position, transform.position);
        Vector3 playerDirection = target.position - transform.position;
        //Debug.DrawRay(transform.position, playerDirection, Color.green,Time.deltaTime);
        float playerAngle = Vector3.Angle(transform.forward, playerDirection);
        if (playerAngle <= fov / 2f)
        {
            inSight = true;
        }
        else
        {
            inSight = false;
        }
        if (inSight == true && PlayerDistance <= awakeDistance && playerInVision == true)
        {
            awareOfPlayer = true;   
        }
        else awareOfPlayer=false;

        if (awareOfPlayer == true)
        {   
            anima.SetBool("aware of player",true);
            zombieAgent.SetDestination(target.position);
        }
    }
    void Drawray()
    {
        Vector3 playerDirection = target.position - transform.position;
        RaycastHit hit;
        //Debug.Log(playerInVision);
        if (Physics.Raycast(new Vector3(transform.position.x,transform.position.y+0.5f,transform.position.z), playerDirection, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                playerInVision = true;
            }
            else
            {
                playerInVision = false;
            }
        }
    }

}

