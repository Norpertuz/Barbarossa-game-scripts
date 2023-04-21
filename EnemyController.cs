using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private float
        range = 6f,
        attackRange = 1f;
    public bool attackState;
    Animator anim;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    CharacterCombat combat;
    private Transform target;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackState = false;
        anim = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
    }

    private void setAttackAnimation()
    {
        anim.SetTrigger("Attack");
    }

    private void setWalkAnimation()
    {
        anim.SetBool("isWalking", true);
    }

    private void rotateEnemy()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    private void attack(float distance)
    {
        setWalkAnimation();
        navMeshAgent.SetDestination(target.position);
        if (distance <= navMeshAgent.stoppingDistance || distance <= attackRange)
        {
            rotateEnemy();
            setAttackAnimation();
            anim.SetBool("isWalking", false);

            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                combat.Attack(targetStats);
            }
        }
    }

    [SerializeField] private LayerMask ground;
    private float walkRange = 250f;
    private Vector3 walkingVector;

    private void walk()
    {
        float x = Random.Range(-walkRange, walkRange), z = Random.Range(-walkRange, walkRange);
        walkingVector = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        setWalkAnimation();
        navMeshAgent.SetDestination(walkingVector);
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= range)
        {
            attack(distance);
            attackState = true;
        }
        else
        {
            walk();
            attackState = false;
        }
    }
}
