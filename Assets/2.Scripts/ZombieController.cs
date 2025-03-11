using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ZombieController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator animator;
    public GameObject damageTextPrefab;

    public GameObject healthBarUI; // 체력 UI
    public Slider healthSlider;    // 체력 슬라이더

    private float speed = 2.0f;
    private float jumpForce = 5.0f;
    private int maxHealth = 100;
    private int currentHealth;
    private int damage = 10;
    private float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;

    private bool isAttacking = false;
    private bool isDead = false;
    private bool isJumping = false;

    float otherZombieX, otherZombieZ = 0;
    float myX, myZ = 0;

    BoxController box;
    HeroController hero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetAnimationState("IsIdle");

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        healthBarUI.SetActive(false);
    }

    private void OnEnable()
    {
        healthBarUI.SetActive(false);
    }

    void Update()
    {
        if (!isAttacking && !isDead)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            SetAnimationState("IsIdle");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Box") && !isDead && Time.time >= nextAttackTime)
        {
            isAttacking = true;
            SetAnimationState("IsAttacking");

            box = other.GetComponent<BoxController>();
            if (box != null)
            {
                box.TakeDamage(damage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
        else if (other.CompareTag("Hero") && !isDead && Time.time >= nextAttackTime)
        {
            isAttacking = true;
            SetAnimationState("IsAttacking");

            hero = other.GetComponent<HeroController>();
            if (hero != null)
            {
                hero.TakeDamage(damage);
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box") && isAttacking)
        {
            isAttacking = false;
            SetAnimationState("IsIdle");
        }
        if (other.CompareTag("Hero") && isAttacking)
        {
            isAttacking = false;
            SetAnimationState("IsIdle");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie") && !isJumping)
        {
            otherZombieX = other.transform.position.x;
            myX = transform.position.x;

            otherZombieZ = other.transform.position.z;
            myZ = transform.position.z;

            if (Mathf.Approximately(myZ, otherZombieZ) && myX > otherZombieX)
            {
                JumpOverZombie();
            }
        }
    }


    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        if (!healthBarUI.activeSelf)
        {
            healthBarUI.SetActive(true);
        }

        ShowDamage(damage);

        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ShowDamage(int damage)
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0);
        GameObject damageText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);
        damageText.GetComponent<DamageText>().SetDamage(damage);
    }


    void JumpOverZombie()
    {
        isJumping = true;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        StartCoroutine(ResetJump());
    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
    }

    void Die()
    {
        healthBarUI.SetActive(false);

        isDead = true;
        isAttacking = false;
        SetAnimationState("IsDead");
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    void SetAnimationState(string state)
    {
        animator.SetBool("IsAttacking", state == "IsAttacking");
        animator.SetBool("IsDead", state == "IsDead");
        animator.SetBool("IsIdle", state == "IsIdle");
    }

    public void OnAttack()
    {

    }
}
