using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    #region SerializableVars
    [SerializeField] private float health = 100f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask groundMask, playerMask;

    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private float walkPointRange;
    [SerializeField] private bool walkPointIsSet = false;

    [SerializeField] private float timeBetweenAttacks; // may not need later if we have some sort of gun class
    [SerializeField] private bool hasAttacked;

    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private bool playerInSight, playerInAttackRange;

    [SerializeField] private float aimOffset = 5f;
    #endregion

    private Transform player;
    private EnemyGun gun;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gun = GetComponent<EnemyGun>();
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerMask);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerMask);
        
        if (!playerInSight && !playerInAttackRange)
        {
            Patrolling();
        } 
        else if (playerInSight && !playerInAttackRange)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointIsSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointIsSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))
        {
            walkPointIsSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        float aimXOff = Random.Range(player.rotation.x - aimOffset, player.rotation.x + aimOffset);
        float aimYOff = Random.Range(player.rotation.y - aimOffset, player.rotation.y + aimOffset);
        float aimZOff = Random.Range(player.rotation.z - aimOffset, player.rotation.z + aimOffset);


        transform.Rotate(new Vector3(aimXOff, aimYOff, 0));
        gun.EnemyShoot();
        /*

        // attack logic here
        if (!hasAttacked)
        {
            Debug.Log("Attacking Player");
            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }*/
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    // visual indicator
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
