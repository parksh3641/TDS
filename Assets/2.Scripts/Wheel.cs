using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private int zombieLimit = 8;

    void Update()
    {
        if (GetZombieCount() >= zombieLimit) return; // 좀비 개수가 많으면 회전 멈춤

        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    int GetZombieCount()
    {
        return GameObject.FindGameObjectsWithTag("Zombie").Length;
    }
}
