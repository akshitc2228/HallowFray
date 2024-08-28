using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField]
    private int attackDamage = 10;

    [SerializeField]
    private float attackRange = 1f;

    [SerializeField]
    private LayerMask attackMask;

    public Vector3 attackOffset;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attackPlayer()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().takeDamage(attackDamage);
            Debug.Log(colInfo.GetComponent<PlayerHealth>().currentHealth);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        if (pos == null)
            return;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
