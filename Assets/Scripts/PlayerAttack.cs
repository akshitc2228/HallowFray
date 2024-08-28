using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    public Animator playerAnimator;

    public GameObject boss;
    private BossHealth bossScript;

    private int attackDamage = 10;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    [SerializeField]
    private Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private void Start()
    {
        if(boss != null)
        {
            bossScript = boss.GetComponent<BossHealth>();
        } else
        {
            Debug.Log("boss object not attached");
        }
    }

    private void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }
    }

    void Attack()
    {
        //play attack animation:
        playerAnimator.SetTrigger("Attack");

        //detect enemies to hit:
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //damage enemies:
        foreach(Collider2D enemy in hitEnemies) 
        {
            bossScript.takeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) 
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}