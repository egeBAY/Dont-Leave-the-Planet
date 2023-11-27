using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float sightRange;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private HealthController playerHealth;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (IsPlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                Debug.Log("Attacked!!");
                cooldownTimer = 0f;
                //anim.SetTrigger("");
            }
        }
    }

    private bool IsPlayerInSight()
    {   
        // Origin, size, angle, direction, distance, layermask
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * sightRange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<HealthController>();
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * sightRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (IsPlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
