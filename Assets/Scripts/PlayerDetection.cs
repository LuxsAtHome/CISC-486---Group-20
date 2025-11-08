using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerDetection : MonoBehaviour
{
    GameObject npc;
    public NavMeshAgent agent;
    public Animator anim;
    public Transform player;

    [Header("Perception")]
    public float visDist = 12f;
    public float visAngle = 60f;   
    public float attackDist = 2.2f;

    void Awake()
    {
        npc = gameObject;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public bool CanSeePlayer()
    {
        if (!player) return false;
        Vector3 dir = player.position - npc.transform.position;
        if (dir.magnitude > visDist) return false;
        if (Vector3.Angle(npc.transform.forward, dir) > visAngle) return false;

        if (Physics.Raycast(npc.transform.position + Vector3.up, dir.normalized, out var hit, visDist))
        {
            if (hit.transform != player) return false;
        }
        return true;
    }

    public bool CanAttackPlayer()
    {
        if (!player) return false;
        return Vector3.Distance(player.position, npc.transform.position) <= attackDist;
    }
}
