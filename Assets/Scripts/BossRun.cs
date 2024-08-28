using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    [SerializeField]
    private float speed = 40f;

    [SerializeField]
    private float attackRange = 3f;

    float attackRate = 1f;
    float nextAttackTime = 1f;

    Transform player;
    Rigidbody2D bossRb;
    BossController bossController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        } catch(MissingReferenceException) { }
        bossRb = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<BossController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.facePlayer();

        Vector2 target = new Vector2(player.position.x, bossRb.position.y);
        Vector2 newPos = Vector2.MoveTowards(bossRb.position, target, speed * Time.fixedDeltaTime); //issues with deltaTime
        bossRb.MovePosition(newPos);

        if((Vector2.Distance(player.position, bossRb.position)) <= attackRange)
        {
            //attackPlayer:
            if(Time.time >= nextAttackTime)
            {
                animator.SetTrigger("Attack");
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
