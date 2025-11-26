using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0 , maxHealth);
        UpdateHealthUI();
        if(overlay.color.a > 0) 
        {
            if (health < 30)
            {
                return;
            }
            
            durationTimer += Time.deltaTime;

            if(durationTimer > duration){
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            TakeDamage(Random.Range(5,10));
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            RestoreHealth(Random.Range(5,10));
        }
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health/maxHealth;

        if(fillBack > hFraction){
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float perComplete = lerpTimer/chipSpeed;
            perComplete = perComplete * perComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, perComplete);
        }

        if(fillFront < hFraction){
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float perComplete = lerpTimer / chipSpeed;
            perComplete = perComplete * perComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, perComplete);
        }

        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
        
    }

    public void RestoreHealth(float heal)
    {
        health += heal;
        lerpTimer = 0f;
    }
}
