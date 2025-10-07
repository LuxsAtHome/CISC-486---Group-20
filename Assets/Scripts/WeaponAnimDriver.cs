using UnityEngine;

public class WeaponAnimDriver : MonoBehaviour
{
    [Header("References")]
    public Animator animator; // drag your weapon Animator here

    [Header("Animator Parameters (names must match)")]
    public string fireParam = "Fire";
    public string reloadParam = "Reload";
    public string loadParam = "Load";   // new animation trigger

    [Header("Input Keys")]
    public KeyCode reloadKey = KeyCode.R;
    public KeyCode loadKey = KeyCode.Q;
    public int fireMouseButton = 0; // 0 = left mouse button

    // internal hashes
    int fireHash, reloadHash, loadHash;
    int stateReloadHash;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();

        fireHash = Animator.StringToHash(fireParam);
        reloadHash = Animator.StringToHash(reloadParam);
        loadHash = Animator.StringToHash(loadParam);

        stateReloadHash = Animator.StringToHash("Base Layer.Reload");
    }

    void Update()
    {
        var info = animator.GetCurrentAnimatorStateInfo(0);
        bool inReload = info.fullPathHash == stateReloadHash && info.normalizedTime < 1f;

        // Fire (blocked during reload)
        if (!inReload && Input.GetMouseButtonDown(fireMouseButton))
        {
            animator.ResetTrigger(reloadHash);
            animator.ResetTrigger(loadHash);
            animator.SetTrigger(fireHash);
        }

        // Reload
        if (!inReload && Input.GetKeyDown(reloadKey))
        {
            animator.ResetTrigger(fireHash);
            animator.ResetTrigger(loadHash);
            animator.SetTrigger(reloadHash);
        }

        // Load (triggered by Q)
        if (!inReload && Input.GetKeyDown(loadKey))
        {
            animator.ResetTrigger(fireHash);
            animator.ResetTrigger(reloadHash);
            animator.SetTrigger(loadHash);
        }
    }
}
