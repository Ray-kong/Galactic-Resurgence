using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyState
    {
        PATROL,
        ATTACK,
        CHASE,
        DEAD
    }

    private EnemyState currentState;
    public NavMeshAgent agent;
    public Transform player;
    public float enemySpeed = 3.5f;
    public float chaseDistance = 20f;
    public float attackDistance = 15f;
    public ImprovedEnemyGun enemyGun;

    private float distanceToPlayer;
    
    public GameObject[] wanderPoints;
    private int currentDestIndex = 0;

    private Target health;

    private Animator anim;

    public float fov = 180f;

    public Transform enemyEyes;
    public bool useSight = false;
    public LayerMask maskExclusion;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.PATROL;
        health = GetComponent<Target>();
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (wanderPoints.Length == 0)
        {
            wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");
        }
        anim = GetComponent<Animator>();
        agent.speed = enemySpeed;
        agent.stoppingDistance = attackDistance;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!health.IsAlive())
        {
            currentState = EnemyState.DEAD;
        }
        switch (currentState)
        {
            case EnemyState.PATROL:
                UpdatePatrolState();
                break;
            case EnemyState.CHASE:
                UpdateChaseState();
                break;
            case EnemyState.ATTACK:
                UpdateAttackState();
                break;
            case EnemyState.DEAD:
                UpdateDeadState();
                break;
        }
        SetBlendSpaceFloats();
    }

    private void UpdatePatrolState()
    {
        agent.stoppingDistance = 0f;
        agent.SetDestination(wanderPoints[currentDestIndex].transform.position);
        PatrolPoints();
        if (distanceToPlayer <= chaseDistance && IsPlayerInClearFOV())
        {
            currentState = EnemyState.CHASE;
        }
        //agent.SetDestination(player.position);
    }

    private void UpdateChaseState()
    {
        agent.stoppingDistance = attackDistance;
        if (distanceToPlayer <= attackDistance)
        {
            currentState = EnemyState.ATTACK;
        } else if (distanceToPlayer > chaseDistance)
        {
            currentState = EnemyState.PATROL;
        }
        agent.SetDestination(player.position);
    }
    
    private void UpdateAttackState()
    {
        agent.stoppingDistance = attackDistance;
        FaceTarget(player.position);
        enemyGun.Shoot();
        if (distanceToPlayer > attackDistance)
        {
            currentState = EnemyState.CHASE;
        }
    }

    private void UpdateDeadState()
    {
        agent.speed = 0f;
        anim.SetTrigger("dead");
    }

    private void SetBlendSpaceFloats()
    {
        Vector3 velocity = agent.velocity;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        float forwardSpeed = Vector3.Dot(velocity, forward);
        float sidewaysSpeed = Vector3.Dot(velocity, right);

        float x = Mathf.Clamp(sidewaysSpeed/ enemySpeed, -1f, 1f);
        float y = Mathf.Clamp(forwardSpeed/ enemySpeed, -1f, 1f);
        
        anim.SetFloat("x", x);
        anim.SetFloat("y", y);

        // Debug.Log("Forward Speed: " + forwardSpeed);
        // Debug.Log("Sideways Speed: " + sidewaysSpeed);
    }
    
    private void PatrolPoints()
    {
        if (Vector3.Distance(transform.position, wanderPoints[currentDestIndex].transform.position) < 1)
        {
            currentDestIndex = (currentDestIndex + 1) % wanderPoints.Length;
            Vector3 nextDestination = wanderPoints[currentDestIndex].transform.position;
            agent.SetDestination(nextDestination);
        }
    }
    
    private void FaceTarget(Vector3 target) 
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    
    private bool IsPlayerInClearFOV()
    {
        if (!useSight)
        {
            // in the case we don't want to use sight we just set to true
            // so if the enemy is in range they will always be chased and shot at
            return true;
        }
        RaycastHit hitInfo;
        Vector3 directionToPlayer = player.position - enemyEyes.position;
        if (Vector3.Angle(directionToPlayer, enemyEyes.forward) <= fov / 2)
        {
            if (Physics.Raycast(enemyEyes.position, directionToPlayer, out hitInfo, chaseDistance, ~maskExclusion))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    private void OnDrawGizmos()
    {
        // range stuff
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        
        // fov stuff
        if (useSight)
        {
            Vector3 frontRayPoint = enemyEyes.position + (enemyEyes.forward * chaseDistance);

            Quaternion leftRotation = Quaternion.AngleAxis(fov * 0.5f, Vector3.up);
            Quaternion rightRotation = Quaternion.AngleAxis(-fov * 0.5f, Vector3.up);

            Vector3 leftRayDirection = leftRotation * enemyEyes.forward;
            Vector3 rightRayDirection = rightRotation * enemyEyes.forward;

            Vector3 leftRayPoint = enemyEyes.position + leftRayDirection * chaseDistance;
            Vector3 rightRayPoint = enemyEyes.position + rightRayDirection * chaseDistance;

            Debug.DrawLine(enemyEyes.position, frontRayPoint, Color.cyan);
            Debug.DrawLine(enemyEyes.position, leftRayPoint, Color.cyan);
            Debug.DrawLine(enemyEyes.position, rightRayPoint, Color.cyan);
        }
    }
}
