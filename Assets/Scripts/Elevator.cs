using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform elevator;           // The platform
    public float descendDistance = 5f;   // How far it goes down
    public float speed = 2f;             // How fast it moves

    private Vector3 startPos;
    private Vector3 endPos;
    private bool playerOn = false;

    void Start()
    {
        startPos = elevator.position;
        endPos = startPos - new Vector3(0, descendDistance, 0);
    }

    void Update()
    {
        if (playerOn)
        {
            // Move toward end position
            elevator.position = Vector3.MoveTowards(
                elevator.position,
                endPos,
                speed * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOn = true;
        }
    }
}
