using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint = 3;

    // Event khi object chết
    public System.Action onDead;

    private int healthPoint;

    private void Start()
    {
        healthPoint = defaultHealthPoint;
    }

    public void TakeDamage(int damage)
    {
        if (healthPoint <= 0) return;

        healthPoint -= damage;

        if (healthPoint <= 0)
            Die();
    }

    protected virtual void Die()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 1);

        // gọi event nếu có script khác đăng ký
        onDead?.Invoke();

        Destroy(gameObject);
    }
}