using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float damage;
    public float hp;
    private float maxHp;
    public float hpRecovery;
    private float critical;
    public float criticalHitChance { get {return critical; }  set { if (value >= 100) critical = 100; } }
    public float CriticalHitDamage;
    public float attackSpeed;
    public int multiAttack;
    public float moveSpeed;

    private bool isTarget;

    private PlayerData playerData;
    public Projectile projectile;
    private Animator animator;
    private Rigidbody2D rb;
    public BackGround[] backGrounds;

    private Vector2 startPosition;

    private Enemy target;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.player = this;

        playerData = GameManager.Instance.playerData;

        if (playerData != null)
        {
            damage = playerData.damage;
            hp = playerData.hp;
            hpRecovery = playerData.hpRecovery;
            criticalHitChance = playerData.criticalHitChance;
            CriticalHitDamage = playerData.CriticalHitDamage;
            attackSpeed = playerData.attackSpeed;
            multiAttack = playerData.multiAttack;
        }
        maxHp = hp;
        startPosition = transform.position;
        StartCoroutine(ProjectileCoroutine());
    }

    private void Update()
    {
        if (target != null)
            if (target.isDead) 
                target = null;


        float targetDic = float.MaxValue;
        if (GameManager.Instance.enemyList.Count <= 0) return;

        foreach (Enemy enemy in GameManager.Instance.enemyList)
        {
            if (enemy.isDead) continue;

            float dic = Vector2.Distance(enemy.transform.position, transform.position);
            
            if (dic < targetDic)
            {
                target = enemy;
                targetDic = dic;
            }
        }


        if (target == null)
        {
            if (rb.position != startPosition)
                Move(startPosition);
            else
                animator.SetBool("IsMoving", false);
            isTarget = false;
            return;
        }
        if (targetDic > 3f)
        {
            isTarget = false;
            if (target.transform.position.x > 3f)
            {
                if (rb.position != startPosition)
                    Move(startPosition);
                else
                    animator.SetBool("IsMoving", false);
            }
            else
                Move(target.transform.position);
        }
        else
        {
            isTarget = true;
            animator.SetBool("IsMoving", false);
        }
    }

    private void Move(Vector2 targetPosition )
    {
        for (int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].Move();
        }
        animator.SetBool("IsMoving", true);
        float dic = Vector2.Distance(targetPosition, rb.position);
        if (dic > 0.1f)
        {
            Vector2 rbp = (targetPosition - rb.position).normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + rbp);
        }
        else
            rb.MovePosition(targetPosition);
    }


    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            //Á×À½
        }
        animator.SetTrigger("Hit");
    }

    public float DealDamage()
    {
        if (critical != 0)
        {
            if (critical >= Random.Range(0f, 100f))
            {
                float criDamage = (damage / 100) * CriticalHitDamage;
                return damage + criDamage;
            }
        }
        return damage;
    }

    private void CreatProjectile()
    {
        if (target == null) return;
        Projectile proj = ProjectilePool.pool.Pop();
        proj.target = target.transform;
        proj.transform.position = transform.position + (Vector3.one * 0.2f);
    }

    private IEnumerator ProjectileCoroutine( )
    {
        float interval = 0f;
        while (true)
        {
            yield return new WaitUntil(() => isTarget);
            CreatProjectile();
            for(int i = 0; i < multiAttack; i++)
            {
                yield return new WaitForSeconds(0.1f);
                CreatProjectile();
                interval += 0.1f;
            }
            if (interval >= 1f) continue;
            yield return new WaitForSeconds(attackSpeed-interval);
            interval = 0f;
        }
    }
}
