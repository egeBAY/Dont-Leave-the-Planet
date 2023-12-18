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
    private Animator swordAnim;
    private HealthController playerHealth;
    private PlayerAttacking playerAttacking;
    private EnemyPatrol enemyPatrol;


    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        anim = GetComponent<Animator>();
        swordAnim = transform.Find("Sword").GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (IsPlayerInSight() && !enemyPatrol.isEnemyStunned)
        {
            if (cooldownTimer >= attackCooldown)
            {
                //Attack
                cooldownTimer = 0f;
                anim.SetTrigger("meleeAttack");
                swordAnim.SetTrigger("swing");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !IsPlayerInSight();
    }

    public bool IsPlayerInSight()
    {   
        // Origin, size, angle, direction, distance, layermask
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * sightRange * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<HealthController>();
            playerAttacking = hit.transform.GetComponent<PlayerAttacking>();
        }
            
        
        return hit.collider != null;
    }
    private void DamagePlayer() // Calls in animator event
    {
        if (IsPlayerInSight() && !playerAttacking.isParrying)
        {
            playerHealth.TakeDamage(damage);
        }

        else if (playerAttacking.isParrying)
        {
            StartCoroutine(enemyPatrol.Stunned());
        }
    }

    public void Died()
    {
        gameObject.GetComponent<LootBag>().InstantiateLoots(transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * sightRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * sightRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
