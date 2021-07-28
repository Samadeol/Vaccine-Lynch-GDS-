using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public Vector3 lastToLastKnown;
    public bool equal;
    public bool inAttackRange, awareOfPlayer, isWalking, isRunning, inVision, inAwareZone, isInRunningZone, inSight;
    public float attackRange = 1.15f, fov = 120f, runnningZoneRange = 20f, awareZone = 40f;
    public Transform target;
    public NavMeshAgent zombie;
    public NavMeshPath path;
    public bool lastk=true;
    public Vector3 lastknown;
    public Vector3 walkPoint;
    public LayerMask whatIsPlayer, whatIsGround;
    public float walkPointRange=50f;
    public bool walkPointSet;
    public float timeBetweenAttacks=1.5f;
    bool alreadyAttacked;
    public Animator zomb;
    public float cooldowntime=6f;
    public float timeGap=0f;
    public float timer = 0f;
    public float damage1 = 1f;
    public float damage2 = 2f;
    public float infectionRange = 1.5f;
    public float infectionMult = 1f;
    public bool Isdead;
    public AudioSource Patroll1;
    public AudioSource Patroll2;
    public AudioSource Breathing;
    public AudioSource Attack1;
    public AudioSource Attack2;
    public AudioSource Dying;
    public AudioSource Chase;
    public float lastTime = 0f;
    Vector3 random;
    Vitals player;
    void Start()
    {
        player = FindObjectOfType<Vitals>();
        Isdead = false;
    }
    void Update()
    {
        //Debug.Log(lastk);
        zomb.SetBool("Dead", Isdead);
        float PlayerDistance = Vector3.Distance(target.position, transform.position);
        Vector3 playerDirection = target.position - transform.position;
        float playerAngle = Vector3.Angle(transform.forward, playerDirection);
        DrawRay();
        if (playerAngle <= fov / 2f)
        {
            inSight = true;
            //Debug.Log("player in sight");
        }
        else
        {
            inSight = false;
        }
        if (PlayerDistance <= awareZone)
        {
            inAwareZone = true;
        }
        else
        {
            inAwareZone = false;
        }
        if (PlayerDistance <= attackRange)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }
        if (PlayerDistance <= runnningZoneRange)
        {
            isInRunningZone = true;
        }
        else
        {
            isInRunningZone = false;
        }
        if (inSight == true && inAwareZone && inVision)
        {
            awareOfPlayer = true;
        }
        else
        {
            awareOfPlayer = false;
            
        }
        random = transform.position - lastknown;
        if (random.magnitude<0.5f) lastk = true;
        lastTime += Time.deltaTime;
        if(lastTime>0.2f)
        {
            lastTime = 0f;
            lastToLastKnown = transform.position; 
        }
        if (lastToLastKnown == transform.position)
        {
            equal = true;
        }
        else
        {
            equal = false;
        }
        if (lastToLastKnown==transform.position && (isRunning || isWalking) && !awareOfPlayer && lastTime>0.1f) lastk = true;
        if (!awareOfPlayer && !inAttackRange && lastk /*transform.position==lastknown*/ )
        {
            /*if(!Isdead) */Patroling();
        }
        else
        {
            Patroll1.Stop();
            Breathing.Stop();
        }
        if (awareOfPlayer && !inAttackRange ) ChasePlayer();
        else Chase.Stop();
        if (inAttackRange && awareOfPlayer) AttackPlayer();
        else
        {
            timeGap = 0f;
            Attack1.Stop();
            Attack2.Stop();
        }
        zomb.SetBool("InAttackRange", inAttackRange);
        zomb.SetBool("AwareOfPlayer",awareOfPlayer);
        zomb.SetBool("IsWalking", isWalking);
        zomb.SetBool("IsRunning", isRunning);
        zomb.SetBool("InVision", inVision);
        zomb.SetBool("Lastk", lastk);
        
        //zomb.SetBool("",);
        if (isWalking == true) zombie.speed = 0.4f;
        if (isWalking == false) zombie.speed = 0f;
        if (isRunning == true)
        {
            zombie.speed = 4f;
            zombie.angularSpeed = 999f;
        }
        if(isRunning==false && isWalking==true) zombie.angularSpeed = 999f;
        if(isWalking == false) zombie.angularSpeed = 999f;
        if(awareOfPlayer && PlayerDistance < infectionRange) player.infection+=Time.deltaTime*infectionMult;
        if (Isdead && !Dying.isPlaying) Dying.Play();
    }
    private void Patroling()
    {
        if (!Isdead)
        {
            isRunning = false;
            Vector3 distanceToWalkPoint = transform.position - walkPoint;
            if ((lastToLastKnown == transform.position) && isWalking && !awareOfPlayer && lastTime > 0.1f || (distanceToWalkPoint.magnitude > 1f && isWalking == false))
            {
                walkPointSet = false;
                if (distanceToWalkPoint.magnitude > 1f) timer = 0f;
            }
            if (!walkPointSet) SearchWalkPoint();
            if (walkPointSet && timer == 0f)
            {
                zombie.SetDestination(walkPoint);
                isWalking = true;
            }


            //Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
            { //walkPointSet = false;
              //Invoke(nameof(walkpointfalse), cooldowntime);
                isWalking = false;
                timer += Time.deltaTime;
                zombie.SetDestination(transform.position);
            }
            if (timer > cooldowntime)
            {
                walkPointSet = false;
                timer = 0f;
            }
            if (isWalking == true)
            {
                if (!Patroll1.isPlaying) Patroll1.Play();
            }
            else Patroll1.Stop();
            if (isWalking == false)
            {
                if (!Breathing.isPlaying) Breathing.Play();
            }
            else Breathing.Stop();
        }
        else
        {
            zombie.SetDestination(transform.position);
            //isWalking = false;
            //isRunning = false;
        }
    }
    private void walkpointfalse()
    {
        walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    private void ChasePlayer()
    {
        lastknown = target.position;
        lastknown.y = transform.position.y;
        if (!Isdead) zombie.SetDestination(target.position);
        else zombie.SetDestination(transform.position);
        isWalking = true;
        if (isInRunningZone) isRunning = true;
        else isRunning = false;
        lastk = false;
        if (!Chase.isPlaying && !Isdead) Chase.Play();
        if (Isdead) Chase.Stop();
    }
    private void AttackPlayer()
    {
        if(!Isdead) transform.LookAt(target);
        timeGap+=Time.deltaTime;
        if (!alreadyAttacked)
        {
            if (timeGap > 5.2f)
            {
                Attack1.Stop();
                zomb.SetBool("NeckBite", true);
                player.TakeDamage(damage2);
                Invoke(nameof(Resettimegap), 3.7f);
                if(!Attack2.isPlaying)
                {
                    Attack2.Play();
                }
            }
            else
            {
                Attack2.Stop();
                zomb.SetBool("NeckBite", false);
                player.TakeDamage(damage1);
                if(!Attack1.isPlaying)
                {
                    Attack1.Play();
                }
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        else
        {
            if(!Isdead) zombie.SetDestination(transform.position);
        }
    }
    private void Resettimegap()
    {
        timeGap = 0f;
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void DrawRay()
    {
        RaycastHit hit;
        Vector3 playerDirection = target.position - transform.position;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), playerDirection, out hit))
        {
            if (hit.transform.tag == "Player" || hit.transform.tag =="Zombie")
            {
                inVision = true;
            }
            else
            {
                inVision = false;
            }
        }
    }
    
}
