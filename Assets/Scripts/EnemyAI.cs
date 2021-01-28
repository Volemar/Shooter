using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    Animator animator;
    NavMeshAgent navMeshAgent;
    EnemyHealth health;
    float distanceToTarget = Mathf.Infinity;
    bool isProveked = false;

    private void Start()
    {
        health = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        if (health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(isProveked)
        {
            EngageTarget();
        } else if (distanceToTarget <= chaseRange)
        {
            isProveked = true;
        }
    }

    public void OnDamageTaken()
    {
        isProveked = true;
    }

    void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget < navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        animator.SetBool("Attack", true);
        print("Feel attacked");
    }
    private void ChaseTarget()
    {
        animator.SetBool("Attack", false);
        animator.SetTrigger("Move"); 
        navMeshAgent.SetDestination(target.position);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
