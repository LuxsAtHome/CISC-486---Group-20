using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    [Header("Key Settings")]
    public string keyId = "Red";   // must match Door's requiredKeyId

    [Header("Optional FX")]
    public AudioClip pickupSfx;
    public GameObject pickupVfx;

    // === For 'E' Interact ===
    public void Interact()
    {
        PickupKey();
    }

    // === For Auto Pickup when walking over ===
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupKey();
        }
    }

    // === Shared function ===
    private void PickupKey()
    {
        // Find player’s inventory
        var player = GameObject.FindWithTag("Player");
        var inv = player ? player.GetComponent<PlayerInventory>() : null;
        if (inv == null) return;

        inv.AddKey(keyId);

        // Play SFX/VFX
        if (pickupSfx) AudioSource.PlayClipAtPoint(pickupSfx, transform.position);
        if (pickupVfx) Instantiate(pickupVfx, transform.position, Quaternion.identity);

        // Remove the key from the scene
        Destroy(gameObject);
    }
}
