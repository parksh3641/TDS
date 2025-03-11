using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject healthBarUI;
    public Slider healthSlider;

    private bool isDead = false;

    public BoxCollider2D truck;

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        healthBarUI.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        if (currentHealth <= 0 || isDead) return;

        if (!healthBarUI.activeSelf)
        {
            healthBarUI.SetActive(true);
        }

        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    void Die()
    {
        truck.enabled = false;

        isDead = true;
        healthBarUI.SetActive(false);
        Destroy(gameObject);
    }
}
