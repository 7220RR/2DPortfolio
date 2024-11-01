using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float damage;
    public float hp;
    public float maxHp;
    public float hpRecovery;
    private float critical;
    public float criticalHitChance { get { return critical; } set { if (value >= 100) critical = 100; } }
    public float CriticalHitDamage;
    public float attackSpeed;
    public int multiAttack;
    public float moveSpeed;

    private PlayerData playerData;
    public Projectile projectile;
    private Animator animator;
    public ParticleSystem recoveryParticle;
    public SpriteRenderer spriteRenderer;
    private Vector2 startPosition;

    private bool isDead = false;
    private bool isTarget;

    public BackGround backGrounds;
    private Rigidbody2D rb;
    private Enemy target;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        recoveryParticle = Instantiate(recoveryParticle, transform);
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
        StartCoroutine(RecoveryCoroutine());
    }

    /*
     * 
     * private void Update()
{
    if (isDead) return;

    if (target != null && target.isDead)
        target = null;

    if (GameManager.Instance.enemyList.Count <= 0)
    {
        MoveToStart();
        return;
    }

    FindClosestTarget();

    if (target == null)
    {
        MoveToStart();
        return;
    }

    HandleTargetDistance();
}

private void FindClosestTarget()
{
    float targetDic = float.MaxValue;

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
}

private void MoveToStart()
{
    if (rb.position != startPosition)
        Move(startPosition);
    else
        animator.SetBool("IsMoving", false);
}

private void HandleTargetDistance()
{
    float targetDic = Vector2.Distance(target.transform.position, transform.position);

    if (targetDic > 3f)
    {
        Move(target.transform.position);
    }
    else
    {
        animator.SetBool("IsMoving", false);
    }
}

     */

    private void Update()
    {
        if (isDead) return;

        if (target != null && target.isDead)
            target = null;

        if (GameManager.Instance.enemyList.Count <= 0)
        {
            Move(startPosition);
            return;
        }

        float targetDic = float.MaxValue;

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

    private void Move(Vector2 targetPosition)
    {
        backGrounds.Move();

        animator.SetBool("IsMoving", true);

        float dic = Vector2.Distance(targetPosition, rb.position);
        if (dic > 0.1f)
            rb.MovePosition(Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed));
        else
            rb.MovePosition(Vector2.Lerp(transform.position, targetPosition, 1));
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
            StartCoroutine(GameManager.Instance.PlayerDead());
            return;
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

    private void CreateProjectile()
    {
        if (target == null) return;
        Projectile proj = ProjectilePool.pool.Pop();
        if (proj == null) return;
        proj.transform.position = transform.position + new Vector3(0.2f, 0.2f);
        proj.target = target.transform;
    }

    private IEnumerator ProjectileCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => isTarget && !isDead);
            CreateProjectile();
            for (int i = 0; i < multiAttack; i++)
            {
                yield return new WaitForSeconds(0.1f);
                CreateProjectile();
            }

            float delay = (1f / attackSpeed) - (0.1f * multiAttack);

            yield return new WaitForSeconds(Mathf.Max(delay, 0));
        }
    }

    private IEnumerator RecoveryCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => (hp < maxHp && hpRecovery != 0)&& !isDead);
            recoveryParticle.Play();
            hp += hpRecovery;
            if (hp > maxHp) hp = maxHp;
            yield return new WaitForSeconds(5f);
        }
    }

    public void ReStart()
    {
        hp = maxHp;
        transform.position = startPosition;
        target = null;
        isTarget = false;
        isDead = false;
    }
}
