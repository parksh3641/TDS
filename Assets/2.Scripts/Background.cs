using UnityEngine;

public class Background : MonoBehaviour
{
    public float speed = 2.0f;
    public float resetPositionX = -10.0f;
    public float startPositionX = 10.0f;
    private int zombieLimit = 8;

    void Update()
    {
        if (GetZombieCount() >= zombieLimit) return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }

    int GetZombieCount()
    {
        return GameObject.FindGameObjectsWithTag("Zombie").Length;
    }
}
