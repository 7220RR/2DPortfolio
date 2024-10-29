using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp;
    private float moveSpeed;
    private float money;
    public float damage;
    private Transform target;
    public EnemyData enemyData;
    public SpriteRenderer spriteRenderer;
    
    private void OnEnable()
    {
        if (GameManager.Instance != null)
            target = GameManager.Instance.player.transform;
        if (enemyData != null)
        {
            gameObject.name=enemyData.name;
            hp=enemyData.hp;
            money=enemyData.money;
            moveSpeed=enemyData.moveSpeed;
            spriteRenderer.sprite = enemyData.enemySprite; 
            damage =enemyData.damage;
        }
    }

    

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            EnemyPool.pool.Push(this);
            GameManager.money += this.money;
        }
    }

}
