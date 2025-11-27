using UnityEngine;

public class GunFireEffects : MonoBehaviour
{
    [Header("References")]
    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
    public AudioClip fireClip;

    [Header("Kickback")]
    public Transform gunTransform;       // usually the GunModel
    public float kickBackDistance = 0.05f;
    public float kickReturnSpeed = 10f;

    Vector3 originalLocalPos;
    float currentKick;

    void Start()
    {
        if (gunTransform == null)
            gunTransform = transform;

        originalLocalPos = gunTransform.localPosition;
    }

    void Update()
    {
        // Smoothly return to original position after a shot
        if (currentKick > 0f)
        {
            currentKick = Mathf.MoveTowards(currentKick, 0f, kickReturnSpeed * Time.deltaTime);
            gunTransform.localPosition = originalLocalPos + Vector3.back * currentKick;
        }
        else
        {
            gunTransform.localPosition = originalLocalPos;
        }
    }

    public void PlayFire()
    {
        // Muzzle flash
        if (muzzleFlash != null)
            muzzleFlash.Play();

        // Sound
        if (audioSource != null && fireClip != null)
            audioSource.PlayOneShot(fireClip);

        // Kickback
        currentKick = kickBackDistance;
    }
}
