using UnityEngine;
using TMPro;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public TextMeshPro textMesh;
    public float floatSpeed = 1f;
    public float duration = 0.5f;

    public void SetDamage(int damage)
    {
        textMesh.text = damage.ToString();
        StartCoroutine(FloatAndDisappear());
    }

    IEnumerator FloatAndDisappear()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 1f, 0);

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        Destroy(gameObject);
    }
}
