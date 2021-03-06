using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicBullet : MonoBehaviour
{
    private float speed;
    private float gravity;
    private Vector3 startPosition;
    private Vector3 startForward;
    public float dealDamage = 30;
    Vector3 currentPoint;
    Vector3 nextPoint;
    private bool isInitialized = false;

    private float startTime = -1;

    public void Initialize(Transform startPoint, float speed, Vector3 bull_dir, float gravity)
    {
        this.startPosition = startPoint.position;
        //this.startForward = startPoint.forward.normalized;
        this.startForward = bull_dir;
        this.speed = speed;
        this.gravity = gravity;
        isInitialized = true;
    }

    private Vector3 FindPointOnParabola(float time)
    {
        Vector3 point = startPosition + (startForward * time * speed);
        Vector3 gravityVec = Vector3.down * time * time * gravity;
        return point + gravityVec;
    }

    private bool CastRayBetweenPoints(Vector3 startPoint, Vector3 endPoint, out RaycastHit hit)
    {
        Debug.DrawRay(startPoint, endPoint - startPoint, Color.green, 5);
        
        return Physics.Raycast(startPoint, endPoint - startPoint, out hit, (endPoint - startPoint).magnitude);
    }

    private void OnHit(RaycastHit hit)
    {
        GetVac getvac = hit.transform.GetComponent<GetVac>();
        if (getvac)
        {
            getvac.OnVac(dealDamage);
        }
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!isInitialized) return;
        if (startTime < 0) startTime = Time.time;
        float currentTime = Time.time - startTime;
        float prevTime = currentTime - Time.fixedDeltaTime;
        float nextTime = currentTime + Time.fixedDeltaTime;

        RaycastHit hit;
        currentPoint = FindPointOnParabola(currentTime);

        if (prevTime > 0)
        {
            Vector3 prevPoint = FindPointOnParabola(prevTime);
            if (CastRayBetweenPoints(prevPoint, currentPoint, out hit))
            {
                OnHit(hit);
            }
        }

        nextPoint = FindPointOnParabola(nextTime);
        if (CastRayBetweenPoints(currentPoint, nextPoint, out hit))
        {
            OnHit(hit);
        }
        
    }
    void Update()
    {
        if (!isInitialized || startTime < 0) return;

        float currentTime = Time.time - startTime;
        Vector3 currentPoint = FindPointOnParabola(currentTime);
        transform.position = currentPoint;
    }
}
