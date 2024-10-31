using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp;
    private float moveSpeed = 1f;
    private float money = 10f;
    public float damage;
    private bool isTarget;
    public bool isDead ;

    public Transform target;
    private Animator animator;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if(GameManager.Instance != null)
            GameManager.Instance.enemyList.Add(this);
        StartCoroutine(AttackCoroutine());
        isDead = false;
        isTarget = false;
    }

    private void OnDisable()
    {
        GameManager.Instance.enemyList.Remove(this);
        spriteRenderer.color = Color.white;
    }

    private void Update()
    {
        if (target != null && !isDead)
        {
            float dic = Vector2.Distance(target.position, transform.position);

            if (dic > 2f)
            {
                animator.SetBool("IsMoving", true);
                isTarget = false;
                Move();
            }
            else
            {
                animator.SetBool("IsMoving", false);
                isTarget = true;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(HitCoroutine());
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            StopCoroutine(AttackCoroutine());
            animator.SetBool("IsMoving", false);
            animator.SetTrigger("Death");
            EnemyPool.pool.Push(this,0.5f);
            GameManager.money += this.money;
        }
    }

    private void Move()
    {
        transform.Translate((target.position-transform.position).normalized*moveSpeed*Time.deltaTime);
    }

    private IEnumerator HitCoroutine()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.2f);
    
        spriteRenderer.color = Color.white;
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(()=>isTarget&&!isDead);
            animator.SetBool("IsAttack", true);
            yield return new WaitForSeconds(1f);
            GameManager.Instance.player.TakeDamage(damage);
            animator.SetBool("IsAttack", false);
        }
    }
}
