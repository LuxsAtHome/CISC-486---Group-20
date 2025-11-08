using UnityEngine;
using UnityEngine.AI;

public class PlayerDetection : MonoBehaviour
{
    GameObject npc;
    NavMeshAgent agent;
    Animator anim;
    public Transform player;
    float visDist = 7.0f;
    float visAngle = 360.0f;
    float attackDist = 2.0f;
    float rotationSpeed = 2.0f;


    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        if (direction.magnitude < attackDist)
        {
            return true;
        }
        return false;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAttackPlayer())
        {
            Vector3 direction = player.position - npc.transform.position;
            float angle = Vector3.Angle(direction, npc.transform.forward);
            direction.y = 0;

            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotationSpeed);
        }
        else if (CanSeePlayer())
        {
            agent.SetDestination(player.position);
        }
    }
}
