using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp = 1f;
    private float moveSpeed = 1f;
    private float money = 10f;
    public float damage = 1f;
    private Transform target;
    private Animator animator;
    //public EnemyData enemyData;
    //public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            target = GameManager.Instance.player.transform;
        //if (enemyData != null)
        //{
        //    gameObject.name=enemyData.name;
        //    hp=enemyData.hp;
        //    money=enemyData.money;
        //    moveSpeed=enemyData.moveSpeed;
        //    spriteRenderer.sprite = enemyData.enemySprite; 
        //    damage =enemyData.damage;
        //}
    }

    private void Update()
    {
        float dic = Vector2.Distance(target.position, transform.position);

        if (dic > 1f)
        {
            animator.SetBool("IsMoving", true);
            animator.SetBool("IsAttack", false);
            Move();
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        hp -= damage;
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            EnemyPool.pool.Push(this,0.2f);
            GameManager.money += this.money;
        }
    }
    private void Move()
    {
        transform.Translate((target.position-transform.position)*moveSpeed*Time.deltaTime);
    }
    private void OnAttack()
    {

    }
    private IEnumerator AttackCoroutine()
    {

    }

}
