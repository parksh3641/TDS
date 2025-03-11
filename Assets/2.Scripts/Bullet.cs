using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2.0f;
    private int damage = 10;

    void OnEnable()
    {
        StartCoroutine(DeactivateAfterTime());
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    IEnumerator DeactivateAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zombie"))
        {
            other.transform.parent.GetComponent<ZombieController>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
