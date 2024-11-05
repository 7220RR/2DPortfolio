using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float moveSpeed;

    public PlayerStatus status;

    private PlayerData playerData;
    public Projectile projectile;
    private Animator animator;
    public ParticleSystem recoveryParticle;
    public SpriteRenderer spriteRenderer;
    private Vector2 startPosition;
    public BackGround backGrounds;
    private Rigidbody2D rb;

    private bool isDead = false;
    private bool isTarget = false;
    private Enemy target;

    private const float player_attack_max_range = 4f;
    private const float move_min_distance = 0.1f;
    private const float target_min_x = 3f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        recoveryParticle = Instantiate(recoveryParticle, transform);
    }

    private void Start()
    {
        if (GameManager.Instance.player == null)
            GameManager.Instance.player = this;

        playerData = GameManager.Instance.playerData;

        if (playerData != null)
            status = playerData.status;
        
        status.maxHp = status.hp;
        startPosition = transform.position;
        StartCoroutine(ProjectileCoroutine());
        StartCoroutine(RecoveryCoroutine());
    }

    private void Update()
    {
        if (isDead) return;

        if (target != null && target.isDead)
            target = null;

        if (GameManager.Instance.enemyList.Count <= 0)
        {
            MoveToStart();
            return;
        }

        FindTarget();

        HandleTargetDistance();
    }

    private void FindTarget()
    {
        float targetDic = float.MaxValue;

        foreach (Enemy enemy in GameManager.Instance.enemyList)
        {
            if(enemy.isDead) continue;
            if(enemy.transform.position.x > target_min_x) continue; 
            float dic = Vector2.Distance(enemy.transform.position , transform.position);

            if (dic < targetDic)
            {
                targetDic = dic;
                target = enemy;
            }
        }
    }

    private void HandleTargetDistance()
    {
        if(target == null) return;

        float targetDic = Vector2.Distance(target.transform.position , transform.position);

        if(targetDic < player_attack_max_range)
        {
            isTarget = true;
            animator.SetBool("IsMoving", false);
        }
        else if (targetDic > player_attack_max_range && target.transform.position.x <= target_min_x)
        {
            isTarget = false;
            Move(target.transform.position);
        }
        else
        {
            isTarget= false;
            MoveToStart();
        }
    }

    private void Move(Vector3 targetPosition)
    {
        backGrounds.Move();

        animator.SetBool("IsMoving", true);

        float dic = Vector2.Distance(targetPosition, rb.position);
        rb.MovePosition(Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed * (dic > move_min_distance ? 1 : 0)));

        if (transform.position == targetPosition)
            animator.SetBool("IsMoving", false);
    }

    private void MoveToStart()
    {
        if (rb.position != startPosition)
            Move(startPosition);
        else
            animator.SetBool("IsMoving", false);
    }

    public void TakeDamage(float damage)
    {
        status.hp -= damage;
        if (status.hp <= 0)
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
        if (status.criticalHitChance != 0)
        {
            if (status.criticalHitChance >= Random.Range(0f, 100f))
            {
                float criDamage = (status.damage / 100) * status.CriticalHitDamage;
                return status.damage + criDamage;
            }
        }
        return status.damage;
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
            for (int i = 0; i < status.multiAttack; i++)
            {
                yield return new WaitForSeconds(0.1f);
                CreateProjectile();
            }

            float delay = Mathf.Max((1f / status.attackSpeed) - (0.1f * status.multiAttack),0);

            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator RecoveryCoroutine()
    {
        while (true)
        {
            yield return new WaitUntil(() => (status.hp < status.maxHp && status.hpRecovery != 0)&& !isDead);
            recoveryParticle.Play();
            status.hp = Mathf.Min(status.hp + status.hpRecovery, status.maxHp);
            yield return new WaitForSeconds(5f);
        }
    }

    public void ReStart()
    {
        status.hp = status.maxHp;
        transform.position = startPosition;
        target = null;
        isTarget = false;
        isDead = false;
    }
}
