using System.Collections;
using System.Collections.Generic;
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

    public PlayerData playerData;
    public Projectile projectile;
    private Animator animator;
    private Rigidbody2D rb;
    public BackGround[] backGrounds;

    private Vector2 startPosition;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.player = this;
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
        startPosition = new Vector2(-2f, 1.5f);
    }

    private void Update()
    {
        Enemy target = null;
        float targetDic = float.MaxValue;
        if (GameManager.Instance.enemyList.Count <= 0) return;
        foreach (Enemy enemy in GameManager.Instance.enemyList)
        {
            float dic = Vector2.Distance(enemy.transform.position, transform.position);

            if (dic < targetDic)
            {
                target = enemy;
                targetDic = dic;
            }
        }
        if (targetDic > 2f)
        {
            if (target.transform.position.x > 3f)
                Move(startPosition);
            else
                Move(target.transform.position);
        }
        else
            CreatProjectile(target);
    }

    private void Move(Vector2 targetPosition )
    {
        for (int i = 0; i < backGrounds.Length; i++)
        {
            backGrounds[i].Move();
        }
        animator.SetBool("IsMoving", true);
        rb.MovePosition(rb.position + (targetPosition * Time.deltaTime * moveSpeed));
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Hit");
        hp -= damage;
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            //Á×À½
        }
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

    private void CreatProjectile(Enemy enemy)
    {
        Projectile proj = ProjectilePool.pool.Pop();
        proj.target = enemy.transform;
    }
}
