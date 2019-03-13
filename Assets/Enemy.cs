using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    Transform[] patrolPoints;

    private int nextPatrolPoint;
    public float speed;

    public void Start()
    {
        nextPatrolPoint = 0;
    }

    public void Update()
    {
        float distanceToWaypoint = Vector3.Distance(patrolPoints[nextPatrolPoint].position, transform.position);
        if (distanceToWaypoint < 2)
        {
            nextPatrolPoint++;
            if (nextPatrolPoint > 3)
                nextPatrolPoint = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[nextPatrolPoint].position, speed * Time.deltaTime);
    }
}
