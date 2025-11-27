using UnityEngine;
using UnityEngine.InputSystem;  


public class SimpleGun : MonoBehaviour
{
    public Camera fpsCamera;
    public float range = 100f;
    public int damage = 20;
    public float fireRate = 10f;

    public GunFireEffects fireEffects;   

    float nextTimeToFire;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // VISUALS + SOUND
        if (fireEffects != null)
            fireEffects.PlayFire();

        // RAYCAST DAMAGE
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
