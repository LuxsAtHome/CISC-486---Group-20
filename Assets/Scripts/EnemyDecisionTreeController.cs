using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDecisionTreeController : MonoBehaviour
{
    
    interface IDecisionNode { void Execute(); }
    class ActionNode : IDecisionNode { readonly Action act; public ActionNode(Action a) { act = a; } public void Execute() => act?.Invoke(); }
    class QuestionNode : IDecisionNode
    {
        readonly Func<bool> q; readonly IDecisionNode t, f;
        public QuestionNode(Func<bool> q, IDecisionNode t, IDecisionNode f) { this.q = q; this.t = t; this.f = f; }
        public void Execute() { if (q()) t.Execute(); else f.Execute(); }
    }

    [Header("Scene Refs")]
    public PlayerDetection detection;         
    public Transform[] patrolPoints;          
    public Animator anim;                    

    [Header("Tuning")]
    public float turnSpeed = 10f;
    public float patrolWait = 0.75f;


    IDecisionNode root;
    NavMeshAgent agent;
    int patrolIndex = 0;
    float waitTimer = 0f;
    float nextAttackTime = 0f;
    public float attackCooldown = 1.0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!detection) detection = GetComponent<PlayerDetection>();
        if (!anim) anim = GetComponent<Animator>();
    }

    void Start()
    {
        BuildTree();
        if (patrolPoints != null && patrolPoints.Length > 0)
            agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    void Update()
    {
        if (detection == null || detection.player == null) return;
        root.Execute();
        if (anim) anim.SetFloat("Speed", agent.velocity.magnitude);
    }


    void BuildTree()
    {
        var attack = new ActionNode(Attack);
        var chase = new ActionNode(Chase);
        var patrol = new ActionNode(Patrol);

        var seeNode = new QuestionNode(detection.CanSeePlayer, chase, patrol);
        root = new QuestionNode(detection.CanAttackPlayer, attack, seeNode);
    }


    void Patrol()
    {
        agent.isStopped = false;
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= patrolWait)
            {
                waitTimer = 0f;
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
                agent.SetDestination(patrolPoints[patrolIndex].position);
            }
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        var pos = detection.player.position;
        if (agent.destination != pos) agent.SetDestination(pos);
    }

    void Attack()
    {
        agent.isStopped = true;


        Vector3 dir = detection.player.position - transform.position; dir.y = 0f;
        if (dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
        }


        if (Time.time >= nextAttackTime)
        {
            if (anim) anim.SetTrigger("Attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!detection) return;
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, detection.visDist);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, detection.attackDist);
        Vector3 L = Quaternion.Euler(0, -detection.visAngle, 0) * transform.forward;
        Vector3 R = Quaternion.Euler(0, detection.visAngle, 0) * transform.forward;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + L * detection.visDist);
        Gizmos.DrawLine(transform.position, transform.position + R * detection.visDist);
    }
}
