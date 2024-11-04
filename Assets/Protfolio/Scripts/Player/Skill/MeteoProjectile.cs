using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoProjectile : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.isDead) return;

            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
