using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private Transform[] patrolPoints;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    private Rigidbody2D rbEnemy;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private float directionThreshold;
    private int nextWaypointIndex = 0;
    private float distanceToNextWaypoint;


    private void Awake()
    {
        rbEnemy = enemy.gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MoveToNextPoint();
    }

    private IEnumerator MoveEnemy(Transform target)
    {
        
        while(Vector3.Distance(enemy.position, target.position) > directionThreshold)
        {
            Vector3 dir = (target.position - enemy.transform.position).normalized;
            rbEnemy.velocity = new Vector2(speed * dir.x, rbEnemy.velocity.y);
            yield return null;
        }

        yield return new WaitForSeconds(waitTime);
        
        nextWaypointIndex += 1;
        if (nextWaypointIndex == patrolPoints.Length)
            nextWaypointIndex = 0;
        
        MoveToNextPoint();

    }

    private void MoveToNextPoint()
    {
        StartCoroutine(MoveEnemy(patrolPoints[nextWaypointIndex]));
        
    }
}
