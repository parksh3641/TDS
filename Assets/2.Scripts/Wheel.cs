using UnityEngine;

public class Wheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private int zombieLimit = 8;

    void Update()
    {
        if (GetZombieCount() >= zombieLimit) return; // ���� ������ ������ ȸ�� ����

        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
    }

    int GetZombieCount()
    {
        return GameObject.FindGameObjectsWithTag("Zombie").Length;
    }
}
