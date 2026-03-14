using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint = 3;

    // Event
    public System.Action onDead;
    public System.Action onHealthChanged;

    public int healthPoint;

    private void Start()
    {
        healthPoint = defaultHealthPoint;

        // thông báo UI hoặc script khác biết HP ban đầu
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (healthPoint <= 0) return;

        healthPoint -= damage;

        // báo cho UI hoặc script khác biết HP thay đổi
        onHealthChanged?.Invoke();

        if (healthPoint <= 0)
            Die();
    }

    protected virtual void Die()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 1);

        // gọi event khi chết
        onDead?.Invoke();

        Destroy(gameObject);
    }
}