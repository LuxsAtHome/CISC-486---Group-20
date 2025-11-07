using System.Collections;
using UnityEngine;

public class Door_slide : MonoBehaviour, IInteractable
{
    [Header("Lock Settings")]
    public bool isLocked = true;
    public string requiredKeyId = "Red";
    public bool consumeKey = true;

    [Header("Sliding Settings")]
    public Transform leaf;             // assign the door cube
    public Vector3 openDirection = Vector3.right;  // direction it slides into wall
    public float openDistance = 1.5f;  // how far to slide
    public float openTime = 0.35f;     // speed of animation

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen;
    private bool moving;

    void Start()
    {
        if (leaf == null)
            leaf = transform.GetChild(0); // auto assign first child if not set

        closedPos = leaf.localPosition;
        openPos = closedPos + openDirection.normalized * openDistance;
    }

    public void Interact()
    {
        if (moving) return;

        // Get the player's inventory to check for the key
        var player = GameObject.FindWithTag("Player");
        var inv = player != null ? player.GetComponent<PlayerInventory>() : null;

        if (isLocked)
        {
            if (inv != null && inv.HasKey(requiredKeyId))
            {
                inv.UseKey(requiredKeyId);
                isLocked = false;
                StartCoroutine(SlideDoorCoroutine(!isOpen)); // ✅ matches name below
            }
            else
            {
                Debug.Log("Locked! Need key: " + requiredKeyId);
            }
        }
        else
        {
            StartCoroutine(SlideDoorCoroutine(!isOpen)); // ✅ matches name below
        }
    }

    private IEnumerator SlideDoorCoroutine(bool open)
    {
        moving = true;
        Vector3 start = leaf.localPosition;
        Vector3 target = open ? openPos : closedPos;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / openTime;
            leaf.localPosition = Vector3.Lerp(start, target, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        isOpen = open;
        moving = false;
    }
}
