using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed = 8f;
    public int damage = 1;

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += flySpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
