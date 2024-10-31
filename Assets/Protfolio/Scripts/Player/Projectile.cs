using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ProjectilePool.pool.Push(this, 3f);
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(Vector2.right*Time.deltaTime*moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.isDead) return;
            animator.SetTrigger("Hit");
            enemy.TakeDamage(GameManager.Instance.player.DealDamage());
            ProjectilePool.pool.Push(this,0.1f);
        }
    }

}
