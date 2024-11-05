using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    private Animator animator;
    public Transform target;

    private bool isRotated;

    private Vector3 targetDic;
    private Vector3 centerPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        ProjectilePool.pool.Push(this, 3f);
        centerPos = Vector3.forward;
        InitializeTarget();
        isRotated = true;
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnDisable()
    {
        target = null;
        transform.rotation = Quaternion.identity;
    }

    private void InitializeTarget()
    {
        if(target !=null)
        {
            centerPos = ((transform.position + target.position) * 0.5f);
        }
    }

    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        if(centerPos==Vector3.forward)
        {
            InitializeTarget();
            return;
        }

        targetDic = (centerPos - transform.position).normalized;

        if(transform.position.y <= GameManager.Instance.player.transform.position.y)
            isRotated = false;

        if(isRotated)
        transform.rotation = Quaternion.FromToRotation(Vector3.down, targetDic);

        transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (enemy.isDead) return;
            animator.SetTrigger("Hit");
            GetComponent<CircleCollider2D>().enabled = false;
            enemy.TakeDamage(GameManager.Instance.player.DealDamage());
            ProjectilePool.pool.Push(this, 0.25f);
        }
    }

}
