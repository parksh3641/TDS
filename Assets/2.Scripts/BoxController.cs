using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoxController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public int maxHealth = 300;
    private int currentHealth;
    public GameObject healthBarUI;
    public Slider healthSlider;

    WaitForSeconds waitForSeconds = new WaitForSeconds(0.2f);

    Color normal = new Color(1, 1, 1, 1);
    Color blink = new Color(1, 1, 1, 0.5f);

    void Start()
    {
        currentHealth = maxHealth;
        healthBarUI.SetActive(false);
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        if (!healthBarUI.activeSelf)
        {
            healthBarUI.SetActive(true);
        }

        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            DestroyBox();
        }

        StopAllCoroutines();
        StartCoroutine(ChangeAlphaTemporarily());
    }

    void DestroyBox()
    {
        Destroy(gameObject);
    }

    IEnumerator ChangeAlphaTemporarily()
    {
        spriteRenderer.color = blink;
        yield return waitForSeconds;
        spriteRenderer.color = normal;
    }
}
