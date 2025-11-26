using UnityEngine;

public class SimpleElevator : MonoBehaviour
{
    public Transform elevator;        // platform to move
    public float distance = 5f;       // positive for up, negative for down
    public float speed = 2f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool moving = false;

    void Start()
    {
        if (elevator == null)
            elevator = transform;

        startPos = elevator.position;
        endPos = startPos + Vector3.up * distance;
    }

    void Update()
    {
        if (!moving) return;

        elevator.position = Vector3.MoveTowards(
            elevator.position,
            endPos,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(elevator.position, endPos) < 0.01f)
            moving = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            moving = true;
    }
}
