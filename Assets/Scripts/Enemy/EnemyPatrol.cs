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
    public bool isEnemyAttacked;
    public bool isEnemyStunned;
    private Rigidbody2D rbEnemy;
    private Vector3 initScale;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float distanceThreshold;
    private int nextWaypointIndex = 0;
    private float distanceToNextWaypoint;
    private Vector2 movementVector;

    [Header("Idle Behaviour")]
    [SerializeField] private float waitTime;
    [SerializeField] private float stunTime;
    private float idleTimer;

    [Header("Animator")]
    [SerializeField] private Animator enemyAnim;


    private void Awake()
    {
        initScale = enemy.localScale;
        rbEnemy = enemy.gameObject.GetComponent<Rigidbody2D>();
        enemyAnim = enemy.gameObject.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        enemyAnim.SetBool("moving", false);
        movementVector = Vector2.zero;
    }

    private void Update()
    {
        if (!isEnemyAttacked && !isEnemyStunned)
        {
            float distance = Vector3.Distance(enemy.position, patrolPoints[nextWaypointIndex].position);
            if (distance < distanceThreshold)
            {
                TargetChange();
            }

            else
                MoveInDirection(patrolPoints[nextWaypointIndex]);
        }

        else if(isEnemyAttacked && !enemy.GetComponent<MeleeEnemyController>().IsPlayerInSight())
            isEnemyAttacked = false;
    }


    public void Attacked()
    {
        // Stop Enemy
        isEnemyAttacked = true;
        movementVector = Vector2.zero;
        enemyAnim.SetBool("moving", false);
        // Make enemy face player
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 dirToPlayer = (playerTransform.position - enemy.transform.position).normalized;
        enemy.localScale = new Vector3(-initScale.x * dirToPlayer.x,
            initScale.y, initScale.z);

    }

    public IEnumerator Stunned()
    {
        Debug.Log("Enemy Stunned");
        isEnemyStunned = true;
        movementVector = Vector2.zero;
        enemy.Find("Sword").gameObject.SetActive(false);
        enemyAnim.SetBool("isStunned", isEnemyStunned);
        yield return new WaitForSeconds(stunTime);
        isEnemyStunned = false;
        enemy.Find("Sword").gameObject.SetActive(true);
        enemyAnim.SetBool("isStunned", isEnemyStunned);
        Debug.Log("Enemy Stun Over");
    }

    private void TargetChange()
    {
        enemyAnim.SetBool("moving", false);
        movementVector = Vector2.zero;
        idleTimer += Time.deltaTime;

        if (idleTimer > waitTime)
        {
            nextWaypointIndex += 1;
            if (nextWaypointIndex == patrolPoints.Length)
                nextWaypointIndex = 0;
        }
            
    }

    private void MoveInDirection(Transform target)
    {
        idleTimer = 0;
        enemyAnim.SetBool("moving", true);
        Vector3 dir = (target.position - enemy.transform.position).normalized;

        //Make enemy face direction
        enemy.localScale = new Vector3(-initScale.x * dir.x,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * dir.x * speed,
            enemy.position.y, enemy.position.z);
    }
}
