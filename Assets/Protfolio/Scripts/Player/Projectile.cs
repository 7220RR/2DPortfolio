using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
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
        if (target == null) ProjectilePool.pool.Push(this);
        Move();
    }
    private void Move()
    {
        transform.Translate((target.position-transform.position)*Time.deltaTime*moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            animator.SetTrigger("Hit");
            enemy.TakeDamage(GameManager.Instance.player.DealDamage());
            ProjectilePool.pool.Push(this,0.15f);
        }
    }

}
