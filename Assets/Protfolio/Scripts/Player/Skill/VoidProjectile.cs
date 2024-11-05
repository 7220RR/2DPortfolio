using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidProjectile : MonoBehaviour
{
    private const int maxHitCount = 10;
    private int hitCount = 0;
    public float damage;
    private const float maxDistance = 3.5f;

    private void Update()
    {
        transform.Translate(Vector2.right * 2f * Time.deltaTime);
        if (transform.position.x >= maxDistance)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.isDead) return;

            enemy.TakeDamage(damage);
            hitCount++;
            
            if (hitCount >= maxHitCount)
                Destroy(gameObject);
        }
    }
}
