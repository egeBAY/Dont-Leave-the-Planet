using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacking : MonoBehaviour
{
    [Header("Attacking")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator swordAnimator;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackCooldown;
    
    public float meleeAttackRange;
    public int damage;
    public LayerMask enemyLayers;

    private float cooldownTimer = Mathf.Infinity;

    [Header("Block/Parry")]
    public bool isParrying;
    [SerializeField] private float parryCooldown;
    [SerializeField] private float parryWindow;
    private float parryCooldownTimer = Mathf.Infinity;
    private float parryWindowTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        parryCooldownTimer += Time.deltaTime;
        parryWindowTimer += Time.deltaTime;
    }

    public void HandleAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed && (cooldownTimer >= attackCooldown))
        {
            cooldownTimer = 0;
            playerAnimator.SetTrigger("playerAttack");
            swordAnimator.SetTrigger("meleeSwing");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, meleeAttackRange, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HealthController>().TakeDamage(damage);
                enemy.GetComponentInParent<EnemyPatrol>().Attacked();
            }
        }
        
    }

    public void HandleParry(InputAction.CallbackContext context)
    {
        if (context.performed && (parryCooldownTimer >= parryCooldown))
        {
            parryCooldownTimer = 0;
            isParrying = true;
            StartCoroutine(OnParry());
        }
    }

    private IEnumerator OnParry()
    {
        yield return new WaitForSeconds(parryWindow);

        isParrying = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, meleeAttackRange);
    }
}
