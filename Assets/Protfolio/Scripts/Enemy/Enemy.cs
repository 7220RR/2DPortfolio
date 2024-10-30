using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp = 1f;
    private float moveSpeed = 1f;
    private float money = 10f;
    public float damage = 1f;
    public Transform target;
    private Animator animator;
    //public EnemyData enemyData;
    //public SpriteRenderer spriteRenderer;
    private bool isTarget = false;
    public bool isDead ;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //OnEnable이  start보다 먼저 일어나므로 오류가 발생
        //에너미를 생성할 때 에너미의 타겟을 변경하는 것으로 수정할 예정
        //if (GameManager.Instance != null)
        //    target = GameManager.Instance.player.transform;

        //에너미 데이터에 따라 다른 모습의 에너미를 만들려 했으나
        //기획자의 의도대로 먼저 만들 예정이라 보류 중
        //if (enemyData != null)
        //{
        //    gameObject.name=enemyData.name;
        //    hp=enemyData.hp;
        //    money=enemyData.money;
        //    moveSpeed=enemyData.moveSpeed;
        //    spriteRenderer.sprite = enemyData.enemySprite; 
        //    damage =enemyData.damage;
        //}
        if(GameManager.Instance != null)
            GameManager.Instance.enemyList.Add(this);
        StartCoroutine(AttackCoroutine());
        isDead = false;
    }

    private void Update()
    {
        if (target != null)
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
            animator.SetTrigger("Death");
            EnemyPool.pool.Push(this,0.2f);
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
            yield return new WaitUntil(()=>isTarget);
            animator.SetBool("IsAttack", true);
            yield return new WaitForSeconds(1f);
            GameManager.Instance.player.TakeDamage(damage);
            animator.SetBool("IsAttack", false);
        }
    }

}
